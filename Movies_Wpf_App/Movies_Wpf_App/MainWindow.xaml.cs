using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Newtonsoft.Json;
using api.Models;
using System.Collections;
using System.Net;


namespace Movies_Wpf_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    /* Developer: Emerald Chen (27044044)

        This app allows a user to log in and therefore he/she will see and edit account details as well as to the movies that they have saved.
     
        On the other hand, the user could search for movies and add them to their profile on a watch later list.
      
       --------------------------------------------------------------------------------------------------------------------------------------

        New features for Assignment 3 :  1) User can register as a new user and log in with new account 
                                         2) User  can add new movies to the database               
    */


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            enteredUsername.Focus();
            GetUsersFromApi.RetrieveUsersFromApi(); // Get updated users from API 

        }
        private static List<Movies> moviesList = GetMoviesFromApi.RetrieveMoviesFromApi();
        private static List<Users> usersList = GetUsersFromApi.RetrieveUsersFromApi();
        private static List<Wishlist> wishList = GetWishlistFromApi.RetrieveSavedMoviesFromApi();

        private static List<string> displayedTitles = new List<string>();
        private static List<string> noDuplicateTitles = new List<string>();

        private static string username;
        private static int loggedInUserId;

        public void DisplayNoDetails()
        {
            title.Text = $"";
            summary.Text = $"";
            genre.Text = $"";
            rating.Text = $"";
            Picture.Source = null;
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            GetUsersFromApi.RetrieveUsersFromApi();
            username = enteredUsername.Text;

            int enteredUnameId = Array.FindIndex(usersList.ToArray(), row => row.Uname == username);
            int enteredPassId = Array.FindIndex(usersList.ToArray(), row => row.Passwrd == enteredPassword.Text);

            // Retrieved from https://stackoverflow.com/questions/4388600/getting-the-index-of-a-particular-item-in-array

            if (enteredUnameId >= 0 && enteredUnameId == enteredPassId)
            {
                userId.Text = $"{ usersList[enteredUnameId].Id}";

                firstName.Text = $"{ usersList[enteredUnameId].Fname}";
                lastName.Text = $"{ usersList[enteredUnameId].Lname}";
                password.Text = $"{ usersList[enteredUnameId].Passwrd}";
                email.Text = $"{ usersList[enteredUnameId].Email}";
                updateButton.IsEnabled = true;
                searchButton.IsEnabled = true;
                detailButton.IsEnabled = true;
                removeButton.IsEnabled = true;
                loginButton.IsEnabled = false;

                switchButton.IsEnabled = true;

                loggedInUserId = int.Parse(userId.Text);

                //WishlistGrid.ItemsSource = titles; ----> Retrieved from http://dotnetpattern.com/wpf-listview-binding

              // Saved movies' titles would be displayed once the user logs in   

                WishlistGrid.ItemsSource = GetNoDuplicateTitles();
                enteredUsername.Text = $"";
                enteredPassword.Text = $"";

                DisplayNoDetails();
            }
            else
            {
                MessageBox.Show("Username or password is incorrect.\n\nPlease try again.");
            }         
        }


        public static List<string> GetWishlistTitles()
        {                
            for (int j = 0; j < wishList.ToArray().Length; j++)
            {
                // MessageBox.Show($"{ wishList[0].Pid}"); ->1
                // MessageBox.Show($"{ wishList[1].Pid}"); ->1
              //  MessageBox.Show($"{ moviesList[wishList[2].Mid].Title}");

                if (loggedInUserId == wishList[j].Pid) // 
                {
                    //  displayedTitles.Add("123");  --> Only for testing
                    //  MessageBox.Show($"{j}"); --> Only for testing

                    displayedTitles.Add(moviesList[wishList[j].Mid - 1].Title); // WHY -1?????
                }
            }
            return displayedTitles;
        }

        public static List<string> GetNoDuplicateTitles()
        {        
            noDuplicateTitles = GetWishlistTitles().Distinct().ToList();  // ----> Retrieved from https://stackoverflow.com/questions/47752/remove-duplicates-from-a-listt-in-c-sharp

            return noDuplicateTitles;
        }

        private void Button_ShowDetail(object sender, RoutedEventArgs e)
        {
            if (WishlistGrid.SelectedIndex < 0 )
            {
                MessageBox.Show("Please click one movie in your wishlist");
            }
            else
            {
                GetMoviesFromApi.RetrieveMoviesFromApi();
                GetWishlistFromApi.RetrieveSavedMoviesFromApi();

                for (int j = 0; j < moviesList.Count; j++)
                {
                    if(noDuplicateTitles[WishlistGrid.SelectedIndex] == moviesList[j].Title)
                    {
                        // MessageBox.Show($"{noDuplicateTitles[WishlistGrid.SelectedIndex]}");
                       // MessageBox.Show($"{moviesList[j - 1].Title}");

                        title.Text = $"{moviesList[j].Title}";
                        summary.Text = $"{moviesList[j].Summary}";
                        genre.Text = $"{moviesList[j].Genre}";
                        rating.Text = $"{moviesList[j].Rating}";
                        Picture.Source = new BitmapImage(new Uri(@"https://m.media-amazon.com/images/M/" + moviesList[j].Picture, UriKind.Absolute));

                    }
                }
            }
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            GetUsersFromApi.RetrieveUsersFromApi();
            if (userId == null || firstName.Text == "" || lastName.Text == "" || password.Text == "" || email.Text == "")
            {
                MessageBox.Show("All fields must be filled in please");
            }
            else
            {
                MessageBox.Show(PutUsers.UpdateUsers(int.Parse(userId.Text), firstName.Text, lastName.Text, password.Text, email.Text, username)); 
                MessageBox.Show("If you updated yoour password, remember to log in with it next time");
            }
        }
            

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            GetMoviesFromApi.RetrieveMoviesFromApi(); // Refresh the app
            //  WishlistGrid.ItemsSource = GetMoviesFromApi();
            if (search.Text == "")
            {
                MessageBox.Show("Please type a movie title");
            }
            
            int i = 0;
            while(i < moviesList.ToArray().Length)
            {
                if (search.Text.ToLower() == moviesList[i].Title.ToLower())
                {
                    MessageBox.Show("Movie found!\n\nPlease click Add button to add it to your wishlist");
                    addMovieButton.IsEnabled = true;
                 
                    break;
                }
                i++;
            }
            //MessageBox.Show($"{i}");

            if (i == moviesList.ToArray().Length && search.Text != "")
            {
                MessageBox.Show("OOps! Not found. \n\nPlease search for aother movie");
            }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
      
        private void Button_AddMovie(object sender, RoutedEventArgs e)
        {
            List<string> titles = GetNoDuplicateTitles();

            int personId = int.Parse(userId.Text);
            int movieId = Array.FindIndex(moviesList.ToArray(), row => row.Title.ToLower() == search.Text.ToLower()); // Here's why search.Text can NOT be null after clicking Search
           // MessageBox.Show($"{movieId}");

            PostWishlist.SaveMovies(personId, (movieId + 1)); // Index origin = 0 whereas id origin = 1         

            MessageBox.Show("Movie has been added to your wishlist!"); 

            titles.Add(moviesList[movieId].Title); // Index origin = 0 whereas id origin = 1

            WishlistGrid.ItemsSource = titles; // Updated UI
            
            addMovieButton.IsEnabled = false;
            search.Text = $"";
        }

        private void Button_Remove(object sender, RoutedEventArgs e)
        {
            //  WishlistGrid.ItemsSource = GetNoDuplicateTitles();

            GetMoviesFromApi.RetrieveMoviesFromApi(); // Refresh the app

            int personId = int.Parse(userId.Text);

            // MessageBox.Show($"{WishlistGrid.SelectedIndex}");

            if (WishlistGrid.SelectedIndex < 0)
            {
                MessageBox.Show("Please click one movie in your wishlist");

                DisplayNoDetails();
            }
            else
            {
                //GetWishlistTitles();
                //GetNoDuplicateTitles();
                //int x = wishList[WishlistGrid.SelectedIndex].Mid;
                //string newTitle = moviesList[x-1].Title;

                List<string> titles = GetNoDuplicateTitles();

               // titles.Add(newTitle);

                // MessageBox.Show($"{titles[WishlistGrid.SelectedIndex]}");
                // MessageBox.Show($"{moviesList[3].Title}");

                // List<string> titles = GetWishlistTitles();

                for (int j = 0; j < moviesList.Count; j++)
                {                   
                    WishlistGrid.ItemsSource = GetNoDuplicateTitles();
                    int z = WishlistGrid.SelectedIndex;
                    if (z >= 0 & titles[z] == moviesList[j+1].Title)     // index still out of range sometimes :(
                    {
                        int MIdFound = Array.FindIndex(moviesList.ToArray(), row => row.Title == titles[z]);

                        int IdFound = Array.FindIndex(wishList.ToArray(), row => row.Mid == (MIdFound+1));

                        //  MessageBox.Show($"{MIdFound + 1}");

                        //  MessageBox.Show($"{IdFound}");
                      

                        int id = wishList[IdFound].Id;

                        DeleteWishlist.DeleteSavedMovie(id, personId, (MIdFound+1));
                        

                        MessageBox.Show("Movie has been removed");

                        // MessageBox.Show($"{noDuplicateTitles[WishlistGrid.SelectedIndex]}");
                        // MessageBox.Show($"{moviesList[j - 1].Title}");

                        DisplayNoDetails();
                        

                        for (int i = 0; i < wishList.Count; i++)
                        {
                            if (wishList[i].Mid == (MIdFound + 1) && wishList[i].Pid == personId)
                            {
                                string item = moviesList[MIdFound].Title;

                                //  MessageBox.Show($"{item}");

                                titles.Remove(item);


                                WishlistGrid.ItemsSource = titles; // Also remove the title from UI
                            }
                        }
                        break;
                    }
                }            
            }
        }
        private void Button_Switch(object sender, RoutedEventArgs e)
        {
            GetUsersFromApi.RetrieveUsersFromApi();

            // Application.Current.Shutdown();  

            loginButton.IsEnabled = true;
            DisplayNoDetails();
            userId.Text = "";
            firstName.Text = "";
            lastName.Text = "";
            password.Text = "";
            email.Text = "";

            updateButton.IsEnabled = false;
            searchButton.IsEnabled = false;
            detailButton.IsEnabled = false;
            removeButton.IsEnabled = false;
            switchButton.IsEnabled = false;

        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
             RegistrationForm window = new RegistrationForm();

             window.Show();
             this.Close();
        }

        private void searchNewMovie_Click(object sender, RoutedEventArgs e)
        {
            MovieAddingForm window = new MovieAddingForm();

            window.Show();
            this.Close();
        }
    }
}


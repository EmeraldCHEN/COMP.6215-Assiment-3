using System;
using System.Windows;

namespace Movies_Wpf_App
{
    /// <summary>
    /// Interaction logic for MovieAddingForm.xaml
    /// </summary>
    public partial class MovieAddingForm : Window
    {
        public MovieAddingForm()
        {
            InitializeComponent();
        }

        private void Button_AddMovie(object sender, RoutedEventArgs e)
        {
            if(movieTitle.Text != "" && movieSummary.Text != "" && imgAdrress.Text != "" && movieGenre.Text != "" && movieRate.Text != "")
            {
                PostMovies.AddMovies(movieTitle.Text, movieSummary.Text, imgAdrress.Text, movieGenre.Text, Double.Parse(movieRate.Text));
                MessageBox.Show("Movie has been added");
                movieTitle.Text = "";
                movieSummary.Text = "";
                imgAdrress.Text = "";
                movieGenre.Text = "";
                movieRate.Text = "";

            }
            else
            {
                MessageBox.Show("Please fill in all fields");
            }
            
        }

        /* e.g.

          1.  Title -- Cinderella

              Rate -- 6.9

              Genre -- Romance

              Image Adrress -- MV5BMjMxODYyODEzN15BMl5BanBnXkFtZTgwMDk4OTU0MzE@._V1_UX182_CR0,0,182,268_AL_
                   
              Summary -- When her father unexpectedly dies, young Ella finds herself at the mercy of her cruel stepmother and her scheming stepsisters. 
              Never one to give up hope, Ella's fortunes begin to change after meeting a dashing stranger.
              
        
           2.  Title -- Rocketman

               Rate -- 7.7

               Genre -- Biography

               Image Adrress -- MV5BMTY0MzUwODc4N15BMl5BanBnXkFtZTgwMjMyMjY0NzM@._V1_UX182_CR0,0,182,268_AL_

               Summary -- A musical fantasy about the fantastical human story of Elton John's breakthrough years.
                        
             
             */

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}

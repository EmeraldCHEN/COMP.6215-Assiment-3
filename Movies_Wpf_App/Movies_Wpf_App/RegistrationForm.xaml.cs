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

using api.Models;

// Code below  4 Assignment 3 ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

namespace Movies_Wpf_App
{
    /// <summary>
    /// Interaction logic for RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        public RegistrationForm()
        {
           InitializeComponent();
        }
  
 
        private void Button_Register(object sender, RoutedEventArgs e)
        {
            GetUsersFromApi.RetrieveUsersFromApi();
            Start();        
        }

        public void Start()
        {
            List<Users> usersList = GetUsersFromApi.RetrieveUsersFromApi();
            List<string> fNameList = new List<string>();
            List<string> lNameList = new List<string>();

            for (int id = 0; id < usersList.Count; id++)
            {
                fNameList.Add(usersList[id].Fname);
                lNameList.Add(usersList[id].Lname);
            }

            int FirstNameId = fNameList.FindIndex(f => f.Contains(registerFName.Text));

            int LastNameId = lNameList.FindIndex(l => l.Contains(registerLName.Text));

            if (registerFName.Text != "" && registerLName.Text != "" && registerPassword.Text != "" && registerEmail.Text != "")
            {             
                if (FirstNameId >= 0 && FirstNameId == LastNameId)
                {
                    MessageBox.Show("This user's already registered. \n\nPlease login.");
                }
                else
                {
                    char firstChar = registerFName.Text.ToString().ToUpper()[0]; // In C#, ToUpper() is a string method

                    string lowerLastName = registerLName.Text.ToString().ToLower();

                    string secondPart = char.ToUpper(lowerLastName[0]) + lowerLastName.Substring(1);

                    string newUserName = firstChar + secondPart;
                    PostUsers.AddUsers(registerFName.Text, registerLName.Text, registerPassword.Text, registerEmail.Text, newUserName);
                    MessageBox.Show("Your username: " + $"{newUserName}" + "\n\nYou could log in with your username & password now!");
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields then cliclk the button again");
            }
           
        }

        private void Button_Login(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}            



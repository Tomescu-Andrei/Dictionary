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
using System.Windows.Shapes;

namespace Dictionar_2._0
{
    public partial class LogIn : Window
    {
        private AddMore addMoreInstance; 

        private List<User> users = new List<User>();
        UserList usersList = new UserList();
        public LogIn()
        {
            InitializeComponent();
            usersList.deserializeUsers();
            addMoreInstance = new AddMore(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = userIdBox.Text;
            string password = passwordBox.Password;
            bool userFound=false;

            foreach (User user in usersList.users)
            {
                if (user.id == username && user.password == password)
                {
                    userFound = true;
                    break;
                }
            }
            if (userFound ==true) 
            {
                userIdBox.Clear();
                passwordBox.Clear();
                this.Hide();
                addMoreInstance.Show();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(userIdBox.Text) || string.IsNullOrWhiteSpace(passwordBox.Password))
                {
                    MessageBox.Show("Toate câmpurile sunt obligatorii. Vă rugăm să completați toate informațiile.", "Avertisment", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                MessageBox.Show("User sau parola incorecta");

                }

            }
        }
    }
}

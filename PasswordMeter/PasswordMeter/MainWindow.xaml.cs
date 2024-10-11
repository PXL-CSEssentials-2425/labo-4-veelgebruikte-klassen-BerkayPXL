using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

namespace PasswordMeter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Input velden: userNameTextBox en passwordTextBox
        /// Output veld: resultTextBlock
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();
        }

        private void passwordMeterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();

            int passwordStrenght = 0;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                resultTextBlock.Text = "Please enter a username and password";
                return;
            }

            if(!password.Contains(username))
            {
                passwordStrenght++;
            }

            if (password.Length >= 10)
            {
                passwordStrenght++;
            }

            bool hasdigit = false;
            bool hasUpper = false;
            bool hasLower = false;

            foreach (char c in password.ToCharArray())
            {
                if (char.IsDigit(c))
                {
                    hasdigit = true;
                }
                if(char.IsUpper(c))
                {
                    hasUpper = true;
                }
                if(char.IsLower(c))
                {
                    hasLower |= true;
                }
            }

            if (hasdigit)
            {
                passwordStrenght++;
            }
            if (hasUpper)
            {
                passwordStrenght++;
            }
            if (hasLower)
            {
                passwordStrenght++;
            }

            switch (passwordStrenght)
            {
                case 5:
                    resultTextBlock.Text = "Strongpassword";
                    resultTextBlock.Foreground = Brushes.Green;
                    break;

                case 4:
                    resultTextBlock.Text = "Medium password";
                    resultTextBlock.Foreground = Brushes.Orange;
                    break;

                default:
                    resultTextBlock.Text = "Password is weak";
                    resultTextBlock.Foreground = Brushes.Red;
                    break;                 
            }
            Random rnd = new Random();
            StringBuilder passwordBuilder = new StringBuilder();

            // 5 letters from username:
            for (int i = 0; i < 5; i++)
            {
                int randomPosition = rnd.Next(0, username.Length);
                string randomChar = username.Substring(randomPosition, 1);

                passwordBuilder.Append(randomChar.ToLower());
            }

            // 5 random digits
            for (int i = 0; i < 5; i++)
            {
                int randomNumber = rnd.Next(0, 10);
                passwordBuilder.Append(randomNumber);
            }

            // 2 letters from username
            for (int i = 0; i < 2; i++)
            {
                int randomPosition = rnd.Next(0, username.Length);
                string randomChar = username.Substring(randomPosition, 1);

                passwordBuilder.Append(randomChar.ToUpper());
            }

            // random number of exclamation marks
            for (int i = 0; i < rnd.Next(1, 6); i++)
            {
                passwordBuilder.Append("!");
            }

            //resultTextBlock.Text = passwordBuilder.ToString();

            MessageBoxResult result = 
            MessageBox.Show("Willekeurig wachtwoord gebruiken?", "Zwak wachtwoord", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if(result == MessageBoxResult.Yes)
            {
                passwordTextBox.Text = passwordBuilder.ToString();
            }
        }
    }
}

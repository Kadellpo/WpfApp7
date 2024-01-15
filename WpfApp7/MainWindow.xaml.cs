using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace WpfApp7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private bool ContainsSpecialCharacters(string password)
        {
            char[] specialCharacters = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')' };

            return password.Any(c => specialCharacters.Contains(c));
        }
        private void password_GotFocus(object sender, RoutedEventArgs e)
        {
            if (password.Text == "Придумайте пароль" || password.Text == "Пароль должен содержать больше 6 символов" || password.Text == "Пароль не содержит специальных символов." || password.Text == "Пароли не совпадают")
            {
                password.Text = "";
                password.Foreground = Brushes.Black;
                password.BorderBrush = Brushes.Black;
            }
        }

        private void password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(password.Text))
            {
                password.Text = "Придумайте пароль";
                password.Foreground = Brushes.Gray;
            }
        }
        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (textBox.Text == "Введите почту" || textBox.Text == "Некорректно введена почта")
            {
                textBox.Text = "";
                textBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "Введите почту";
                textBox.Foreground = Brushes.Gray;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Authorization auth = new Authorization();
            auth.Show();
            this.Close();
        }
            bool checkEmail;
        bool check;

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            var Login = login.Text;
            var Email = textBox.Text;
            var Password = password.Text;
            var context = new AppDbContext();
            if (login.Text is null || login.Text == "Придумайте логин")
            {
                login.BorderBrush = Brushes.Red;
                login.Foreground = Brushes.Red;
                login.Text = "Не введен логин";
                return;
            }
            if (Email != "Введите почту")
            {
                if (Email.Contains("@"))
                {
                    string[] words = Email.Split("@");

                    if (Regex.IsMatch(words[0], "^[a-zA-Z0-9]*$"))
                    {
                        checkEmail = true;
                    }
                    else
                    {
                        textBox.BorderBrush = Brushes.Red;
                        textBox.Text = "Некорректно введена почта";
                        textBox.Foreground = Brushes.Red;
                        return;


                    }
                    if (words[1].Contains("."))
                    {
                        string[] latters = words[1].Split(".");
                        if (latters[1].Length >= 2)
                        {
                            if (checkEmail == true)
                            {
                                var userAdd1 = context.Users.SingleOrDefault(x => x.Email == textBox.Text);
                                if (userAdd1 == null)
                                {
                                    check = true;
                                }
                                else
                                {
                                    textBox.BorderBrush = new SolidColorBrush(Colors.Red);
                        textBox.Foreground = Brushes.Red;
                                    textBox.Text = "Почта уже зарегестрированна";
                                }
                            }
                        }
                        else
                        {
                            textBox.BorderBrush = new SolidColorBrush(Colors.Red);
                        textBox.Foreground = Brushes.Red;
                            textBox.Text = "Некорректно введена почта";

                        }
                    }
                    else
                    {
                        textBox.BorderBrush = new SolidColorBrush(Colors.Red);
                        textBox.Foreground = Brushes.Red;
                        textBox.Text = "Некорректно введена почта";

                    }
                }
                else
                {
                    textBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    textBox.Text = "Некорректно введена почта";

                        return;
                }
            }
            if (ContainsSpecialCharacters(Password))
            {
            }
            else
            {
                password.BorderBrush = Brushes.Red;
                password.Foreground = Brushes.Red;
                passwordRepeat.BorderBrush = Brushes.Red;
                passwordRepeat.Foreground = Brushes.Red;
                password.Text = "Пароль не содержит специальных символов.";
                passwordRepeat.Text = "";
                return;
            }
            if (password.Text != passwordRepeat.Text)
            {
                password.BorderBrush = Brushes.Red;
                password.Foreground = Brushes.Red;
                passwordRepeat.BorderBrush = Brushes.Red;
                passwordRepeat.Foreground = Brushes.Red;
                password.Text = "Пароли не совпадают";
                passwordRepeat.Text = "";
                return;
            }
            var user_exists = context.Users.FirstOrDefault(x => x.Login == Login );
            var user_exists2 = context.Users.FirstOrDefault(x => x.Email == Email );
            if (password.Text.Length < 6 && passwordRepeat.Text.Length < 6)
            {
                password.BorderBrush = Brushes.Red;
                password.Foreground = Brushes.Red;
                passwordRepeat.BorderBrush = Brushes.Red;
                passwordRepeat.Foreground = Brushes.Red;
                password.Text = "Пароль должен содержать больше 6 символов";
                passwordRepeat.Text = "";
                return;
            }

            if (password.Text != passwordRepeat.Text)
            {
                password.BorderBrush = Brushes.Red;
                passwordRepeat.BorderBrush = Brushes.Red;
                password.Foreground = Brushes.Red;
                passwordRepeat.Foreground = Brushes.Red;
                return;
            }
            if (user_exists is not null || user_exists2 is not null)
            {
                MessageBox.Show("Такой пользователь уже согрешил");
                return;
            }
            var user = new User { Login = Login, password = Password, Email = Email };
            context.Users.Add(user);
            context.SaveChanges();
            MessageBox.Show("Welcome to the Hell");
        }

        private void passwordRepeat_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passwordRepeat.Text == "Повторите пароль")
            {
                passwordRepeat.Text = "";
                passwordRepeat.Foreground = Brushes.Black;
                passwordRepeat.BorderBrush = Brushes.Black;
            }
        }

        private void passwordRepeat_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordRepeat.Text))
            {
                passwordRepeat.Text = "Повторите пароль";
                passwordRepeat.Foreground = Brushes.Gray;
            }
        }

        private void login_GotFocus(object sender, RoutedEventArgs e)
        {
            if (login.Text == "Придумайте логин" || login.Text == "Не введен логин")
            {
                login.Text = "";
                login.Foreground = Brushes.Black;
                login.BorderBrush = Brushes.Black;
            }
        }

        private void login_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(login.Text))
            {
                login.Text = "Придумайте логин";
                login.Foreground = Brushes.Gray;
            }
        }
    }
}

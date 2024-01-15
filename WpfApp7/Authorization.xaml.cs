using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace WpfApp7
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void auth_Click(object sender, RoutedEventArgs e)
        {
            var log = AuthLog.Text;
            var pas = AuthPas.Text;

            var context = new AppDbContext();

            var user = context.Users.SingleOrDefault(x => x.Login == log && x.password == pas || x.Email == log);
            if (user is null)
            {
                MessageBox.Show("Неправмльный логин или пароль");
                AuthLog.BorderBrush = Brushes.Red;
                AuthLog.Foreground = Brushes.Red;
                AuthPas.Foreground = Brushes.Red;
                AuthPas.BorderBrush = Brushes.Red;
                return;
            }
            MessageBox.Show("Вы успешно вошли!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Site site = new Site();
            site.Show();
            this.Close();
            var log1 = context.Users.SingleOrDefault(x => (x.Email == log));
            site.hello.Text +=  " " + log1.Login;
        }

        private void AuthPas_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AuthPas.Text == "Введите пароль")
            {
                AuthPas.Text = "";
                AuthPas.Foreground = Brushes.Black;
                AuthPas.BorderBrush = Brushes.Black;
            }
        }

        private void AuthPas_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AuthPas.Text))
            {
                AuthPas.Text = "Введите пароль";
                AuthPas.Foreground = Brushes.Gray;
            }
        }

        private void AuthLog_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AuthLog.Text == "Введите почту")
            {
                AuthLog.Text = "";
                AuthLog.Foreground = Brushes.Black;
                AuthLog.BorderBrush = Brushes.Black;
            }
        }

        private void AuthLog_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AuthLog.Text))
            {
                AuthLog.Text = "Введите логин или почту";
                AuthLog.Foreground = Brushes.Gray;
            }
        }
    }
}

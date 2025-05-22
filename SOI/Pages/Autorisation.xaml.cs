using SOI.AppicationData;
using System;
using System.Collections.Generic;
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

namespace SOI.Pages
{
    public partial class Autorisation : Page
    {
        public Autorisation()
        {
            InitializeComponent();
            EnterEnable();
        }

        private void EnterEnable()
        {
            if (txbPassword.Password.Length > 0 && txbLogin.Text.Length > 0)
            {
                Enter.IsEnabled = true;
            }
            else
            {
                Enter.IsEnabled = false;
            }
        }

        private void txbLogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbLogin.Text))
            {
                txbLogin.Visibility = Visibility.Collapsed;
                txbLoginPlaceHolder.Visibility = Visibility.Visible;
            }
        }

        private void txbLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            EnterEnable();
        }

        private void txbLoginPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            txbLoginPlaceHolder.Visibility = Visibility.Collapsed;
            txbLogin.Visibility = Visibility.Visible;
            txbLogin.Focus();
        }

        private void txbPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbPassword.Password))
            {
                txbPassword.Visibility = Visibility.Collapsed;
                txbPasswordPlaceHolder.Visibility = Visibility.Visible;
            }
        }

        private void txbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            EnterEnable();
        }

        private void txbPasswordPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            txbPasswordPlaceHolder.Visibility = Visibility.Collapsed;
            txbPassword.Visibility = Visibility.Visible;
            txbPassword.Focus();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SOI_Users userObj = AppConnect.model.SOI_Users.FirstOrDefault(x => x.user_login == txbLogin.Text && x.user_password == txbPassword.Password);

                if (userObj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Ошибка при авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                else
                {
                    switch (userObj.user_role_id)
                    {
                        case 3:
                            MessageBox.Show("Здравствуйте, пользователь " + userObj.user_login + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.frameMain.Navigate(new AskPage(userObj));
                            break;
                        case 2:
                            MessageBox.Show("Здравствуйте, менеджер " + userObj.user_login + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.frameMain.Navigate(new ItemPage(userObj));
                            break;
                        case 1:
                            MessageBox.Show("Здравствуйте, администратор " + userObj.user_login + "!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                            AppFrame.frameMain.Navigate(new ItemPage(userObj));
                            break;
                        default:
                            MessageBox.Show("Ощибка данных на сервере.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: \n" + ex.Message.ToString(), "Критическая работа приложения!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ExitB_Click(object sender, RoutedEventArgs e)
        {
            var exitBi = MessageBox.Show("Вы уверенны, что хотите закрыть приложение?", "Выход из приложения", MessageBoxButton.YesNo, MessageBoxImage.Hand);

            if (exitBi == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}

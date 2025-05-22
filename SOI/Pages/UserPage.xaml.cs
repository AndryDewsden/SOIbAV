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
    public partial class UserPage : Page
    {
        SOI_Users current_user = new SOI_Users();
        public UserPage(SOI_Users user)
        {
            InitializeComponent();
            current_user = user;

            WindowShowList(listUsers, 2);
            WindowShowPanel(userdactor, 2);
            WindowShowList(listReport, 2);
            WindowShowPanel(ordersdactor, 2);
            WindowShowCombo(projectSearchCombo, 2);
            WindowShowList(listRequest, 2);
            WindowShowList(listOff, 2);

            cbx_user_role.Items.Add("--выбрать--");
            for (int i = 0; i < AppConnect.model.SOI_User_Roles.ToList().Count; i++)
            {
                cbx_user_role.Items.Add(AppConnect.model.SOI_User_Roles.ToList()[i]);
            }

            switch (current_user.user_role_id)
            {
                case 1:
                    Title = $"Страница администратора {current_user.user_first_name}";

                    ComboMenu.Items.Add("Пользователи");
                    //ComboMenu.Items.Add("Отчёты");
                    //ComboMenu.Items.Add("Запросы");
                    //ComboMenu.Items.Add("Списанное");

                    cbx_user_role.IsEnabled = true;
                    cbx_user_role.Visibility = Visibility.Visible;

                    l_user_role.Visibility = Visibility.Collapsed;

                    //prostatusBox.Items.Add("--выбрать--");
                    //for (int i = 0; i < AppConnect.model1db.Statuses_Cursach.ToList().Count; i++)
                    //{
                    //    prostatusBox.Items.Add(AppConnect.model1db.Statuses_Cursach.ToList()[i]);
                    //}

                    //employeeCombo.Items.Add("--выбрать--");
                    //for (int i = 0; i < AppConnect.model1db.Employees_Cursach.ToList().Count; i++)
                    //{
                    //    employeeCombo.Items.Add(AppConnect.model1db.Employees_Cursach.ToList()[i]);
                    //}

                    //projectSearchCombo.Items.Add("--выбрать--");
                    //for (int i = 0; i < AppConnect.model1db.Projects_Cursach.ToList().Count; i++)
                    //{
                    //    projectSearchCombo.Items.Add(AppConnect.model1db.Projects_Cursach.ToList()[i]);
                    //}

                    //clientBox.Items.Add("--выбрать--");
                    //for (int i = 0; i < AppConnect.model1db.Customers_Cursach.ToList().Count; i++)
                    //{
                    //    clientBox.Items.Add(AppConnect.model1db.Customers_Cursach.ToList()[i]);
                    //}

                    //creatorBox.Items.Add("--выбрать--");
                    //for (int i = 0; i < AppConnect.model1db.Employees_Cursach.ToList().Count; i++)
                    //{
                    //    creatorBox.Items.Add(AppConnect.model1db.Employees_Cursach.ToList()[i]);
                    //}

                    //employeeComboGroup.Items.Add("--выбрать--");
                    //for (int i = 0; i < AppConnect.model1db.Employees_Cursach.ToList().Count; i++)
                    //{
                    //    employeeComboGroup.Items.Add(AppConnect.model1db.Employees_Cursach.ToList()[i]);
                    //}

                    //projectComboGroup.Items.Add("--выбрать--");
                    //for (int i = 0; i < AppConnect.model1db.Projects_Cursach.ToList().Count; i++)
                    //{
                    //    projectComboGroup.Items.Add(AppConnect.model1db.Projects_Cursach.ToList()[i]);
                    //}

                    break;

                case 3:
                    Title = $"Страница пользователя {current_user.user_first_name}";

                    //AppFrame.frameMain.Navigate(new Listemployee(use));
                    break;

                case 2:
                    Title = $"Страница менеджера {current_user.user_first_name}";

                    cbx_user_role.Items.Add("--выбрать--");

                    ComboMenu.Items.Add("Пользователи");
                    ComboMenu.Items.Add("Отчёты");
                    ComboMenu.Items.Add("Запросы");
                    ComboMenu.Items.Add("Списанное");


                    prostatusBox.Items.Add("");

                    cbx_user_role.IsEnabled = false;
                    cbx_user_role.Visibility = Visibility.Collapsed;

                    l_user_role.Visibility = Visibility.Visible;

                    //for (int i = 0; i < AppConnect.model1db.Statuses_Cursach.ToList().Count; i++)
                    //{
                    //    prostatusBox.Items.Add(AppConnect.model1db.Statuses_Cursach.ToList()[i]);
                    //}

                    break;

                default:
                    MessageBox.Show("Произошла какае-то ощибка с данными пользователя. Вас перекинут на страницу авторизации.", "О-оу", MessageBoxButton.OK, MessageBoxImage.Error);
                    AppFrame.frameMain.Navigate(new Autorisation());
                    break;
            }
            listUsers.ItemsSource = fillUsers();
            listReport.ItemsSource = fillReports();
        }

        SOI_Users[] fillUsers()
        {
            var pro = AppConnect.model.SOI_Users.ToList();
            return pro.ToArray();
        }

        SOI_Reports[] fillReports() 
        {
            var pro = AppConnect.model.SOI_Reports.ToList();
            return pro.ToArray();
        }

        SOI_Requests[] fillRequests()
        {
            var pro = AppConnect.model.SOI_Requests.ToList();
            return pro.ToArray();
        }

        SOI_Offs[] fillOffs()
        {
            var pro = AppConnect.model.SOI_Offs.ToList();
            return pro.ToArray();
        }

        //прятать/показывать списки
        private void WindowShowList(System.Windows.Controls.ListView sim, int s)
        {
            switch (s)
            {
                case 1:
                    sim.IsEnabled = true;
                    sim.Visibility = Visibility.Visible;
                    break;
                case 2:
                    sim.IsEnabled = false;
                    sim.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        //прятать/показывать панели
        private void WindowShowPanel(StackPanel sim, int s)
        {
            switch (s)
            {
                case 1:
                    sim.IsEnabled = true;
                    sim.Visibility = Visibility.Visible;
                    break;
                case 2:
                    sim.IsEnabled = false;
                    sim.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        //прятать/показывать кнопки
        private void WindowShowButton(System.Windows.Controls.Button sim, int s)
        {
            switch (s)
            {
                case 1:
                    sim.IsEnabled = true;
                    sim.Visibility = Visibility.Visible;
                    break;
                case 2:
                    sim.IsEnabled = false;
                    sim.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        //прятать/показывать комбобоксы
        private void WindowShowCombo(System.Windows.Controls.ComboBox sim, int s)
        {
            switch (s)
            {
                case 1:
                    sim.IsEnabled = true;
                    sim.Visibility = Visibility.Visible;
                    break;
                case 2:
                    sim.IsEnabled = false;
                    sim.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void ComboMenu_DropDownClosed_1(object sender, EventArgs e)
        {
            switch (ComboMenu.SelectionBoxItem.ToString())
            {
                case "Пользователи":
                    WindowShowList(listUsers, 1);
                    WindowShowPanel(userdactor, 1);

                    WindowShowList(listReport, 2);
                    WindowShowPanel(ordersdactor, 2);

                    WindowShowCombo(projectSearchCombo, 2);
                    WindowShowList(listRequest, 2);
                    WindowShowPanel(groupdactor, 2);

                    WindowShowList(listOff, 2);
                    break;

                case "Отчёты":
                    WindowShowList(listUsers, 2);
                    WindowShowPanel(userdactor, 2);

                    WindowShowList(listReport, 1);
                    WindowShowPanel(ordersdactor, 1);

                    WindowShowCombo(projectSearchCombo, 2);
                    WindowShowList(listRequest, 2);
                    WindowShowPanel(groupdactor, 2);

                    WindowShowList(listOff, 2);
                    break;

                case "Запросы":
                    WindowShowList(listUsers, 2);
                    WindowShowPanel(userdactor, 2);

                    WindowShowList(listReport, 2);
                    WindowShowPanel(ordersdactor, 2);

                    WindowShowCombo(projectSearchCombo, 1);
                    WindowShowList(listRequest, 1);
                    WindowShowPanel(groupdactor, 1);

                    WindowShowList(listOff, 2);
                    break;

                case "Списанное":
                    WindowShowList(listUsers, 2);
                    WindowShowPanel(userdactor, 2);

                    WindowShowList(listReport, 2);
                    WindowShowPanel(ordersdactor, 2);

                    WindowShowCombo(projectSearchCombo, 2);
                    WindowShowList(listRequest, 2);
                    WindowShowPanel(groupdactor, 2);

                    WindowShowList(listOff, 1);
                    break;

                default:
                    WindowShowList(listUsers, 2);
                    WindowShowPanel(userdactor, 2);

                    WindowShowList(listReport, 2);
                    WindowShowPanel(ordersdactor, 2);

                    WindowShowCombo(projectSearchCombo, 2);
                    WindowShowList(listRequest, 2);
                    WindowShowPanel(groupdactor, 2);

                    WindowShowList(listOff, 2);
                    break;
            }
        }

        private void tbx_user_firstname_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_firstname, tbx_user_firstname_placeholder, 2);
        }

        private void tbx_user_firstname_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_firstname, tbx_user_firstname_placeholder, 1);
            tbx_user_firstname.Focus();
        }

        private void tbx_user_lastname_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_lastname, tbx_user_lastname_placeholder, 2);
        }

        private void tbx_user_lastname_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_lastname, tbx_user_lastname_placeholder, 1);
            tbx_user_lastname.Focus();
        }

        private void tbx_user_surname_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_surname, tbx_user_surname_placeholder, 2);
        }

        private void tbx_user_surname_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_surname, tbx_user_surname_placeholder, 1);
            tbx_user_surname.Focus();
        }

        private void tbx_user_position_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_position, tbx_user_position_placeholder, 2);
        }

        private void tbx_user_position_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_position, tbx_user_position_placeholder, 1);
            tbx_user_position.Focus();
        }

        private void tbx_user_phone_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_phone, tbx_user_phone_placeholder, 2);
        }

        private void tbx_user_phone_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_phone, tbx_user_phone_placeholder, 1);
            tbx_user_phone.Focus();
        }

        private void tbx_user_mail_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_mail, tbx_user_mail_placeholder, 2);
        }

        private void tbx_user_mail_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_mail, tbx_user_mail_placeholder, 1);
            tbx_user_mail.Focus();
        }

        private void tbx_user_login_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_login, tbx_user_login_placeholder, 2);
        }

        private void tbx_user_login_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_login, tbx_user_login_placeholder, 1);
            tbx_user_login.Focus();
        }

        private void tbx_user_password_LostFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_password, tbx_user_password_placeholder, 2);
        }

        private void tbx_user_password_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_user_password, tbx_user_password_placeholder, 1);
            tbx_user_password.Focus();
        }

        private void Original(System.Windows.Controls.TextBox org, System.Windows.Controls.TextBox place, int r)
        {
            switch (r)
            {
                case 1:
                    if (org.Text != "")
                    {
                        place.Visibility = Visibility.Collapsed;
                        org.Visibility = Visibility.Visible;
                    }
                    break;
                case 2:
                    if (string.IsNullOrEmpty(org.Text))
                    {
                        org.Visibility = Visibility.Collapsed;
                        place.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        SOI_Users _curUser = new SOI_Users();
        private void listUsers_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Checher();
        }

        private void Checher()
        {
            if (listUsers.SelectedItem != null)
            {
                _curUser = (SOI_Users)listUsers.SelectedItem;

                tbx_user_firstname.Text = _curUser.user_first_name;
                tbx_user_lastname.Text = _curUser.user_last_name;
                tbx_user_surname.Text = _curUser.user_surname;
                tbx_user_position.Text = _curUser.user_position;
                tbx_user_phone.Text = _curUser.user_phone;
                tbx_user_mail.Text = _curUser.user_mail;
                tbx_user_login.Text = _curUser.user_login;
                tbx_user_password.Text = _curUser.user_password;
                l_user_role.Content = AppConnect.model.SOI_User_Roles.First(x => x.id_user_role == _curUser.user_role_id).user_role_name;

                if(_curUser.user_role_id == 1 && current_user.user_role_id == 2)
                {
                    tbx_user_login.Text = null;
                    tbx_user_password.Text = null;

                    tbx_user_login.IsEnabled = false;
                    tbx_user_login.Visibility = Visibility.Collapsed;
                    
                    tbx_user_login_placeholder.IsEnabled = false;
                    tbx_user_login_placeholder.Visibility = Visibility.Collapsed;
                    
                    tbx_user_password.IsEnabled = false;
                    tbx_user_password.Visibility = Visibility.Collapsed;
                    
                    tbx_user_password_placeholder.IsEnabled = false;
                    tbx_user_password_placeholder.Visibility = Visibility.Collapsed;
                }
                else
                {
                    tbx_user_login.IsEnabled = true;
                    tbx_user_login.Visibility = Visibility.Visible;
                    
                    tbx_user_login_placeholder.IsEnabled = true;
                    tbx_user_login_placeholder.Visibility = Visibility.Visible;
                    
                    tbx_user_password.IsEnabled = true;
                    tbx_user_password.Visibility = Visibility.Visible;
                    
                    tbx_user_password_placeholder.IsEnabled = true;
                    tbx_user_password_placeholder.Visibility = Visibility.Visible;
                }    

                for (int i = 0; i < AppConnect.model.SOI_Users.Count(); i++)
                {
                    if (cbx_user_role.Items[i] == AppConnect.model.SOI_User_Roles.FirstOrDefault(x => x.id_user_role == _curUser.user_role_id))
                    {
                        cbx_user_role.SelectedIndex = i;
                        break;
                    }
                }

                Original(tbx_user_firstname, tbx_user_firstname_placeholder, 1);
                Original(tbx_user_lastname, tbx_user_lastname_placeholder, 1);
                Original(tbx_user_surname, tbx_user_surname_placeholder, 1);
                Original(tbx_user_position, tbx_user_position_placeholder, 1);
                Original(tbx_user_phone, tbx_user_phone_placeholder, 1);
                Original(tbx_user_mail, tbx_user_mail_placeholder, 1);
                Original(tbx_user_mail, tbx_user_mail_placeholder, 1);
                Original(tbx_user_login, tbx_user_login_placeholder, 1);
                Original(tbx_user_password, tbx_user_password_placeholder, 1);

            }
        }

        private void goAsset_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new ItemPage(current_user));
        }

        private void goReport_Click(object sender, RoutedEventArgs e)
        {
            //AppFrame.frameMain.Navigate(new ItemPage(current_user));
        }

        private void user_add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void user_red_Click(object sender, RoutedEventArgs e)
        {

        }

        private void user_del_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("", "", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    var delete_user_reports = AppConnect.model.SOI_Reports.Where(x => x.report_creator_user_id == _curUser.id_user).ToList();
                    for(int i = 0; i < delete_user_reports.Count; i++)
                    {

                    }

                    var delete_user_offs = AppConnect.model.SOI_Offs.Where(x => x.off_responsible_user_id == _curUser.id_user).ToList();
                    for (int i = 0; i < delete_user_offs.Count; i++)
                    {

                    }

                    var delete_user_owns = AppConnect.model.SOI_Owns.Where(x => x.own_user_id == _curUser.id_user).ToList();
                    for (int i = 0; i < delete_user_owns.Count; i++)
                    {

                    }

                    var delete_user_answers = AppConnect.model.SOI_Answers.Where(x => x.answer_user_id == _curUser.id_user).ToList();
                    for (int i = 0; i < delete_user_answers.Count; i++)
                    {

                    }

                    var delete_user_requests = AppConnect.model.SOI_Requests.Where(x => x.reguest_user_id == _curUser.id_user).ToList();
                    for (int i = 0; i < delete_user_requests.Count; i++)
                    {

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

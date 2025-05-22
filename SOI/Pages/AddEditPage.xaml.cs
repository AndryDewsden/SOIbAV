using SOI.AppicationData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
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
    public partial class AddEditPage : Page
    {
        private SOI_Assets current_asset = new SOI_Assets();
        private SOI_Owns current_own = new SOI_Owns();
        private SOI_Offs current_off = new SOI_Offs();
        SOI_Users current_user = new SOI_Users();
        public AddEditPage(SOI_Users current_user, SOI_Assets asset)
        {
            InitializeComponent();
            this.current_user = current_user;

            cbx_asset_location.Items.Add("");
            cbx_asset_owner.Items.Add("");
            cbx_asset_status.Items.Add("");
            cbx_asset_type.Items.Add("");

            for(int i = 0; i < AppConnect.model.SOI_Locations.ToList().Count; i++)
            {
                cbx_asset_location.Items.Add(AppConnect.model.SOI_Locations.ToList()[i]);
            }

            for (int i = 0; i < AppConnect.model.SOI_Users.ToList().Count; i++)
            {
                cbx_asset_owner.Items.Add(AppConnect.model.SOI_Users.ToList()[i]);
            }

            for (int i = 0; i < AppConnect.model.SOI_Asset_Statuses.ToList().Count; i++)
            {
                cbx_asset_status.Items.Add(AppConnect.model.SOI_Asset_Statuses.ToList()[i]);
            }

            for (int i = 0; i < AppConnect.model.SOI_Asset_Types.ToList().Count; i++)
            {
                cbx_asset_type.Items.Add(AppConnect.model.SOI_Asset_Types.ToList()[i]);
            }

            if (asset != null)
            {
                current_asset = asset;
                Title = "Редактирование";
                Red.IsEnabled = true;

                check(tbx_name_asset, tbx_name_asset_placeholder);
                check(tbx_asset_number, tbx_asset_number_placeholder);
                check(tbx_asset_comment, tbx_asset_comment_placeholder);

                var a = AppConnect.model.SOI_Owns.FirstOrDefault(x => x.own_asset_id == current_asset.id_asset);
                if (a != null)
                {
                    current_own = a;
                    var b = AppConnect.model.SOI_Offs.FirstOrDefault(x => x.off_asset_own_id == current_own.id_asset_own);
                    if (b != null) 
                    {
                        current_off = b;
                        OwnPanel.Visibility = Visibility.Visible;
                        OwnPanel.IsEnabled = true;

                        offPanel.Visibility = Visibility.Visible;
                        offPanel.IsEnabled = true;
                    }
                    else
                    {
                        OwnPanel.Visibility = Visibility.Visible;
                        OwnPanel.IsEnabled = true;

                        offPanel.Visibility = Visibility.Collapsed;
                        offPanel.IsEnabled = false;
                    }
                }
                else
                {
                    OwnPanel.Visibility = Visibility.Collapsed;
                    OwnPanel.IsEnabled = false;

                    offPanel.Visibility = Visibility.Collapsed;
                    offPanel.IsEnabled = false;
                }
            }
            else
            {
                Title = "Добавление";
                Red.IsEnabled = false;
                Red.Visibility = Visibility.Collapsed;
                dbx_asset_own_date.SelectedDate = DateTime.Now;
            }

            DataContext = current_asset;
        }

        private void Original(TextBox org, TextBox place)
        {
            place.Visibility = Visibility.Collapsed;
            org.Visibility = Visibility.Visible;
        }

        private void placeHolder(TextBox org, TextBox place)
        {
            if (string.IsNullOrEmpty(org.Text))
            {
                org.Visibility = Visibility.Collapsed;
                place.Visibility = Visibility.Visible;
            }
        }

        private void check(TextBox org, TextBox place)
        {
            if (org.Text == null)
            {
                placeHolder(org, place);
            }
            else
            {
                Original(org, place);
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (cbx_asset_location.SelectedIndex != 0 && cbx_asset_status.SelectedIndex != 0 && cbx_asset_type.SelectedIndex != 0 && tbx_name_asset.Text != "" && tbx_asset_number.Text != "")
            {
                if (cbx_asset_status.Text == "Свободно")
                {
                    addAsset();
                }
                else if (cbx_asset_status.Text == "В эксплуатации" && cbx_asset_owner.SelectedIndex != 0 && dbx_asset_own_date.Text != "")
                {
                    addOwner();
                }
                else if (cbx_asset_status.Text == "Списано" && cbx_asset_owner.SelectedIndex != 0 && dbx_asset_own_date.Text != "" && dbx_asset_off_date.Text != "" && cbx_asset_off_responsible_user.SelectedIndex != 0)
                {
                    addOff();
                }
                else
                {
                    MessageBox.Show("Заполните все необходимые поля!", "Уведомлениее", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Заполните все необходимые поля!", "Уведомлениее", MessageBoxButton.OK);
            }
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            if (cbx_asset_location.SelectedIndex != 0 && cbx_asset_status.SelectedIndex != 0 && cbx_asset_type.SelectedIndex != 0 && tbx_name_asset.Text != "" && tbx_asset_number.Text != "")
            {
                if(cbx_asset_status.Text == "Свободно")
                {
                    checkOwn();
                }
                else if (cbx_asset_status.Text == "В эксплуатации" && cbx_asset_owner.SelectedIndex != 0 && dbx_asset_own_date.Text != "")
                {
                    editOwner();
                }
                else if (cbx_asset_status.Text == "Списано" && cbx_asset_owner.SelectedIndex != 0 && dbx_asset_own_date.Text != "" && dbx_asset_off_date.Text != "" && cbx_asset_off_responsible_user.SelectedIndex != 0)
                {
                    editOff();
                }
                else
                {
                    MessageBox.Show("Заполните все необходимые поля!", "Уведомлениее", MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Заполните все необходимые поля!", "Уведомлениее", MessageBoxButton.OK);
            }
        }

        private void addAsset()
        {
            var res = MessageBox.Show("Вы действительно хотите добавить это оборуовние?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    current_asset.asset_name = tbx_name_asset.Text;
                    current_asset.asset_serial_number = tbx_asset_number.Text;
                    if (tbx_asset_comment.Text != "")
                    {
                        current_asset.asset_comment = tbx_asset_comment.Text;
                    }

                    current_asset.asset_location_id = AppConnect.model.SOI_Locations.First(x => x.location_number == cbx_asset_location.Text).id_location;

                    current_asset.asset_status_id = AppConnect.model.SOI_Asset_Statuses.First(x => x.asset_status_name == cbx_asset_status.Text).id_asset_status;

                    current_asset.asset_type_id = AppConnect.model.SOI_Asset_Types.First(x => x.asset_type == cbx_asset_type.Text).id_asset_type;

                    AppConnect.model.SOI_Assets.Add(current_asset);
                    AppConnect.model.SaveChanges();
                    MessageBox.Show("Данные успешно добавленны", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.frameMain.Navigate(new ItemPage(current_user));
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void addOwner()
        {
            try
            {
                addAsset();
                current_own.own_asset_id = current_asset.id_asset;
                current_own.own_date = (DateTime)dbx_asset_own_date.SelectedDate;
                current_own.own_user_id = AppConnect.model.SOI_Users.First(x => x.user_login == cbx_asset_owner.Text).id_user;
                AppConnect.model.SOI_Owns.Add(current_own);
                AppConnect.model.SaveChanges();
                MessageBox.Show("Данные успешно добавленны", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frameMain.Navigate(new ItemPage(current_user));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void addOff()
        {
            addAsset();
            addOwner();
            current_off.off_asset_own_id = current_own.id_asset_own;
            current_off.off_date = (DateTime)dbx_asset_off_date.SelectedDate;
            current_off.off_reason = tbx_off_reason.Text;
            current_off.off_document_link = tbx_off_link.Text;
            current_off.off_responsible_user_id = AppConnect.model.SOI_Users.First(x => x.user_login == cbx_asset_off_responsible_user.Text).id_user;

        }
        private void editAsset()
        {
                var res = MessageBox.Show("Вы действительно хотите редактировать это оборудовние?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                {
                try
                {
                    current_asset.asset_name = tbx_name_asset.Text;
                    current_asset.asset_serial_number = tbx_asset_number.Text;
                    if (tbx_asset_comment.Text != "")
                    {
                        current_asset.asset_comment = tbx_asset_comment.Text;
                    }

                    current_asset.asset_location_id = AppConnect.model.SOI_Locations.FirstOrDefault(x => x.location_number == cbx_asset_location.Text).id_location;

                    current_asset.asset_status_id = AppConnect.model.SOI_Asset_Statuses.FirstOrDefault(x => x.asset_status_name == cbx_asset_status.Text).id_asset_status;

                    current_asset.asset_type_id = AppConnect.model.SOI_Asset_Types.FirstOrDefault(x => x.asset_type == cbx_asset_type.Text).id_asset_type;

                    AppConnect.model.SaveChanges();
                    MessageBox.Show("Данные успешно редактированы", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.frameMain.Navigate(new ItemPage(current_user));
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void editOwner()
        {

        }

        private void editOff()
        {

        }

        private void checkOwn()
        {
            var a = AppConnect.model.SOI_Owns.FirstOrDefault(x => x.own_asset_id == current_asset.id_asset);
            if (a != null && cbx_asset_status.Text == "Свободно")
            {
                current_own = a;
                var b = AppConnect.model.SOI_Offs.FirstOrDefault(x => x.off_asset_own_id == current_own.id_asset_own);
                if (b != null && (cbx_asset_status.Text == "Свободно" || cbx_asset_status.Text == "В эксплуатации"))
                {
                    current_off = b;
                    var res = MessageBox.Show("Изменение статуса приведёт к удалению инфрмации о владении и списании оборудования. Продолжить?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.Yes)
                    {
                        try
                        {
                            AppConnect.model.SOI_Offs.Remove(current_off);
                            AppConnect.model.SOI_Owns.Remove(current_own);
                            AppConnect.model.SaveChanges();
                            editAsset();
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    var res = MessageBox.Show("Изменение статуса приведёт к удалению инфрмации о владении оборудованием. Продолжить?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (res == MessageBoxResult.Yes)
                    {
                        try
                        {
                            AppConnect.model.SOI_Owns.Remove(current_own);
                            AppConnect.model.SaveChanges();
                            editAsset();
                        }

                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            else
            {
                editAsset();
            }
        }

        private void list_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new ItemPage(current_user));
        }

        private void goBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        private void tbx_name_asset_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_name_asset, tbx_name_asset_placeholder);
            tbx_name_asset.Focus();
        }

        private void tbx_name_asset_LostFocus(object sender, RoutedEventArgs e)
        {
            check(tbx_name_asset, tbx_name_asset_placeholder);
            placeHolder(tbx_name_asset, tbx_name_asset_placeholder);
        }

        private void tbx_asset_number_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_asset_number, tbx_asset_number_placeholder);
            tbx_asset_number.Focus();
        }

        private void tbx_asset_number_LostFocus(object sender, RoutedEventArgs e)
        {
            check(tbx_asset_number, tbx_asset_number_placeholder);
            placeHolder(tbx_asset_number, tbx_asset_number_placeholder);
        }

        private void tbx_asset_comment_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_asset_comment, tbx_asset_comment_placeholder);
            tbx_asset_comment.Focus();
        }

        private void tbx_asset_comment_LostFocus(object sender, RoutedEventArgs e)
        {
            check(tbx_asset_comment, tbx_asset_comment_placeholder);
            placeHolder(tbx_asset_comment, tbx_asset_comment_placeholder);
        }

        private void cbx_asset_status_DropDownClosed(object sender, EventArgs e)
        {
            switch(cbx_asset_status.Text)
            {
                case "Списано":
                    OwnPanel.Visibility = Visibility.Visible;
                    OwnPanel.IsEnabled = true;

                    offPanel.Visibility = Visibility.Visible;
                    offPanel.IsEnabled = true;
                    break;
                case "В эксплуатации":
                    OwnPanel.Visibility = Visibility.Visible;
                    OwnPanel.IsEnabled = true;

                    offPanel.Visibility = Visibility.Collapsed;
                    offPanel.IsEnabled = false;
                    break;
                case "Свободно":
                    OwnPanel.Visibility = Visibility.Collapsed;
                    OwnPanel.IsEnabled = false;

                    offPanel.Visibility = Visibility.Collapsed;
                    offPanel.IsEnabled = false;
                    break;
            }
        }
    }
}
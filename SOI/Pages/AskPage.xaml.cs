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
    public partial class AskPage : Page
    {
        SOI_Users current_user = new SOI_Users();
        SOI_Requests current_request = new SOI_Requests();
        SOI_Answers current_answer = new SOI_Answers();
        SOI_Assets current_asset = new SOI_Assets();
        public AskPage(SOI_Users user)
        {
            InitializeComponent();
            current_user = user;

            listRequest.ItemsSource = FindRequests();

            ClosePanel(stp_admin_request);
            ClosePanel(stp_user_request_look);
            ClosePanel(stp_request_look);
            ClosePanel(stp_answer);

            OpenPanel(stp_user_request_create);
            OpenPanel(stp_request_create);

            cbx_asset_type.Items.Add("");

            var a = AppConnect.model.SOI_Asset_Types.ToList();
            for (int i = 0; i < a.Count; i++)
            {
                cbx_asset_type.Items.Add(a[i]);
            }
        }

        //
        private void ClearFields()
        {
            cbx_asset_type.SelectedIndex = 0;
            tbx_discription.Text = string.Empty;
        }

        //
        SOI_Requests[] FindRequests()
        {
            if (current_user.user_role_id == 3)
            {
                var pro = AppConnect.model.SOI_Requests.Where(x => x.reguest_user_id == current_user.id_user).ToList();
                return pro.ToArray();
            }
            else
            {
                var pro = AppConnect.model.SOI_Requests.ToList();
                return pro.ToArray();
            }
        }

        SOI_Assets[] FindAssets()
        {
            var pro = AppConnect.model.SOI_Assets.Where(x => (x.asset_status_id != 2 || x.asset_status_id != 3) && x.asset_type_id == current_request.request_asset_type_id).ToList();
            return pro.ToArray();
        }

        //Показ выбранной заявки
        private void listRequest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            stp_request_create.IsEnabled = false;
            stp_request_create.Visibility = Visibility.Collapsed;

            stp_request_look.IsEnabled = true;
            stp_request_look.Visibility = Visibility.Visible;

            if (listRequest.SelectedItem != null)
            {
                switch (current_user.user_role_id)
                {
                    case 1:
                        stp_admin_request.IsEnabled = true;
                        stp_admin_request.Visibility = Visibility.Visible;

                        stp_user_request_create.IsEnabled = false;
                        stp_user_request_create.Visibility = Visibility.Collapsed;

                        stp_user_request_look.IsEnabled = false;
                        stp_user_request_look.Visibility = Visibility.Collapsed;
                        break;
                    case 2:
                        stp_admin_request.IsEnabled = true;
                        stp_admin_request.Visibility = Visibility.Visible;

                        stp_user_request_create.IsEnabled = false;
                        stp_user_request_create.Visibility = Visibility.Collapsed;

                        stp_user_request_look.IsEnabled = false;
                        stp_user_request_look.Visibility = Visibility.Collapsed;
                        break;
                    case 3:
                        stp_admin_request.IsEnabled = false;
                        stp_admin_request.Visibility = Visibility.Collapsed;

                        stp_user_request_create.IsEnabled = false;
                        stp_user_request_create.Visibility = Visibility.Collapsed;

                        stp_user_request_look.IsEnabled = true;
                        stp_user_request_look.Visibility = Visibility.Visible;
                        break;
                    default:

                        break;
                }

                current_request = (SOI_Requests)listRequest.SelectedItem;

                l_request_number.Content = $"{current_request.id_request}";
                l_user_creator.Content = $"{AppConnect.model.SOI_Users.First(x => x.id_user == current_request.reguest_user_id).user_first_name}";
                l_date_creation.Content = $"{current_request.request_date}";
                l_status.Content = $"{AppConnect.model.SOI_Reques_Statuses.First(x => x.id_request_status == current_request.request_status_id).request_status_name}";
                l_asset_type.Content = $"{AppConnect.model.SOI_Asset_Types.First(x => x.id_asset_type == current_request.request_asset_type_id).asset_type}";
                l_comment.Content = $"{current_request.request_comment}";

                current_answer = AppConnect.model.SOI_Answers.FirstOrDefault(x => x.answer_request_id == current_request.id_request);

                if (current_answer != null)
                {
                    l_answer_type.Content = "Ваша заявка принята!";
                    l_answer_message.Content = $"Принял заявку: {AppConnect.model.SOI_Users.First(x => x.id_user == current_answer.answer_user_id).user_first_name}\n Дата принятия: {current_answer.answer_date}\n Наименование: {AppConnect.model.SOI_Assets.First(x => x.id_asset == current_answer.answer_asset_id).asset_name}\n Серийный номер: {AppConnect.model.SOI_Assets.First(x => x.id_asset == current_answer.answer_asset_id).asset_serial_number}";
                }
                else
                {
                    switch (current_request.request_status_id)
                    {
                        case 1:
                            stp_answer.IsEnabled = false;
                            stp_answer.Visibility = Visibility.Collapsed;
                            break;
                        case 2:
                            stp_answer.IsEnabled = true;
                            stp_answer.Visibility = Visibility.Visible;

                            l_answer_type.Content = "Ваша заявка принята!";
                            l_answer_message.Content = $"Принял заявку: {AppConnect.model.SOI_Users.First(x => x.id_user == current_answer.answer_user_id).user_first_name}\n Дата принятия: {current_answer.answer_date}\n Наименование: {AppConnect.model.SOI_Assets.First(x => x.id_asset == current_answer.answer_asset_id).asset_name}\n Серийный номер: {AppConnect.model.SOI_Assets.First(x => x.id_asset == current_answer.answer_asset_id).asset_serial_number}";
                            break;
                        case 3:
                            stp_answer.IsEnabled = true;
                            stp_answer.Visibility = Visibility.Visible;

                            l_answer_type.Content = "Ваша заявка отклоненна";
                            l_answer_message.Content = "Ваши требования были отклоненны";
                            break;
                        case 4:
                            stp_answer.IsEnabled = true;
                            stp_answer.Visibility = Visibility.Visible;

                            l_answer_type.Content = "Ваша заявка замороженна";
                            l_answer_message.Content = "Ожидайте пересмотра заявки.";
                            break;
                        default:
                            stp_answer.IsEnabled = false;
                            stp_answer.Visibility = Visibility.Collapsed;
                            break;
                    }
                }
            }
        }

        private void tbx_discription_LostFocus(object sender, RoutedEventArgs e)
        {
            check(tbx_discription, tbx_discription_placeholder);
            placeHolder(tbx_discription, tbx_discription_placeholder);
        }

        private void tbx_discription_placeholder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(tbx_discription, tbx_discription_placeholder);
            tbx_discription.Focus();
        }

        //
        private void Original(TextBox org, TextBox place)
        {
            place.Visibility = Visibility.Collapsed;
            org.Visibility = Visibility.Visible;
        }

        //
        private void placeHolder(TextBox org, TextBox place)
        {
            if (string.IsNullOrEmpty(org.Text))
            {
                org.Visibility = Visibility.Collapsed;
                place.Visibility = Visibility.Visible;
            }
        }

        //
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

        private void bt_clear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClosePanel(StackPanel closed_panel)
        {
            closed_panel.IsEnabled = false;
            closed_panel.Visibility = Visibility.Collapsed;
        }

        private void OpenPanel(StackPanel open_panel)
        {
            open_panel.IsEnabled = true;
            open_panel.Visibility = Visibility.Visible;
        }

        //Создание заявки
        private void bt_send_request_Click(object sender, RoutedEventArgs e)
        {
            if(cbx_asset_type.SelectedIndex != 0)
            {
                try
                {
                    current_request.reguest_user_id = current_user.id_user;
                    current_request.request_date = DateTime.Now;
                    current_request.request_status_id = 1;
                    if(tbx_discription.Text != "")
                    {
                        current_request.request_comment = tbx_discription.Text;
                    }
                    current_request.request_asset_type_id = cbx_asset_type.SelectedIndex;

                    AppConnect.model.SOI_Requests.Add(current_request);
                    AppConnect.model.SaveChanges();
                    MessageBox.Show($"Ваша заявка успешно отправлена!\nОжидайте ответа менеджера или администратора.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearFields();
                    listRequest.ItemsSource = FindRequests();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            listRequest.ItemsSource = FindRequests();
            listRequest.SelectedItem = null;
        }

        //Переключение на поле создания заявки
        private void bt_user_create_Click(object sender, RoutedEventArgs e)
        {
            current_request = null;

            ClosePanel(stp_admin_request);
            ClosePanel(stp_user_request_look);
            ClosePanel(stp_request_look);

            OpenPanel(stp_user_request_create);
            OpenPanel(stp_request_create);

            ClearFields();
            
            listRequest.ItemsSource = FindRequests();
            listRequest.SelectedItem = null;
        }

        //Переключение на панель создания заявки
        private void bt_admin_create_Click(object sender, RoutedEventArgs e)
        {
            current_request = null;

            ClosePanel(stp_admin_request);
            ClosePanel(stp_user_request_look);
            ClosePanel(stp_request_look);

            OpenPanel(stp_user_request_create);
            OpenPanel(stp_request_create);

            listRequest.SelectedItem = null;
            ClearFields();
            listRequest.ItemsSource = FindRequests();
        }

        //Переход на панель принятия заявки
        private void bt_accept_request_Click(object sender, RoutedEventArgs e)
        {
            if(current_request != null)
            {
                ClosePanel(stp_requestList);
                ClosePanel(stp_admin_request);

                OpenPanel(stp_asset_char);
                OpenPanel(stp_accept);
                OpenPanel(stp_assetList);

                listAsset.ItemsSource = FindAssets();
            }
        }

        //Отклонение заявки
        private void bt_denied_request_Click(object sender, RoutedEventArgs e)
        {
            if(current_request != null)
            {
                try
                {
                    current_request.request_status_id = 3;
                    AppConnect.model.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            listRequest.ItemsSource = FindRequests();
            listRequest.SelectedItem = null;
        }

        //Заморозка заявки
        private void bt_froze_request_Click(object sender, RoutedEventArgs e)
        {
            if (current_request != null)
            {
                try
                {
                    current_request.request_status_id = 4;
                    AppConnect.model.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            listRequest.ItemsSource = FindRequests();
            listRequest.SelectedItem = null;
        }

        //Удаление заявки
        private void bt_delete_Click(object sender, RoutedEventArgs e)
        {
            var res = MessageBox.Show("Вы действительно хотите удалить свою заявку?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    SOI_Answers delete_answer = AppConnect.model.SOI_Answers.FirstOrDefault(x => x.answer_request_id == current_request.id_request);
                    if (delete_answer != null)
                    {
                        AppConnect.model.SOI_Answers.Remove(delete_answer);
                    }
                    AppConnect.model.SOI_Requests.Remove(current_request);
                    AppConnect.model.SaveChanges();
                    MessageBox.Show("Заявка успешно удаленна.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            listRequest.ItemsSource = FindRequests();
            listRequest.SelectedItem = null;
        }

        private void listAsset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(listAsset.SelectedItem != null)
            {
                current_asset = (SOI_Assets)listAsset.SelectedItem;
                l_asset_characteristics.Content = $"";

                bt_accept_asset.IsEnabled = true;

                OpenPanel(stp_asset_char);
            }
            else
            {
                bt_accept_asset.IsEnabled = false;
                ClosePanel(stp_asset_char);
            }
        }

        private void bt_accept_asset_Click(object sender, RoutedEventArgs e)
        {
            listRequest.ItemsSource = FindRequests();
            listRequest.SelectedItem = null;
        }

        private void bt_refuse_Click(object sender, RoutedEventArgs e)
        {
            OpenPanel(stp_requestList);
            OpenPanel(stp_admin_request);

            ClosePanel(stp_asset_char);
            ClosePanel(stp_accept);
            ClosePanel(stp_assetList);

            listRequest.ItemsSource = FindRequests();
            listRequest.SelectedItem = null;
        }
    }
}

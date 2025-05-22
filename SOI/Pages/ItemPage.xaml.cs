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
    public partial class ItemPage : Page
    {
        SOI_Users use = new SOI_Users();
        public ItemPage(SOI_Users user)
        {
            InitializeComponent();
            use = user;

            userDisplay.Content = use.user_first_name;

            switch (user.user_role_id)
            {
                case 1:
                    //админ
                    addConMenu.IsEnabled = true;
                    editConMenu.IsEnabled = true;
                    delConMenu.IsEnabled = true;

                    addButton.IsEnabled = true;
                    editButton.IsEnabled = true;
                    delButton.IsEnabled = true;
                    break;
                case 3:
                    //пользователь
                    addConMenu.IsEnabled = false;
                    editConMenu.IsEnabled = false;
                    delConMenu.IsEnabled = false;
                    addConMenu.Visibility = Visibility.Collapsed;
                    editConMenu.Visibility = Visibility.Collapsed;
                    delConMenu.Visibility = Visibility.Collapsed;

                    addButton.IsEnabled = false;
                    editButton.IsEnabled = false;
                    delButton.IsEnabled = false;
                    addButton.Visibility = Visibility.Collapsed;
                    editButton.Visibility = Visibility.Collapsed;
                    delButton.Visibility = Visibility.Collapsed;

                    userDisplay.IsEnabled = false;
                    userDisplay.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    //менеджер
                    addConMenu.IsEnabled = true;
                    editConMenu.IsEnabled = true;
                    delConMenu.IsEnabled = true;

                    addButton.IsEnabled = true;
                    editButton.IsEnabled = true;
                    delButton.IsEnabled = true;
                    break;
                default:
                    MessageBox.Show("Произошла какае-то ощибка с данными пользователя. Вас перекинут на страницу авторизации.", "О-оу", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }

            //сортировшик
            Sorter.ItemsSource = new Sort[]
            {
                new Sort { Name_sorter = "нет" },
                new Sort { Name_sorter = "по увеличению" },
                new Sort { Name_sorter = "по уменьшению" }
            };
            Sorter.SelectedIndex = 0;

            //фильтр
            Filter.ItemsSource = new Filt[]
            {
                new Filt { Name_filter = "Всё оборудование" },
                new Filt { Name_filter = "Рабочее оборудование" },
                new Filt { Name_filter = "Только свободное" },
                new Filt { Name_filter = "Эксплуатируемое" },
                new Filt { Name_filter = "Списаное оборудование" }
            };
            Filter.SelectedIndex = 1;

            //список
            listAssets.ItemsSource = FindAsset();
        }

        public class Sort
        {
            public string Name_sorter { get; set; } = "";
            public override string ToString() => $"{Name_sorter}";
        }

        public class Filt
        {
            public string Name_filter { get; set; } = "";
            public override string ToString() => $"{Name_filter}";
        }

        //составление списка
        SOI_Assets[] FindAsset()
        {
            var assets_list = AppConnect.model.SOI_Assets.ToList();
            var asset_count = assets_list;

            if (Searcher.Text != null)
            {
                assets_list = assets_list.Where(x => x.asset_serial_number.ToLower().Contains(Searcher.Text.ToLower()) || x.asset_name.ToLower().Contains(Searcher.Text.ToLower())).ToList();
            }

            if (Filter.SelectedIndex > 0)
            {
                switch (Filter.SelectedIndex)
                {
                    case 1:
                        assets_list = assets_list.Where(x => x.asset_status_id == 2 || x.asset_status_id == 3).ToList();
                        break;
                    case 2:
                        assets_list = assets_list.Where(x => x.asset_status_id == 3).ToList();
                        break;
                    case 3:
                        assets_list = assets_list.Where(x => x.asset_status_id == 2).ToList();
                        break;
                    case 4:
                        assets_list = assets_list.Where(x => x.asset_status_id == 1).ToList();
                        break;
                }
            }

            //if (Sorter.SelectedIndex > 0)
            //{
            //    switch (Sorter.SelectedIndex)
            //    {
            //        case 1:
            //            products = products.OrderBy(x => x.employee_age).ToList();
            //            break;
            //        case 2:
            //            products = products.OrderByDescending(x => x.employee_age).ToList();
            //            break;
            //    }
            //}

            if (assets_list.Count > 0)
            {
                Counter.Content = $"Найдено {assets_list.Count} из {asset_count.Count} оборудования.";
            }
            else
            {
                Counter.Content = "Информация о оборудовании не найдена.";
            }

            return assets_list.ToArray();
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
        private void Searcher_LostFocus(object sender, RoutedEventArgs e)
        {
            check(Searcher, SearcherPlaceHolder);
            placeHolder(Searcher, SearcherPlaceHolder);
        }

        private void SearcherPlaceHolder_GotFocus(object sender, RoutedEventArgs e)
        {
            Original(Searcher, SearcherPlaceHolder);
            Searcher.Focus();
        }

        private void Searcher_TextChanged(object sender, TextChangedEventArgs e)
        {
            listAssets.ItemsSource = FindAsset();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listAssets.ItemsSource = FindAsset();
        }

        private void Sorter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listAssets.ItemsSource = FindAsset();
        }

        private void userDisplay_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new UserPage(use));
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            addFun();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            editFun();
        }

        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            spiFun();
        }

        //Функция перехода на страницу добавления оборудования
        private void addFun()
        {
            AppFrame.frameMain.Navigate(new AddEditPage(use, null));
        }

        //Функция перехода на страницу редактирования выбранного оборудования
        private void editFun()
        {
            SOI_Assets red = (SOI_Assets)listAssets.SelectedItem;
            AppFrame.frameMain.Navigate(new AddEditPage(use, red));
        }


        private void delFun()
        {
            if ((SOI_Assets)listAssets.SelectedItem != null)
            {
                var deleleAsset = (SOI_Assets)listAssets.SelectedItem;
                SOI_Owns deleteOwner = AppConnect.model.SOI_Owns.FirstOrDefault(x => x.own_asset_id == deleleAsset.id_asset);
                SOI_Offs deleteOff = AppConnect.model.SOI_Offs.FirstOrDefault(x => x.off_asset_own_id == deleteOwner.id_asset_own);
                
                var res = MessageBox.Show($"Вы действительно хотите удалить это оборудование? Данные о владении и списании оборудования также будут удаленны. Продолжить?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.model.SOI_Offs.Remove(deleteOff);
                        AppConnect.model.SOI_Owns.Remove(deleteOwner);
                        AppConnect.model.SOI_Assets.Remove(deleleAsset);
                        AppConnect.model.SaveChanges();
                        listAssets.ItemsSource = FindAsset();
                        MessageBox.Show("Данные успешно удалены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        //Функция перехода на страницу списания выбранного оборудования
        private void spiFun()
        {
            if ((SOI_Assets)listAssets.SelectedItem != null)
            {
                var del = (SOI_Assets)listAssets.SelectedItem;
                AppFrame.frameMain.Navigate(new offPage(use, del));
            }
        }

        private void addConMenu_Click(object sender, RoutedEventArgs e)
        {
            addFun();
        }

        private void editConMenu_Click(object sender, RoutedEventArgs e)
        {
            editFun();
        }

        private void spiConMenu_Click(object sender, RoutedEventArgs e)
        {
            spiFun();
        }

        private void delConMenu_Click(object sender, RoutedEventArgs e)
        {
            delFun();
        }

        private void bt_go_request_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new AskPage(use));
        }
    }
}

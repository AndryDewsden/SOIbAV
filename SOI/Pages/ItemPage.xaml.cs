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
        public ItemPage()
        {
            InitializeComponent();
            LoadEquipment();
        }

        private void LoadEquipment()
        {
            //listEquipment.ItemsSource = AppConnect.model1db.Equipment.ToList();
        }

        private void Searcher_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEquipment();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterEquipment();
        }

        private void Sorter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterEquipment();
        }

        private void FilterEquipment()
        {
            //var equipment = AppConnect.model1db.Equipment.ToList();
            if (!string.IsNullOrEmpty(Searcher.Text))
            {
                //equipment = equipment.Where(x => x.Name.ToLower().Contains(Searcher.Text.ToLower())).ToList();
            }

            // Apply sorting
            if (Sorter.SelectedIndex == 1) // Ascending
            {
                //equipment = equipment.OrderBy(x => x.Name).ToList();
            }
            else if (Sorter.SelectedIndex == 2) // Descending
            {
                //equipment = equipment.OrderByDescending(x => x.Name).ToList();
            }

            //listEquipment.ItemsSource = equipment;
            //Counter.Content = $"Найдено {equipment.Count} единиц оборудования.";
        }
    }
}

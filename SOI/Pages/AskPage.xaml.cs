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
        public AskPage()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserNameTextBox.Text;
            string equipmentName = EquipmentNameTextBox.Text;
            string description = DescriptionTextBox.Text;

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(equipmentName))
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveRequest(userName, equipmentName, description);

            MessageBox.Show("Заявка успешно отправлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            ClearFields();
        }

        private void SaveRequest(string userName, string equipmentName, string description)
        {

        }

        private void ClearFields()
        {
            UserNameTextBox.Clear();
            EquipmentNameTextBox.Clear();
            DescriptionTextBox.Clear();
        }
    }
}

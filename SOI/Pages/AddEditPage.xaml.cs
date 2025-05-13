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
    public partial class AddEditPage : Page
    {
        //private Equipment_Cursach _currentEquipment;

        //public AddPage(Equipment_Cursach equipment)
        public AddEditPage()
        {
            InitializeComponent();
            //_currentEquipment = equipment ?? new Equipment_Cursach();
            //DataContext = _currentEquipment;

            //// Заполнение комбобоксов
            //LoadData();
            //if (equipment != null)
            //{
            //    FillFields(equipment);
            //}
        }

        //private void LoadData()
        //{
        //    // Заполнение списка спецификаций
        //    foreach (var spec in AppConnect.model1db.Specifications_Cursach.ToList())
        //    {
        //        specificationCombo.Items.Add(spec.specification_name);
        //    }

        //    // Заполнение списка поставщиков
        //    foreach (var supplier in AppConnect.model1db.Suppliers_Cursach.ToList())
        //    {
        //        supplierCombo.Items.Add(supplier.supplier_name);
        //    }
        //}

        //private void FillFields(Equipment_Cursach equipment)
        //{
        //    nameEquipment.Text = equipment.equipment_name;
        //    quantityBox.Text = equipment.quantity.ToString();
        //    priceBox.Text = equipment.price.ToString();
        //    specificationCombo.SelectedItem = AppConnect.model1db.Specifications_Cursach
        //        .FirstOrDefault(s => s.id_specification == equipment.equipment_id_specification)?.specification_name;
        //    supplierCombo.SelectedItem = AppConnect.model1db.Suppliers_Cursach
        //        .FirstOrDefault(s => s.id_supplier == equipment.equipment_id_supplier)?.supplier_name;
        //}

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //if (ValidateFields())
            //{
            //if (_currentEquipment.id_equipment == 0) // Добавление
            //{
            //_currentEquipment.equipment_name = nameEquipment.Text;
            //_currentEquipment.quantity = Convert.ToInt32(quantityBox.Text);
            //_currentEquipment.price = Convert.ToDecimal(priceBox.Text);
            //_currentEquipment.equipment_id_specification = AppConnect.model1db.Specifications_Cursach
            //    .FirstOrDefault(s => s.specification_name == specificationCombo.Text)?.id_specification;
            //_currentEquipment.equipment_id_supplier = AppConnect.model1db.Suppliers_Cursach
            //    .FirstOrDefault(s => s.supplier_name == supplierCombo.Text)?.id_supplier;

            //AppConnect.model1db.Equipment_Cursach.Add(_currentEquipment);
            //}
            //else // Редактирование
            //{
            //var existingEquipment = AppConnect.model1db.Equipment_Cursach.Find(_currentEquipment.id_equipment);
            //if (existingEquipment != null)
            //{
            //    existingEquipment.equipment_name = nameEquipment.Text;
            //    existingEquipment.quantity = Convert.ToInt                        existingEquipment.quantity = Convert.ToInt32(quantityBox.Text);
            //    existingEquipment.price = Convert.ToDecimal(priceBox.Text);
            //    existingEquipment.equipment_id_specification = AppConnect.model1db.Specifications_Cursach
            //        .FirstOrDefault(s => s.specification_name == specificationCombo.Text)?.id_specification;
            //    existingEquipment.equipment_id_supplier = AppConnect.model1db.Suppliers_Cursach
            //        .FirstOrDefault(s => s.supplier_name == supplierCombo.Text)?.id_supplier;
            //}
            //}

            try
            {
                //AppConnect.model1db.SaveChanges();
                MessageBox.Show("Данные успешно сохранены", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                //AppFrame.frameMain.Navigate(new EquipmentListPage()); // Переход на страницу списка оборудования
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //else
        //{
        //    MessageBox.Show("Пожалуйста, заполните все необходимые поля!", "Уведомление", MessageBoxButton.OK);
        //}
    }

    //private bool ValidateFields()
    //{
    //    return !string.IsNullOrWhiteSpace(nameEquipment.Text) &&
    //           !string.IsNullOrWhiteSpace(quantityBox.Text) &&
    //           !string.IsNullOrWhiteSpace(priceBox.Text) &&
    //           specificationCombo.SelectedIndex > 0 &&
    //           supplierCombo.SelectedIndex > 0;
    //}

    //private void GoBack_Click(object sender, RoutedEventArgs e)
    //{
    //    AppFrame.frameMain.GoBack();
    //}

    //private void quantityBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    //{
    //    CheckIsNumeric(e);
    //}

    //private void priceBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    //{
    //    CheckIsNumeric(e);
    //}

    //private void CheckIsNumeric(TextCompositionEventArgs e)
    //{
    //    e.Handled = !int.TryParse(e.Text, out _);
    //}
}
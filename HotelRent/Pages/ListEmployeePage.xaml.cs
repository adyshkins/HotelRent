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
using HotelRent.EF;
using HotelRent.ClassHelper;

namespace HotelRent.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListEmployeePage.xaml
    /// </summary>
    public partial class ListEmployeePage : Page
    {
        List<Employee> employees = new List<Employee>();
        List<string> listSort = new List<string>()
        { 
            "По умолчанию",
            "По фамилии",
            "По имени",
            "По должности"
        };

        public ListEmployeePage()
        {
            InitializeComponent();

            CmbSort.ItemsSource = listSort;
            CmbSort.SelectedIndex = 0;
            GetList();
        }

        private void GetList()
        {
            // Получаем весь список
            employees = AppData.Context.Employee.ToList();

            // Поиск
            employees = employees.
                Where(i => i.LastName.ToLower().Contains(TxbSearch.Text.ToLower())
                || i.FirstName.ToLower().Contains(TxbSearch.Text.ToLower())).ToList();
            // Сортировка

            int selectedItemSort = CmbSort.SelectedIndex;
            switch (selectedItemSort)
            {
                case 0: 
                    employees = employees.OrderBy(i => i.IdEmployee).ToList();    
                    break;

                case 1:
                    employees = employees.OrderBy(i => i.LastName).ToList();
                    break;

                case 2:
                    employees = employees.OrderBy(i => i.FirstName).ToList();
                    break;

                case 3:
                    employees = employees.OrderBy(i => i.IdRole).ToList();
                    break;

                default:
                    employees = employees.OrderBy(i => i.IdEmployee).ToList();
                    break;
            }

            // Выгрузка в листвью
            lvEmployee.ItemsSource = employees;
        }

        private void DeleteItem()
        {
            if (lvEmployee.SelectedItem is Employee)
            {
                var result = MessageBox.Show("Удалить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes)
                {
                    return;
                }

                var empDel = lvEmployee.SelectedItem as Employee;
                AppData.Context.Employee.Remove(empDel);
                AppData.Context.SaveChanges();
                MessageBox.Show("Пользователь удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                GetList();
            }
            else
            {
                MessageBox.Show("Ничего не выбрано", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.NavigateClass.frame.Navigate(new Pages.AddEditEmployeePage());
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteItem();
        }

        private void lvEmployee_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            DeleteItem();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvEmployee.SelectedItem is Employee)
            {
                var result = MessageBox.Show("Изменить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes)
                {
                    return;
                }
                var empEdit = lvEmployee.SelectedItem as Employee;

                ClassHelper.NavigateClass.frame.Navigate(new Pages.AddEditEmployeePage(empEdit));


            }


        }
        private void TxbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetList();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GetList();
        }
    }
}

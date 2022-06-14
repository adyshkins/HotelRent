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

namespace HotelRent.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditEmployeePage.xaml
    /// </summary>
    public partial class AddEditEmployeePage : Page
    {

        private bool isEdit = false;
        private EF.Employee editEmployee;
        
        public AddEditEmployeePage()
        {
            InitializeComponent();

            CmbRole.ItemsSource = ClassHelper.AppData.Context.Role.ToList();
            CmbRole.DisplayMemberPath = "NameRole";
            CmbRole.SelectedIndex = 0;
        }

        public AddEditEmployeePage(EF.Employee employee)
        {
            editEmployee = employee;
            InitializeComponent();
            TbTitle.Text = "Изменение работника";


            TxbLastName.Text = employee.LastName;
            TxbFirstName.Text = employee.FirstName;
            TxbEmail.Text = employee.Email; 
            TxbPhone.Text = employee.Phone;
            TxbLogin.Text = employee.Login;
            PswPassword.Password = employee.Password;


            CmbRole.ItemsSource = ClassHelper.AppData.Context.Role.ToList();
            CmbRole.DisplayMemberPath = "NameRole";
            CmbRole.SelectedIndex = employee.IdRole - 1;

            isEdit = true;
        }


            private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            ClassHelper.NavigateClass.frame.GoBack();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(TxbLastName.Text))
            {
                MessageBox.Show("Поле Фамилия не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(TxbFirstName.Text))
            {
                MessageBox.Show("Поле Имя не должно быть пустым", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            if (isEdit)
            {
                editEmployee.LastName = TxbLastName.Text;
                editEmployee.FirstName = TxbFirstName.Text;
                editEmployee.Login = TxbLogin.Text;
                editEmployee.Password = PswPassword.Password;
                editEmployee.Phone = TxbPhone.Text;
                editEmployee.Email = TxbEmail.Text;
                editEmployee.IdRole = CmbRole.SelectedIndex + 1;

                ClassHelper.AppData.Context.SaveChanges();

                MessageBox.Show("Данные успешно изменены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                ClassHelper.NavigateClass.frame.Navigate(new Pages.ListEmployeePage());
            }
            else
            {
                var result = MessageBox.Show("Добавить пользователя?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    EF.Employee employee = new EF.Employee();

                    employee.LastName = TxbLastName.Text;
                    employee.FirstName = TxbFirstName.Text;
                    employee.Login = TxbLogin.Text;
                    employee.Password = PswPassword.Password;
                    employee.Phone = TxbPhone.Text;
                    employee.Email = TxbEmail.Text;
                    employee.IdRole = CmbRole.SelectedIndex + 1;
                    ClassHelper.AppData.Context.Employee.Add(employee);
                    ClassHelper.AppData.Context.SaveChanges();

                    MessageBox.Show("Сотрудник добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClassHelper.NavigateClass.frame.Navigate(new Pages.ListEmployeePage());
                }
            }
            
        }
    }
}

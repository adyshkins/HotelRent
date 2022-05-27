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
        public ListEmployeePage()
        {
            InitializeComponent();

            employees = AppData.context.Employee.ToList();

            lvEmployee.ItemsSource = employees;
        }
    }
}

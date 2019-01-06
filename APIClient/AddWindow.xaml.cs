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
using System.Windows.Shapes;
using EmployeeDepartmentWEB.Models;

namespace APIClient
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public Employee Employee { get; set; }

        public AddWindow (Employee employee)
        {
            InitializeComponent();
            Employee = employee;
        }

        private void BtnSave_Click ( object sender, RoutedEventArgs e )
        {
            Employee.Name = tbName.Text;
            Employee.Second_name = tbSecond_name.Text;
            Employee.Age = Int32.Parse( tbAge.Text );
            Employee.DepartmentId = Int32.Parse( tbDepartmentId.Text );
            this.DialogResult = true;
        }

        private void BtnCancel_Click ( object sender, RoutedEventArgs e )
        {
            this.DialogResult = false;
        }
    }
}

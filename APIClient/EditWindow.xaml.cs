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
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public Employee Employee { get; set; }

        public EditWindow (Employee employee)
        {
            InitializeComponent();

            Employee = employee;

            WindowLoaded();
        }

        private void WindowLoaded ()
        {
            tbName.Text = Employee.Name;
            tbSecond_name.Text = Employee.Second_name;
            tbAge.Text = Employee.Age.ToString();
        }

        private void BtnSave_Click ( object sender, RoutedEventArgs e )
        {
            Employee.Name = tbName.Text;
            Employee.Second_name = tbSecond_name.Text;
            Employee.Age = Int32.Parse( tbAge.Text );
            this.DialogResult = true;
        }

        private void BtnCancel_Click ( object sender, RoutedEventArgs e )
        {
            this.DialogResult = false;
        }
    }
}

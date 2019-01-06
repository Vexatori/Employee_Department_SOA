using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Data;
using System.Net.Http;
using Newtonsoft.Json;
using EmployeeDepartmentWEB.Models;

namespace APIClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent();

            string urlDpt = @"http://localhost:61975/getdepartments";
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add( "Accept", "application/json" );
            var res = httpClient.GetStringAsync( urlDpt ).Result;

            ObservableCollection<Department> departments = JsonConvert.DeserializeObject<ObservableCollection<Department>>( res );
            cbDepartments.ItemsSource = departments;
        }

        /// <summary>
        /// Обработка выбора в выпадающем списке департаментов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbDepartments_SelectionChanged ( object sender, SelectionChangedEventArgs e )
        {
            UpdateEmployees();
        }

        /// <summary>
        /// Добавление сотрудника в базу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAddEmpl_Click ( object sender, RoutedEventArgs e )
        {
            Employee employee = new Employee();
            AddWindow addWindow = new AddWindow( employee );
            addWindow.ShowDialog();

            if ( addWindow.DialogResult.Value )
            {
                string urlPost = @"http://localhost:61975/addemployee";
                HttpClient client = new HttpClient();

                string obj = @"{" + $@"""EmployeeId"": ""{GetEmployees().Count}"", ""Name"": ""{addWindow.Employee.Name}"", ""Second_name"": ""{addWindow.Employee.Second_name}"", ""Age"": ""{addWindow.Employee.Age}"", ""DepartmentId"": ""{addWindow.Employee.DepartmentId}""" + @"}";
                StringContent content = new StringContent( obj, Encoding.Unicode, "application/json" );
                var r = client.PostAsync( urlPost, content ).Result;
            }

            UpdateEmployees();
        }

        /// <summary>
        /// Удаление сотрудника из базы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteEmpl_Click ( object sender, RoutedEventArgs e )
        {
            string urlDel = @"http://localhost:61975/deleteemployee/" + ( ( int )( lvEmployees.SelectedItem as Employee ).EmployeeId );
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add( "Accept", "application/json" );
            var r = httpClient.DeleteAsync( urlDel );

            UpdateEmployees();
        }

        /// <summary>
        /// Обработка двойного нажатия кнопки мыши на сотруднике, вызывается новое окно для изменения его параметров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvEmployees_MouseDoubleClick ( object sender, MouseButtonEventArgs e )
        {
            Employee employee = lvEmployees.SelectedItem as Employee;
            EditWindow editWindow = new EditWindow( employee );
            editWindow.ShowDialog();

            if ( editWindow.DialogResult.Value )
            {
                string urlPost = @"http://localhost:61975/editemployee/" + employee.EmployeeId.ToString();
                HttpClient client = new HttpClient();

                string obj = @"{" + $@"""Name"": ""{editWindow.Employee.Name}"", ""Second_name"": ""{editWindow.Employee.Second_name}"", ""Age"": ""{editWindow.Employee.Age}"", ""DepartmentId"": ""{editWindow.Employee.DepartmentId}""" + @"}";
                StringContent content = new StringContent( obj, Encoding.Unicode, "application/json" );
                var r = client.PostAsync( urlPost, content ).Result;
            }

            UpdateEmployees();
        }

        /// <summary>
        /// Обновление сотрудников в выбранном департаменте
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateEmployees ()
        {
            string urlGet = @"http://localhost:61975/getemployeesfromdepartment/" + ( ( int )( cbDepartments.SelectedItem as Department ).Id );

            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add( "Accept", "application/json" );
            var res = httpClient.GetStringAsync( urlGet ).Result;

            ObservableCollection<Employee> employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>( res );
            lvEmployees.ItemsSource = employees;
        }

        private static ObservableCollection<Employee> GetEmployees ()
        {
            string urlGet = @"http://localhost:61975/getemployees";
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add( "Accept", "application/json" );
            var res = httpClient.GetStringAsync( urlGet ).Result;

            ObservableCollection<Employee> employees = JsonConvert.DeserializeObject<ObservableCollection<Employee>>( res );

            return employees;
        }
    }
}

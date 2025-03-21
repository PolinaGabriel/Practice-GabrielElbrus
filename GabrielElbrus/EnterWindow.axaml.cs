using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using GabrielElbrus.Models;
using System.Collections.Generic;
using System.Linq;

namespace GabrielElbrus;

public partial class EnterWindow : Window
{
    public Employee _employee = new Employee(); //пользователь, который вошёл

    public bool _start; //маркер для загрузки списка

    public EnterWindow(Employee employee)
    {
        InitializeComponent();
        _start = false;
        _employee = employee;
        employeeImage.Source = new Bitmap("Asset/" + _employee.EmployeeImage);
        employeeName.Text = _employee.EmployeeName;
        employeePosition.Text = _employee.EmployeePositionNavigation.PositionName;
        List<Employee> _logins = [new Employee() { EmployeeLogin = "Выбрать пользователя" }, .. Helper.Database.Employees.OrderBy(x => x.EmployeeId).ToList()];
        filterBox.ItemsSource = _logins;
        filterBox.SelectedIndex = 0;
        sortBox.SelectedIndex = 0;
        List<Enter> enters = Helper.Database.Enters.OrderBy(x => x.EnterId).ToList();
        enterList.ItemsSource = enters.Select(x => new {
            x.EnterTime,
            x.EnterEmployeeNavigation.EmployeeLogin,
            x.EnterResult
        });
        _start = true;
    }

    public void ForFilter(object sender, SelectionChangedEventArgs selectionChangedEventArgs) => Fill();

    public void ForSort(object sender, SelectionChangedEventArgs selectionChangedEventArgs) => Fill();

    /// <summary>
    /// Заполнение списка входов
    /// </summary>
    public void Fill()
    {
        List<Enter> enters = Helper.Database.Enters.OrderBy(x => x.EnterId).ToList();
        if (_start = true)
        {
            enters = DataOrder.EnterSort(DataOrder.EnterFilter(enters, filterBox), sortBox);
        }
        enterList.ItemsSource = enters.Select(x => new {
            x.EnterTime,
            x.EnterEmployeeNavigation.EmployeeLogin,
            x.EnterResult
        });

    }

    /// <summary>
    /// Возвращение к окну списка заказов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void GoBack(object sender, RoutedEventArgs routedEventArgs)
    {
        SaleWindow saleWindow = new SaleWindow(_employee);
        saleWindow.Show();
        this.Close();
    }
}
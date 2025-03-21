using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using GabrielElbrus.Models;
using System.Collections.Generic;
using System.Linq;

namespace GabrielElbrus;

public partial class CustomerWindow : Window
{
    public Employee _employee = new Employee(); //пользователь, который вошёл

    public Sale _sale = new Sale(); //новый или выбранный заказ

    public Customer _customer = new Customer(); //новый клиент

    public CustomerWindow(Employee employee, Sale sale, Customer customer)
    {
        InitializeComponent();
        _employee = employee;
        _sale = sale;
        _customer = customer;
        this.DataContext = _customer;
        employeeImage.Source = new Bitmap("Asset/" + _employee.EmployeeImage);
        employeeName.Text = _employee.EmployeeName;
        employeePosition.Text = _employee.EmployeePositionNavigation.PositionName;
        errorBlock.IsVisible = false;
    }

    /// <summary>
    /// Отмена добавления
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void Cancel(object sender, RoutedEventArgs routedEventArgs) => ShowEditWindow();

    /// <summary>
    /// Добавление клиента
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void Save(object sender, RoutedEventArgs routedEventArgs)
    {
        if (
               Check.Fill(codeBox.Text) == true &&
               Check.Fill(nameBox.Text) == true &&
               Check.Fill(passportBox.Text) == true &&
               Check.Fill(birthBox.Text) == true &&
               Check.Fill(addressBox.Text) == true &&
               Check.Fill(loginBox.Text) == true &&
               Check.Fill(passportBox.Text) == true &&
               Check.DistinctCustomer(_customer)
               )
        {
            Helper.Database.Customers.Add(_customer);
            Helper.Database.SaveChanges();
            ShowEditWindow();
        }
        else
        {
            errorBlock.IsVisible = true;
        }
    }

    /// <summary>
    /// Переход к окну изменения/добавления заказа
    /// </summary>
    public void ShowEditWindow()
    {
        EditWindow editWindow = new EditWindow(_employee, _sale);
        editWindow.Title = _sale.SaleId == 0 ? "Новый заказ" : "Редактирование заказа";
        editWindow.deleteButton.IsVisible = _sale.SaleId == 0 ? false : true;
        List <SaleService> saleServices = Helper.Database.SaleServices.Where(x => x.SaleServiceSale == _sale.SaleId).OrderBy(x => x.SaleServiceId).ToList();
        foreach (Service service in editWindow.serviceList.ItemsSource)
        {
            foreach (SaleService saleService in saleServices)
            {
                if (service.ServiceId == saleService.SaleServiceService)
                {
                    editWindow.serviceList.SelectedItems.Add(service);
                }
            }
        }
        editWindow.Show();
        this.Close();
    }
}
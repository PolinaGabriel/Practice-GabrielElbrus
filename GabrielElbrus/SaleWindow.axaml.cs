using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using GabrielElbrus.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GabrielElbrus;

public partial class SaleWindow : Window
{
    public Employee _employee = new Employee(); //пользователь, который вошёл
    
    public SaleWindow(Employee employee)
    {
        InitializeComponent();
        _employee = employee;
        employeeImage.Source = new Bitmap("Asset/" + _employee.EmployeeImage);
        employeeName.Text = _employee.EmployeeName;
        employeePosition.Text = _employee.EmployeePositionNavigation.PositionName;
        List<Sale> sales = Helper.Database.Sales.OrderBy(x => x.SaleId).ToList();
        saleList.ItemsSource = sales.Select(x => new
        {
            x.SaleId,
            x.SaleCode,
            x.SaleCustomerNavigation.CustomerName,
            x.SaleStatusNavigation.StatusName,
            Visibility = (_employee.EmployeePosition == 1) ? false : true
        });
        if (employee.EmployeePosition != 1)
        {
            enterButton.IsVisible = false;
        }
        else
        {
            addButton.IsVisible = false;
        }
    }

    /// <summary>
    /// Выход
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void GoBack(object sender, RoutedEventArgs routedEventArgs)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }

    /// <summary>
    /// Переход к окну истории входов
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void GoToEnters(object sender, RoutedEventArgs routedEventArgs)
    {
        EnterWindow enterWindow = new EnterWindow(_employee);
        enterWindow.Show();
        this.Close();
    }

    /// <summary>
    /// Переход к окну добавления заказа
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void AddSale(object sender, RoutedEventArgs routedEventArgs)
    {
        Sale sale = new Sale() { SaleStart = DateTime.Now };
        EditWindow editWindow = new EditWindow(_employee, sale);
        editWindow.Title = "Новый заказ";
        editWindow.deleteButton.IsVisible = false;
        editWindow.Show();
        this.Close();
    }

    /// <summary>
    /// Переход к окну редактирования заказ
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void EditSale(object sender, RoutedEventArgs routedEventArgs)
    {
        Sale sale = Helper.Database.Sales.FirstOrDefault(x => x.SaleId == (int)(sender as Button).Tag);
        EditWindow editWindow = new EditWindow(_employee, sale);
        editWindow.Title = "Редактирование заказа";
        List<SaleService> saleServices = Helper.Database.SaleServices.Where(x => x.SaleServiceSale == sale.SaleId).OrderBy(x => x.SaleServiceId).ToList();
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
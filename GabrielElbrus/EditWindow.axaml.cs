
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Castle.Core.Resource;
using GabrielElbrus.Models;
using HarfBuzzSharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace GabrielElbrus;

public partial class EditWindow : Window
{
    public Employee _employee = new Employee(); //пользователь, который вошёл

    public Sale _sale = new Sale(); //новый или выбранный заказ

    public List<Customer> _customers = Helper.Database.Customers.OrderBy(x => x.CustomerId).ToList(); //список клиентов

    public List<Status> _statuses = Helper.Database.Statuses.OrderBy(x => x.StatusId).ToList(); //список статусов заказа

    public List<Service> _services = Helper.Database.Services.OrderBy(x => x.ServiceId).ToList(); //список услуг

    public EditWindow(Employee employee, Sale sale)
    {
        InitializeComponent();
        _employee = employee;
        _sale = sale;
        this.DataContext = _sale;
        employeeImage.Source = new Bitmap("Asset/" + _employee.EmployeeImage);
        employeeName.Text = _employee.EmployeeName;
        employeePosition.Text = _employee.EmployeePositionNavigation.PositionName;
        errorBlock.IsVisible = false;
        customerSearchBox.Text = "";
        serviceSearchBox.Text = "";
        customerBox.ItemsSource = _customers;
        customerBox.SelectedIndex = _sale.SaleId == 0 ? -1 : _sale.SaleCustomer - 1;
        statusBox.ItemsSource = _statuses;
        statusBox.SelectedIndex = _sale.SaleId == 0 ? -1 : _sale.SaleStatus - 1;
        serviceList.ItemsSource = _services;
    }

    /// <summary>
    /// Отмена изменений
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void Cancel(object sender, RoutedEventArgs routedEventArgs)
    {
        SaleWindow saleWindow = new SaleWindow(_employee);
        saleWindow.Show();
        this.Close();
    }

    /// <summary>
    /// Поиск клиента
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="keyEventArgs"></param>
    public void FillCustomers(object sender, KeyEventArgs keyEventArgs)
    {
        customerBox.ItemsSource = DataOrder.CustomerSearch(_customers, customerSearchBox.Text);
    }

    /// <summary>
    /// Поиск услуги
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="keyEventArgs"></param>
    public void FillServices(object sender, KeyEventArgs keyEventArgs)
    {
        serviceList.ItemsSource = DataOrder.ServiceSearch(_services, serviceSearchBox.Text);
    }

    /// <summary>
    /// Добавление клиента, если его нет в базе
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void AddCustomer(object sender, RoutedEventArgs routedEventArgs)
    {
        Customer customer = new Customer() { CustomerBirth = DateTime.Now };
        CustomerWindow customerWindow = new CustomerWindow(_employee, _sale, customer);
        customerWindow.Show();
        this.Close();
    }
    
    /// <summary>
    /// Проверки
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void Checks(object sender, RoutedEventArgs routedEventArgs)
    {
        if (Check.Fill(codeBox.Text) == false)
        {
            codeBox.Text = Helper.Database.Customers.FirstOrDefault(x => x.CustomerId == customerBox.SelectedIndex).CustomerCode + "/" + startDatePicker.SelectedDate;
        }
        if (
            Check.Fill(timeBox.Text) == true &&
            Check.Select(customerBox.SelectedIndex) == true &&
            Check.Select(statusBox.SelectedIndex) == true &&
            Check.Multiple(serviceList.SelectedItems.Count) == true &&
            Check.DistinctSale(_sale) == true &&
            Check.Hours(timeBox.Text) == true &&
            Check.DateSelection(startDatePicker) == true &&
            Check.TimeSelection(startTimePicker) == true
            )
        {
            if (Check.NotZero(Convert.ToDouble(timeBox.Text)) == true)
            {
                if ((statusBox.SelectedItem as Status).StatusId == 3)
                {
                    if (Check.DateSelection(endDatePicker) == true && Check.TimeSelection(endTimePicker) == true)
                    {
                        if (startDatePicker.SelectedDate == endDatePicker.SelectedDate)
                        {
                            if (startTimePicker.SelectedTime < endTimePicker.SelectedTime)
                            {
                                Save();
                            }
                            else
                            {
                                errorBlock.IsVisible = true;
                            }
                        }
                        else if (startDatePicker.SelectedDate > endDatePicker.SelectedDate)
                        {
                            errorBlock.IsVisible = true;
                        }
                        else
                        {
                            Save();
                        }
                    }
                    else
                    {
                        errorBlock.IsVisible = true;
                    }
                }
                else
                {
                    Save();
                }
            }
            else
            {
                errorBlock.IsVisible = true;
            }
        }
        else
        {
            errorBlock.IsVisible = true;
        } 
    }

    /// <summary>
    /// Сохранение изменений
    /// </summary>
    public void Save()
    {
        errorBlock.IsVisible = false;
        _sale.SaleCustomer = (customerBox.SelectedItem as Customer).CustomerId;
        _sale.SaleStatus = (statusBox.SelectedItem as Status).StatusId;
        _sale.SaleEmployee = _employee.EmployeeId;
        if (_sale.SaleId == 0)
        {
            Helper.Database.Sales.Add(_sale);
        }
        else
        {
            Helper.Database.Sales.Update(_sale);
            EditServices.RemoveServices(_sale);
        }
        Helper.Database.SaveChanges();
        EditServices.AddServices(_sale, serviceList);
        Helper.Database.SaveChanges();
        SaleWindow saleWindow = new SaleWindow(_employee);
        saleWindow.Show();
        this.Close();
    }

    /// <summary>
    /// Удаление заказа
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void Delete(object sender, RoutedEventArgs routedEventArgs)
    {
        DeleteWindow deleteWindow = new DeleteWindow(_employee, _sale);
        deleteWindow.Show();
        this.Close();
    }
}
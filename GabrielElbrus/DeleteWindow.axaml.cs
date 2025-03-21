using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GabrielElbrus.Models;

namespace GabrielElbrus;

public partial class DeleteWindow : Window
{
    public Employee _employee = new Employee(); //пользователь, который вошёл

    public Sale _sale = new Sale(); //новый или выбранный заказ

    public DeleteWindow(Employee employee, Sale sale)
    {
        InitializeComponent();
        _employee = employee;
        _sale = sale;
        this.Title = _sale.SaleCode;
    }

    /// <summary>
    /// Согласие на удаление
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void Yes(object sender, RoutedEventArgs routedEventArgs)
    {
        EditServices.RemoveServices(_sale);
        Helper.Database.Sales.Remove(_sale);
        Helper.Database.SaveChanges();
        SaleWindow saleWindow = new SaleWindow(_employee);
        saleWindow.Show();
        this.Close();
    }

    /// <summary>
    /// Отказ от удаления
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="routedEventArgs"></param>
    public void No(object sender, RoutedEventArgs routedEventArgs)
    {
        EditWindow editWindow = new EditWindow(_employee, _sale);
        editWindow.Show();
        this.Close();
    }
}
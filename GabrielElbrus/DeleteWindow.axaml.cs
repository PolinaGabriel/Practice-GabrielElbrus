using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GabrielElbrus.Models;

namespace GabrielElbrus;

public partial class DeleteWindow : Window
{
    public Employee _employee = new Employee(); //������������, ������� �����

    public Sale _sale = new Sale(); //����� ��� ��������� �����

    public DeleteWindow(Employee employee, Sale sale)
    {
        InitializeComponent();
        _employee = employee;
        _sale = sale;
        this.Title = _sale.SaleCode;
    }

    /// <summary>
    /// �������� �� ��������
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
    /// ����� �� ��������
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
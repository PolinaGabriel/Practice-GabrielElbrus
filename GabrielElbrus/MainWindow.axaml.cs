using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using GabrielElbrus.Models;
using System;
using System.Linq;

namespace GabrielElbrus
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            loginBox.Text = "";
            passwordBox.Text = "";
            passwordBox.PasswordChar = '*';
            enterButton.IsEnabled = false;
            errorBlock.IsVisible = false;
        }

        /// <summary>
        /// Кнопка входа не работает при пустых полях логина и пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="keyEventArgs"></param>
        public void Enable(object sender, KeyEventArgs keyEventArgs)
        {
            enterButton.IsEnabled = (loginBox.Text == "" || passwordBox.Text == "") ? false : true;
        }

        /// <summary>
        /// Показ и скрытие пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        public void ShowPassword(object sender, RoutedEventArgs routedEventArgs)
        {
            if (showButton.IsChecked == true)
            {
                passwordBox.PasswordChar -= '*';
            }
            else
            {
                passwordBox.PasswordChar = '*';
            }
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        public void Enter(object sender, RoutedEventArgs routedEventArgs)
        {
            Employee employee = Helper.Database.Employees.FirstOrDefault(x => x.EmployeeLogin == loginBox.Text);
            if (employee != null)
            {
                Enter enter = new Enter() { EnterTime = DateTime.Now, EnterEmployee = employee.EmployeeId };
                if (employee.EmployeePassword == passwordBox.Text)
                {
                    enter.EnterResult = true;
                    errorBlock.IsVisible = false;
                    SaleWindow saleWindow = new SaleWindow(employee);
                    saleWindow.Show();
                    this.Close();
                }
                else
                {
                    enter.EnterResult = false;
                    errorBlock.Text = "Неверный пароль";
                    errorBlock.IsVisible = true;
                }
                Helper.Database.Enters.Add(enter);
                Helper.Database.SaveChanges();
            }
            else
            {
                errorBlock.Text = "Логин отсутствует в базе";
                errorBlock.IsVisible = true;
            }
        }
    }
}
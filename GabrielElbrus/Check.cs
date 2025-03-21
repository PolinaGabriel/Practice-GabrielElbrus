using Avalonia.Controls;
using GabrielElbrus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielElbrus
{
    public static class Check
    {
        /// <summary>
        /// Проверка заполненности поля
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool Fill(string text)
        {
            return string.IsNullOrEmpty(text) == true ? false : true;
        }

        /// <summary>
        /// Проверка выбора индекса в текстбоксе
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool Select(int index)
        {
            return index == -1 ? false : true;
        }

        /// <summary>
        /// Проверка выбора услуг в листбоксе
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool Multiple(int count)
        {
            return count == 0 ? false : true;
        }

        /// <summary>
        /// Проверка уникальности кода заказа
        /// </summary>
        /// <param name="sale"></param>
        /// <returns></returns>
        public static bool DistinctSale(Sale sale)
        {
            bool check = true;
            foreach (Sale s in Helper.Database.Sales.Where(x => x.SaleId != sale.SaleId))
            {
                if (s.SaleCode == sale.SaleCode)
                {
                    check = false;
                    break;
                }
            }
            return check;
        }

        /// <summary>
        /// Проверка уникальности клиента
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static bool DistinctCustomer(Customer customer)
        {
            bool check = true;
            foreach (Customer c in Helper.Database.Customers)
            {
                if (c.CustomerCode == customer.CustomerCode || c.CustomerPassport == customer.CustomerPassport || c.CustomerLogin == customer.CustomerLogin)
                {
                    check = false;
                    break;
                }
            }
            return check;
        }

        /// <summary>
        /// Проверка заполнения текстбокса с часами заказа
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool Hours(string text)
        {
            bool check = true;
            foreach (char c in text)
            {
                if (char.IsDigit(c) != true && c != ',')
                {
                    check = false;
                    break;
                }
            }
            return check;
        }

        /// <summary>
        /// Проверка заполнения текстбокса с часами заказа
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static bool NotZero(double count)
        {
            return count == 0 ? false : true;
        }


        public static bool DateSelection(CalendarDatePicker calendarDatePicker)
        {
            return calendarDatePicker.SelectedDate == null ? false : true;
        }

        public static bool TimeSelection(TimePicker timePicker)
        {
            return timePicker.SelectedTime == null ? false : true;
        }
    }
}
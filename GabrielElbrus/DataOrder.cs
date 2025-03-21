using Avalonia.Controls;
using GabrielElbrus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielElbrus
{
    public static class DataOrder
    {
        /// <summary>
        /// Поиск клиентов
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public static List<Customer> CustomerSearch(List<Customer> customers, string searchLine)
        {
            List<Customer> search = new List<Customer>();
            string[] words = searchLine.Split(' ');
            foreach (Customer customer in customers)
            {
                foreach (string word in words)
                {
                    if (customer.CustomerName.ToLower().Contains(word.ToLower()) == true)
                    {
                        search.Add(customer);
                    }
                }
            }
            return search;
        }

        /// <summary>
        /// Поиск услуг
        /// </summary>
        /// <param name="services"></param>
        /// <param name="searchLine"></param>
        /// <returns></returns>
        public static List<Service> ServiceSearch(List<Service> services, string searchLine)
        {
            List<Service> search = new List<Service>();
            string[] words = searchLine.Split(' ');
            foreach (Service service in services)
            {
                foreach (string word in words)
                {
                    if (service.ServiceName.ToLower().Contains(word.ToLower()) == true)
                    {
                        search.Add(service);
                    }
                }
            }
            return search;
        }

        /// <summary>
        /// Фильтрация истории входов
        /// </summary>
        /// <param name="enters"></param>
        /// <param name="filterBox"></param>
        /// <returns></returns>
        public static List<Enter> EnterFilter(List<Enter> enters, ComboBox filterBox)
        {
            switch (filterBox.SelectedIndex)
            {
                case 0:
                    return enters;

                default:
                    IEnumerable<Enter> filter =
                    from x in enters
                    where x.EnterEmployee == filterBox.SelectedIndex
                    select x;
                    return filter.ToList();
            }
        }

        /// <summary>
        /// Сортировка истории входов
        /// </summary>
        /// <param name="enters"></param>
        /// <param name="sortBox"></param>
        /// <returns></returns>
        public static List<Enter> EnterSort(List<Enter> enters, ComboBox sortBox)
        {
            switch (sortBox.SelectedIndex)
            {
                default:
                    return enters;

                case 1:
                    return enters.OrderBy(x => x.EnterTime).ToList();

                case 2:
                    return enters.OrderByDescending(x => x.EnterTime).ToList();
            }
        }
    }
}
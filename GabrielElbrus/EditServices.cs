using Avalonia.Controls;
using GabrielElbrus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GabrielElbrus
{
    public static class EditServices
    {
        /// <summary>
        /// Удаление записей из ассоциативной таблицы
        /// </summary>
        /// <param name="sale"></param>
        public static void RemoveServices(Sale sale)
        {
            foreach (SaleService saleService in Helper.Database.SaleServices.OrderBy(x => x.SaleServiceId))
            {
                if (saleService.SaleServiceSale == sale.SaleId)
                {
                    Helper.Database.SaleServices.Remove(saleService);
                }
            }
        }

        /// <summary>
        /// Добавление записей из ассоциативной таблицы
        /// </summary>
        /// <param name="sale"></param>
        /// <param name="listBox"></param>
        public static void AddServices(Sale sale, ListBox listBox)
        {
            foreach (Service service in listBox.SelectedItems)
            {
                SaleService saleService = new SaleService()
                {
                    SaleServiceSale = sale.SaleId,
                    SaleServiceService = service.ServiceId
                };
                Helper.Database.SaleServices.Add(saleService);
            }
        }
    }
}
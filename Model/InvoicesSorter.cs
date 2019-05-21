using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public static class InvoicesSorter
    {
        public static ICollection<Invoice> SortByMonthYear(string date, ICollection<Invoice> collection)
        {
            DateTime _date = Convert.ToDateTime(date,CultureInfo.InvariantCulture);

            return collection.Select(x => new
            {
                Inv = x,
                _invDate = Convert.ToDateTime(x.InvoiceDate.ToString())
            })
            .Where(x => x._invDate.Year == _date.Year)
            .Where(x => x._invDate.Month == _date.Month)
            .Select(x => x.Inv)
            .ToList();
        }

        /// <summary>
        /// Gets the paid invoices.
        /// </summary>
        /// <param name="collection">Invoices collection</param>
        /// <returns>Paid Invoices</returns>
        public static ICollection<Invoice> GetPaidInvoices(ICollection<Invoice> collection)
        {
            return collection.Where(x => x.LeftToPay == 0).ToList();
        }

        /// <summary>
        /// Gets the unpaid invoices.
        /// </summary>
        /// <param name="collection">Invoices Collection</param>
        /// <returns>UnPaid Invoices</returns>
        public static ICollection<Invoice> GetUnPaidInvoices(ICollection<Invoice> collection)
        {
            return collection.Where(x => x.LeftToPay != 0).ToList();
        }

    }
}

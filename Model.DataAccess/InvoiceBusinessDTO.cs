using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataAccess
{
    public class InvoiceBusinessDTO
    {
        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string Number { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime PaidDate { get; set; }
        public Decimal ToPay { get; set; }
        public string ToPayInWords { get; set; }
        public DateTime Deadline { get; set; }
        public decimal Paid { get; set; }
        public decimal LeftToPay { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; }
        //---Buisness
        public string BusinessName { get; set; }
        public string NIP { get; set; }
        public string Regon { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string BuisnessBankName { get; set; }
        public string BuisnessBankAccountNumber { get; set; }
    }
}

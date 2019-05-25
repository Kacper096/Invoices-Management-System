using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataAccess
{
    public class InvoiceDetailsDTO
    {
        public string ServiceName { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal NetValue { get; set; }
        public string VatRate { get; set; }
        public decimal VatValue { get; set; }
        public decimal Value { get; set; }
    }
}

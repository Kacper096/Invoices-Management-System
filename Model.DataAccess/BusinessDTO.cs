using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataAccess
{
    public class BusinessDTO
    {
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

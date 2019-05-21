using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class InvoiceDetails
    {
        private readonly string _ServiceName;
        private readonly int _Amount;
        private readonly decimal _UnitPrice;
        private readonly decimal _NetValue;
        private readonly string _VatRate;
        private readonly decimal _VatValue;
        private readonly decimal _Value;

        public InvoiceDetails(string ServiceName, int Amount, decimal UnitPrice, decimal NetValue, string VatRate, decimal VatValue, decimal Value)
        {
            _ServiceName = ServiceName ?? throw new ArgumentNullException(nameof(ServiceName));
            _Amount = Amount;
            _UnitPrice = UnitPrice;
            _NetValue = NetValue;
            _VatRate = VatRate ?? throw new ArgumentNullException(nameof(VatRate));
            _VatValue = VatValue;
            _Value = Value;
        }

        public string ServiceName => _ServiceName;

        public int Amount => _Amount;

        public decimal UnitPrice => Decimal.Round(_UnitPrice,2);

        public decimal NetValue => Decimal.Round(_NetValue,2);

        public string VatRate => _VatRate;

        public decimal VatValue => Decimal.Round(_VatValue, 2);

        public decimal Value => Decimal.Round(_Value,2);
    }
}

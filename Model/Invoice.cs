using Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Invoice
    {
        #region Private Fields
        private readonly IAccountDataAccess _account;
        private readonly int _ID;
        private readonly string _Number;
        private readonly DateTime _InvoiceDate;
        private readonly DateTime _PaidDate;
        private readonly Decimal _ToPay;
        private readonly string _ToPayInWords;
        private readonly DateTime _Deadline;
        private readonly decimal _Paid;
        private readonly decimal _LeftToPay;
        private readonly string _Status;
        private readonly string _Currency;
        private readonly Business _business;
        private readonly bool _canPay = false;
        private readonly int _categoryID;
        #endregion

        #region Properties
        public int ID => _ID;

        public int CategoryID => _categoryID;

        public string Number => _Number;

        public string InvoiceDate => _InvoiceDate.ToString("dd/MM/yyyy");

        public decimal ToPay => Decimal.Round(_ToPay,2);

        public string ToPayWithCurrency => CostWithCurrency(this._ToPay);

        public string Deadline => _Deadline.ToString("dd/MM/yyyy");

        public decimal LeftToPay => Decimal.Round(_LeftToPay,2);

        public string LeftToPayWithCurrency => CostWithCurrency(this._LeftToPay);

        public string Status => _Status;

        public Business Business => _business;

        public bool CanPay => _canPay;

        public decimal Paid => Decimal.Round(_Paid,2);

        public string ToPayInWords => _ToPayInWords;

        public string PaidDate => _PaidDate.ToString("dd/MM/yyyy") == "01/01/0001" ? string.Empty : _PaidDate.ToString("dd/MM/yyyy");

        public ICollection<InvoiceDetails> Details => GetInvoiceDetails();

        #endregion

        #region Constructors
        public Invoice()
        {
            _account = new AccountDataAccess();
        }

        public Invoice(int ID, int CategoryID, string Number, DateTime InvoiceDate, DateTime PaidDate, decimal ToPay, string ToPayInWords, DateTime Deadline, decimal Paid, decimal LeftToPay, string Status, string Currency, Business business)
        {
            _ID = ID;
            _categoryID = CategoryID;
            _Number = Number;
            _InvoiceDate = InvoiceDate;
            _PaidDate = PaidDate;
            _ToPay = ToPay;
            _ToPayInWords = ToPayInWords;
            _Deadline = Deadline;
            _Paid = Paid;
            _LeftToPay = LeftToPay;
            _Status = Status;
            _Currency = Currency;
            _business = business;
            _account = new AccountDataAccess();
            _canPay = _LeftToPay == 0 ? false : true;
        }


        #endregion

        /// <summary>
        /// Pays this invoice.
        /// </summary>
        /// <param name="value"></param>
        public async Task<bool> Pay(decimal value)
        {
            if (value <= 0)
                throw new ArgumentException("Kwota nie może być poniżej ani równa 0.");
            try
            {
                if (value > _LeftToPay)
                    return await _account.PayInvoice(_ID, _LeftToPay);
                else
                    return await _account.PayInvoice(_ID, value);
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Gets the string with currency. Additionally round to two decimal places.
        /// </summary>
        /// <param name="cost">Decimal to convert.</param>
        /// <returns></returns>
        private string CostWithCurrency(decimal cost)
        {
            string result = string.Format("{0} {1}", Decimal.Round(cost, 2), _Currency.ToLower());

            return result;
        }

        /// <summary>
        /// Gets the collection's information about Invoice.
        /// </summary>
        /// <returns>Invoice Details</returns>
        private ICollection<InvoiceDetails> GetInvoiceDetails()
        {
            IEnumerable<InvoiceDetailsDTO> result = _account.GetInvoiceDetails(_ID);
            ICollection<InvoiceDetails> _invoiceDetails = new List<InvoiceDetails>();

            foreach(var item in result)
            {
                _invoiceDetails.Add(new InvoiceDetails(item.ServiceName, item.Amount, item.UnitPrice,
                    item.NetValue, item.VatRate, item.VatValue, item.Value));
            }

            return _invoiceDetails;
        }

    }
}

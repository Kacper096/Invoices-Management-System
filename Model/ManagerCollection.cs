using Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ManagerCollection
    {
        private readonly CategoryDataAccess _dataAccess;
        private readonly AccountDataAccess _account;

        private readonly ICollection<Category> _cats;
        private readonly ICollection<Invoice> _invs;
        private readonly ICollection<Business> _buss;

        public ICollection<Category> Categories => _cats;

        /// <summary>
        /// Gets the latest invoices.
        /// </summary>
        public ICollection<Invoice> AllInvoices => _invs.Select(x => x).ToList();

        public ManagerCollection()
        {
            _dataAccess = new CategoryDataAccess();
            _account = new AccountDataAccess();
            _cats = new List<Category>();
            _invs = new List<Invoice>();
            _buss = new List<Business>();
            SetCategories();
        }

        /// <summary>
        /// Gets the Categories from Database.
        /// </summary>
        private void SetCategories()
        {
            IEnumerable<CategoryDTO> dTOs = _dataAccess.GetAllCategories().ToArray();


            foreach (var item in dTOs)
            {
                var kat = new Category(item.ID,item.Name, item.Opis);

                _cats.Add(kat);
            }

        }

        /// <summary>
        /// Gets the inforamtion about invoices belong to client.
        /// </summary>
        /// <param name="accountID">Client's ID</param>
        /// <param name="categoryID">Category's ID</param>
        /// <returns></returns>
        public ICollection<Invoice> GetAllInvoices(int accountID, int categoryID)
        {
            _invs.Clear();

            //Gets the All user's invoices.
            IEnumerable<InvoiceBusinessDTO> _dTOs = _account.GetInvoicesInfo(accountID, categoryID);

            foreach(var item in _dTOs)
            {
                Invoice _inv = new Invoice(item.ID, item.CategoryID, item.Number, item.InvoiceDate, item.PaidDate, item.ToPay,
                    item.ToPayInWords, item.Deadline, item.Paid, item.LeftToPay, item.Status, item.Currency,
                    business:new Business(item.BusinessName,item.NIP, item.Regon, item.BuisnessBankName, item.BuisnessBankAccountNumber,
                    new Address(item.Country, item.City,item.Street,item.PostalCode)));

                _invs.Add(_inv);
            }

            return _invs;
        }

        /// <summary>
        /// Gets all companies information.
        /// </summary>
        /// <returns></returns>
        public ICollection<Business> GetAllCompanies()
        {
            try
            {
                _buss.Clear();

                IEnumerable<BusinessDTO> _res = _account.GetComapnies();

                foreach(var item in _res)
                {
                    Business _bus = new Business(item.BusinessName, item.NIP, item.Regon, item.BuisnessBankName, item.BuisnessBankAccountNumber, item.Country,
                        item.City, item.Street, item.PostalCode);

                    _buss.Add(_bus);
                }

                return _buss;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Getss the invoice from collection.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Invoice GetInvoice(int ID, ICollection<Invoice> invoices)
        {
            return invoices.Where(x => x.ID == ID).Select(x => x).FirstOrDefault();
        }
    }
}

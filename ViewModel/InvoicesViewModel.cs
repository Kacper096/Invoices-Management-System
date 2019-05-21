using Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ViewModel
{
    public class InvoicesViewModel : BindableBase, IDisposable
    {
        #region Private Fields
        private readonly ManagerCollection _manager;
        private string _date = DateTime.Now.ToString("MM/dd/yyyy"); //Default
        private ICollection<Invoice> _invoices;
        private string _errorMessage = string.Empty;
        private bool _isAccepted = false;
        private bool _isLoading = false;

        //CheckBoxes
        private bool _isPaid = true;
        private bool _isUnPaid = true;
        #endregion

        #region Invoice Details
        // It chooses the vm children.
        private object _selectedViewModel;
        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                SetProperty(ref _selectedViewModel, value);
            }
        }

        public ICommand DetailsCommand => new DelegateCommand<object>(GetInvoiceDetails);

        private void GetInvoiceDetails(object invoiceID)
        {
            if (invoiceID is int _result)
            {
                var invoice = new InvoiceViewModel( _manager.GetInvoice(_result, _invoices));
                SelectedViewModel = invoice;
                invoice.CloseControl += () => SelectedViewModel = null;
            }
        }
        #endregion

        #region Open / Close DataContext
        //It's neccessary to say other ViewModel 'I wanna close right now'
        public delegate void Close();
        public event Close CloseControl;
        #endregion

        #region Shows Selected Pay Invoice Content
        private Invoice _invoice = null;

        public Invoice PayInvoice
        {
            get => _invoice;
            set
            {
                SetProperty(ref _invoice, value);
            }
        }

        public ICommand PayContent => new DelegateCommand<object>(ShowPayContent);

        private void ShowPayContent(object invoice)
        {
            if (invoice is int _invID)
                PayInvoice = _manager.GetInvoice(_invID, _invoices);

        }
        #endregion

        #region Pay Invoice
        public ICommand PayCommand => new DelegateCommand<string>(Pay);

        /// <summary>
        /// Pay the invoice.
        /// </summary>
        /// <param name="value"></param>
        private async void Pay(string _toPayString)
        {
            IsLoading = true;
            if(string.IsNullOrEmpty(_toPayString))
            {
                ErrorMessage = "Wprowadź dane.";
                IsAccepted = true;
                IsLoading = false;
            }
            else
                if (decimal.TryParse(_toPayString, out decimal result))
                {
                    result = Decimal.Round(result, 2);

                    try
                    {
                        IsAccepted = await PayInvoice.Pay(result);
                    if (!IsAccepted)
                        {
                            ErrorMessage = "Transakcja nie może być zrealizowana.";
                        }
                        else
                        {
                            var _tempCat = PayInvoice.CategoryID;
                            ErrorMessage = "Transakcja zrealizowana.";
                            //refresh the list of invoices
                            Invoices = SelectInvoices(InvoicesSorter.SortByMonthYear(_date, _manager.GetAllInvoices(MainViewModel.Account.ID, _tempCat)).ToList());
                        }

                        IsAccepted = true;
                        IsLoading = false;
                    }
                    catch (Exception e)
                    {
                        ErrorMessage = e.Message;
                        IsAccepted = true;
                        IsLoading = false;
                    }
                }
                
        }

        /// <summary>
        /// It will show error message or accepted message.
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                SetProperty(ref _errorMessage, value);
            }
        }

        /// <summary>
        /// It provides Active data to expander in Faktury.xaml
        /// </summary>
        public bool IsAccepted
        {
            get => _isAccepted;
            set
            {
                SetProperty(ref _isAccepted, value);
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                SetProperty(ref _isLoading, value);
            }
        }
        public ICommand Accepted => new DelegateCommand(() => { IsAccepted = false; });
        #endregion
        public ICommand ExitCommand { get; }
       

        /// <summary>
        /// Contains invoices which it's displayed.
        /// </summary>
        public ICollection<Invoice> Invoices
        {
            get => _invoices;
            set
            {
                if(!_invoices.Equals(value))
                    SetProperty(ref _invoices, value);
            }
        }

        /// <summary>
        /// It's important to sort invoices.
        /// </summary>
        public string Data
        {
            get => _date;
            set
            {
                SetProperty(ref _date, value);
                Invoices = SelectInvoices(InvoicesSorter.SortByMonthYear(_date, _manager.AllInvoices).ToList());
            }
        }

        #region CheckBoxes 
        /// <summary>
        /// Returns true if _invoices has been paid invoices.
        /// </summary>
        public bool IsPaid
        {
            get => _isPaid;
            set
            {
                SetProperty(ref _isPaid, value);
                Invoices = InvoicesSorter.SortByMonthYear(_date,SelectInvoices());
            }
        }

        public bool IsUnPaid
        {
            get => _isUnPaid;
            set
            {
                SetProperty(ref _isUnPaid, value);

                Invoices = InvoicesSorter.SortByMonthYear(_date,SelectInvoices());
            }
                
        }
        #endregion

        public InvoicesViewModel(int categoryID)
        {
            //gets the invoices from manager collection
            _manager = new ManagerCollection();
            _invoices = InvoicesSorter.SortByMonthYear(_date,_manager.GetAllInvoices(MainViewModel.Account.ID, categoryID));
            //exit invoices page
            ExitCommand = new DelegateCommand(CloseMe); 
        }

        /// <summary>
        /// Selects collection Invoices. It selects from All invoices to Paid Invoices and Unpaid Invoices.
        /// </summary>
        private ICollection<Invoice> SelectInvoices(ICollection<Invoice> invoices = null)
        {
            ICollection<Invoice> _temp = new List<Invoice>();
            ICollection<Invoice> _allFromDB;

            if (invoices != null)
                _allFromDB = invoices;
            else
                _allFromDB = _manager.AllInvoices;


            if (_isPaid && !_isUnPaid)
                _temp = InvoicesSorter.GetPaidInvoices(_allFromDB);
            else if (!_isPaid && _isUnPaid)
                _temp = InvoicesSorter.GetUnPaidInvoices(_allFromDB);
            else if (_isPaid && _isUnPaid)
                _temp = _allFromDB;

            return _temp;
        }

        /// <summary>
        /// This is attached with ExitButton.
        /// </summary>
        private void CloseMe()
        {
            CloseControl?.Invoke();
            this.Dispose();
        }

        /// <summary>
        /// It Deletes all data and it deletes this element.
        /// </summary>
        public void Dispose()
        {
            _invoices.Clear();
            CloseControl = null;
            GC.SuppressFinalize(this);
        }
    }
}

using Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class InvoiceViewModel : IDisposable
    {
        private readonly Invoice _invoice;


        public Invoice Invoice => _invoice;

        #region Attach with view
        //It's neccessary to say other ViewModel 'I wanna close right now'
        public delegate void Close();
        public event Close CloseControl;

        public ICommand ExitCommand => new DelegateCommand(CloseMe);
        #endregion

        public InvoiceViewModel(Invoice invoice)
        { 
            _invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
        }
        /// <summary>
        /// This is attached with ExitButton.
        /// </summary>
        private void CloseMe()
        {
            CloseControl?.Invoke();
            this.Dispose();
        }

        public void Dispose()
        {
            CloseControl = null;
            GC.SuppressFinalize(this);
        }
    }
}

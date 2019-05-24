using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModel
{
    public class CreateAccViewModel : BindableBase, IDisposable
    {
        #region Private Fields
        private string _name = string.Empty;
        private string _surrname = string.Empty;
        private string _pesel = string.Empty;
        private string _password = string.Empty;
        private string _repeatedPassword = string.Empty;
        private static int _amountOfInstances = 0;
        #endregion

        #region Binding Properties
        public string Name
        {
            get => _name;
            set
            {
                SetProperty(ref _name, value);
            }
        }
        public string Surrname
        {
            get => _surrname;
            set
            {
                SetProperty(ref _surrname, value);
            }
        }
        public string PESEL
        {
            get => _pesel;
            set
            {
                SetProperty(ref _pesel, value);
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
            }
        }
        public string RepeatedPassword
        {
            get => _repeatedPassword;
            set
            {
                SetProperty(ref _repeatedPassword, value);
            }
        }
        #endregion

        public static int AmountOfInstances
        {
            get => _amountOfInstances;
        }

        public CreateAccViewModel()
        {
            _amountOfInstances++;
            CloseCommand = new DelegateCommand(CloseMe);
        }

        #region Exit View
        public ICommand CloseCommand { get; }

        public delegate void Close();
        public event Close CloseControl;

        /// <summary>
        /// This is attached with ExitButton.
        /// </summary>
        private void CloseMe()
        {
            CloseControl?.Invoke();
            this.Dispose();
        }
        #endregion

        public void Dispose()
        {
            CloseControl = null;
            _amountOfInstances = 0;
            GC.SuppressFinalize(this);
        }
    }
}

using Model;
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
        private string _lastname = string.Empty;
        private string _pesel = string.Empty;
        private string _password = string.Empty;
        private string _repeatedPassword = string.Empty;
        private static int _amountOfInstances = 0;
        private ILogin _login;
        private string _errorMessage;
        private bool _showErrorMessage;
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
        public string Lastname
        {
            get => _lastname;
            set
            {
                SetProperty(ref _lastname, value);
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

        #region Create New Account
        public ICommand CreateCommand => new DelegateCommand(CreateAccount);
        public ICommand Accepted => new DelegateCommand(() => { ShowErrorMessage = false; });
        /// <summary>
        /// Provides error text to UI.
        /// </summary>
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                SetProperty(ref _errorMessage, value);
            }
        }

        public bool ShowErrorMessage
        {
            get => _showErrorMessage;
            set
            {
                SetProperty(ref _showErrorMessage, value);
            }
        }

        /// <summary>
        /// It creates acccount. It's connected with Db.
        /// </summary>
        private async void CreateAccount()
        {
            try
            {
                if (String.Equals(Password,RepeatedPassword))
                {
                   var _approve = await _login.CreateAccount(PESEL, Name, Lastname, Password);

                    if (_approve)
                    {
                        ErrorMessage = "Konto założone pomyślnie.";
                    }
                    else
                        ErrorMessage = "Coś poszło nie tak.";

                    ShowErrorMessage = true;
                }
                else
                {
                    throw new ArgumentException("Wpisz poprawne powtórzone hasło.");
                }
                
            }
            catch(Exception e)
            {
                ErrorMessage = e.Message;
                ShowErrorMessage = true;
            }
        }
        #endregion
        public CreateAccViewModel()
        {
            _amountOfInstances++;
            _login = new Login();
        }

        #region Exit View
        public ICommand CloseCommand => new DelegateCommand(CloseMe);

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

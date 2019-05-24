using Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace ViewModel
{
    public class LoginViewModel : BindableBase
    {
        #region Private Fields
        private readonly ILogin _login;
        private bool _authorization = false;
        private string _error = string.Empty;
        private string _errorColor = ValidationColors.Valid;
        private string _password = string.Empty;
        private string _pesel = string.Empty;
        private bool _isLoading = false;

        #endregion

        #region Open Next Window
        public event Action OpenNewWindow;
        public event Action CloseWindow;

        protected virtual void OnWindowOn(Action handler)
        {
            handler?.Invoke();
        }
        #endregion

        /// <summary>
        /// Gets the pesel from UI
        /// </summary>
        public string Pesel
        {
            get => _pesel;
            set
            {
                if (_pesel != value)
                    SetProperty(ref _pesel, value);
            }
        }

        /// <summary>
        /// Gets the password from UI.
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
            }
        }

        /// <summary>
        /// Gets the AccountID
        /// </summary>
        public int AccountID
        {
            get => _login.AccountID;
        }

        /// <summary>
        /// It shows error message.
        /// </summary>
        public string ErrorMessage
        {
            get => _error;
            set
            {
                if (_error != value)
                {
                    SetProperty(ref _error, value);
                    this.ErrorMessageColor = ValidationColors.InValid;
                }
            }
        }

        /// <summary>
        /// Helpers to changes the Error Color.
        /// </summary>
        public string ErrorMessageColor
        {
            get => _errorColor;
            set
            {
                if (_errorColor != value)
                    SetProperty(ref _errorColor, value);
            }
        }

        /// <summary>
        /// Sets true if content is load.
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                SetProperty(ref _isLoading, value);
            }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
      

        public LoginViewModel()
        {
            _login = new Login();
            LoginCommand = new DelegateCommand(Authorization);
            CancelCommand = new DelegateCommand(CancelAuthorization);
        }

        /// <summary>
        /// It authorizes the account.
        /// </summary>
        private async void Authorization()
        {
            try
            {
                IsLoading = true;

                //It compares password from UI and password from DB
                 _authorization = await _login.Login(_pesel,_password);

                if (_authorization)
                {
                    IsLoading = false;
                    OnWindowOn(OpenNewWindow);
                    OnWindowOn(CloseWindow);
                }
                else
                {
                    ErrorMessage = "Wpisz poprawne dane.";
                    IsLoading = false;
                }
            }
            catch(Exception e)
            {
                ErrorMessage = e.Message;
                IsLoading = false;
            }
          }

        /// <summary>
        /// It resets the Pesel and Password in UI.
        /// </summary>
        private void CancelAuthorization()
        {
            this.Pesel = string.Empty;
            this.Password = string.Empty;
            IsLoading = false;
        }

        #region Create Account
        public ICommand AccountCommand => new DelegateCommand(CreateAccount);

        private object _selectedView = null;
        public object SelectedView
        {
            get => _selectedView;
            set
            {
                SetProperty(ref _selectedView, value);
            }
        }

        /// <summary>
        /// Creates Account ViewModel and Opens new view.
        /// </summary>
        private void CreateAccount()
        {
            if (CreateAccViewModel.AmountOfInstances == 0)
            {
                var _acc = new CreateAccViewModel();
                SelectedView = _acc;
                _acc.CloseControl += () => SelectedView = null;

            }
        }
        #endregion
    }
}

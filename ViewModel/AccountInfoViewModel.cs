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
    public class AccountInfoViewModel : BindableBase
    {
        #region Private fields
        private IAccount _acc;
        private bool _IsChangeAblePass = false;
        private bool _isAccepted = false;
        private string _oldPassword = string.Empty;
        private string _newPassword = string.Empty;
        private string _errorMessage = string.Empty;
        #endregion

        public string FullName => String.Format("{0} {1}", _acc.Name, _acc.LastName);
        public string PESEL => MainViewModel.Account.Pesel.ToString();
        public IAccount AccountInfo => _acc;

        #region Change Account's Password
        #region Passwords

        public string OldPassword
        {
            set
            {
                SetProperty(ref _oldPassword, value);
                IsChangeablePass = PassesIsEmpty;
            }
        }


        public string NewPassword
        {
            get => _newPassword;

            set
            {
                SetProperty(ref _newPassword, value);
                IsChangeablePass = PassesIsEmpty;
            }
        }
        #endregion

        //It sets IsEnabled in button
        //Button is disabled if passwords are empty.
        public bool IsChangeablePass
        {
            get => _IsChangeAblePass;
            set
            {
                SetProperty(ref _IsChangeAblePass, value);
            }
        }

        //It sets true if password has been changed.
        public bool IsAccepted
        {
            get => _isAccepted;
            set
            {
                SetProperty(ref _isAccepted, value);
            }
        }

        public ICommand Accepted => new DelegateCommand(() => { IsAccepted = false; });

        //This command is attached to button (change password), (zmien)
        public ICommand ChangePass => new DelegateCommand(ChangePassword);

        //It changes the password.
        private async void ChangePassword()
        {
            if (IsChangeablePass)
            {
                try
                {
                    if (_acc.CheckOldPassword(_oldPassword))
                    {
                        IsAccepted = await _acc.ChangePassword(_newPassword);

                        if (!IsAccepted)
                        {
                            ErrorMessage = "Nowe hasło nie może być takie samo.";
                        }
                        else
                        {
                            ErrorMessage = "Hasło zostało zmienione.";
                        }
                        IsAccepted = true;
                        OldPassword = string.Empty;
                        NewPassword = string.Empty;
                    }
                }
                catch(Exception e)
                {
                    ErrorMessage = e.Message;
                    IsAccepted = true;
                }
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                SetProperty(ref _errorMessage, value);
            }
        }

        //It checks password if they are empty or null. Returns false.
        private bool PassesIsEmpty => (!string.IsNullOrEmpty(_oldPassword) && !string.IsNullOrEmpty(_newPassword));

        #endregion

        public AccountInfoViewModel(IAccount account)
        {
            _acc = account;
        }
        
    }
}

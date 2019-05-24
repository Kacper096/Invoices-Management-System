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
    public class MainViewModel : BindableBase
    {
        #region Account Fields
        private static IAccount _account;
        private static readonly string _welcome = "Witaj ";
        #endregion

        #region Account Methods

        /// <summary>
        /// It's contains info about Account.
        /// </summary>
        public static IAccount Account
        {
            get => _account;
        }

        /// <summary>
        /// Welcome Title
        /// </summary>
        public static string WelcomeAccount
        {
            get => _welcome + _account.Name;
        }
        #endregion

        private object _CategoryVM;
        private object _AccountVM;
        private object _CompaniesVM;

        private object _selectedvm;

        /// <summary>
        /// Get's the current viewmodel to contentPresenter.
        /// </summary>
        public object SelectedViewModel
        {
            get => _selectedvm;
            set
            {
                SetProperty(ref _selectedvm, value);
            }
        }

        #region Command ViewModels
        public ICommand AccountCommand => new DelegateCommand(AccountVM);
        public ICommand FeesCommand => new DelegateCommand(FeesVM);
        public ICommand CompanyCommand => new DelegateCommand(CompanyVM);
        #endregion

        /// <summary>
        /// Sets the Views via viewmodels.
        /// </summary>
        private void AccountVM() => SelectedViewModel = _AccountVM;
        private void FeesVM() => SelectedViewModel = _CategoryVM;
        private void CompanyVM() => SelectedViewModel = _CompaniesVM;

        public MainViewModel(int accountID)
        {
            _account = new Account(accountID);
            _CategoryVM = new CategoryViewModel();
            _AccountVM = new AccountInfoViewModel(_account);
            _CompaniesVM = new CompanyViewModel();
            //It's default Main Page. 
            SelectedViewModel = _CategoryVM; 
        }

    }
}

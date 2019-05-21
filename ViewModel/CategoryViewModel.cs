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
    public class CategoryViewModel : BindableBase, IDisposable
    {
        #region Private Fields
        private readonly ICollection<Category> _categories;

        private object _selectedViewModel;
        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                SetProperty(ref _selectedViewModel, value);
            }
        }
        #endregion

        public ICollection<Category> Categories => _categories;

        public ICommand InvoiceCommand => new DelegateCommand<object>(SelectView);


        #region Invoices Management
       
        public void SelectView(object CategoryId)
        {
            if(CategoryId is int _catID)
            {
                 var _tempVM = new InvoicesViewModel(_catID);
                SelectedViewModel = _tempVM;
                _tempVM.CloseControl += () => SelectedViewModel = null;
            }
        }
        #endregion

        public CategoryViewModel()
        {
            _categories = new ManagerCollection().Categories;
        }

        /// <summary>
        /// Clears all datas in this ViewModel
        /// </summary>
        public void Dispose()
        {
            Categories.Clear();
            SelectedViewModel = null;
            GC.SuppressFinalize(this);
        }
    }
}

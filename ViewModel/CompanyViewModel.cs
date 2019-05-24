using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CompanyViewModel
    {
        private readonly ManagerCollection _manager;
        private readonly ICollection<Business> _businesses;

        public ICollection<Business> Companies
        {
            get
            {
                return _businesses;
            }
        }

        public CompanyViewModel()
        {
            _manager = new ManagerCollection();
            _businesses = _manager.GetAllCompanies();
        }
    }
}

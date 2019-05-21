using Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Category
    {
        private readonly int _id = 0;
        private readonly string _name = string.Empty;
        private readonly string _description = string.Empty;
        private readonly string _image = string.Empty;

        public int ID => _id;
        public string IDstring => _id.ToString();
        public string Name => _name;
        public string Description => _description;
        public string Image => _image;

        public Category(int Id,string Name, string Description)
        {
            _id = Id;
            _name = Name;
            _description = Description;
            _image = string.Format("/Views/Images/{0}.png", Name);
        }
    }
}

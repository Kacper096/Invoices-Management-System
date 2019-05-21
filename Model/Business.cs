using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Business
    {
        private readonly string _name = string.Empty;
        private readonly string _nip = string.Empty;
        private readonly string _regon = string.Empty;
        private readonly string _bankName = string.Empty;
        private readonly string _numberAccBank = string.Empty;
        private readonly Address _address;
        private readonly string _image = string.Empty;

        public Business(string name)
        {
            _name = name;
        }

        public Business(string name, string nip, string regon, string bankName, string numberAccBank, Address address)
        {
            _name = name;
            _nip = nip;
            _regon = regon;
            _bankName = bankName;
            _numberAccBank = numberAccBank;
            _address = address;
            _image = string.Format("/Views/Images/Business/{0}.png", _name.ToLowerInvariant());
        }

        public Business(string name, string nip, string regon, string bankName, string numberAccBank, string country,
            string city, string street, string postalCode)
        {
            _name = name;
            _nip = nip;
            _regon = regon;
            _bankName = bankName;
            _numberAccBank = numberAccBank;
            _address = new Address(country, city, street, postalCode);
            _image = string.Format("/Views/Images/Business/{0}.png", _name.ToLowerInvariant());

        }

        public string Name => _name;

        public string Nip => _nip;

        public string Regon => _regon;

        public string BankName => _bankName;

        public string NumberAccBank => _numberAccBank; 

        public Address Address => _address;

        public string AddressString => _address.ToString();

        public string Image => _image;
    }
}

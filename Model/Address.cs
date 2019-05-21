using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Address
    {
        private readonly string _country = string.Empty;
        private readonly string _city = string.Empty;
        private readonly string _street = string.Empty;
        private readonly string _postalCode = string.Empty;

        public Address(string country, string city, string street,  string postalCode)
        {
            _country = country;
            _city = city;
            _street = street;
            _postalCode = postalCode;
        }

        #region Properties
        public string Country => _country;

        public string City => _city;

        public string Street => _street;

        public string PostalCode => _postalCode;

        #endregion
        public override string ToString()
        {
            string _pattern = "Kraj: {0}" +
                Environment.NewLine + "Miasto: {1}" +
                Environment.NewLine + "Ulica: {2}" +
                Environment.NewLine + "Kod pocztowy: {3}";

            return string.Format(_pattern, _country, _city, _street, _postalCode);
        }
    }
}

using Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Account : IAccount
    {
        private readonly AccountDataAccess _account;
        private int _id;
        private string _pesel;
        private string _name = string.Empty;
        private string _lastname = string.Empty;
        private string _paidFees = string.Empty;
        private string _unpaidFees = string.Empty;

        public int ID => _id;
        public string Name => _name;
        public string LastName => _lastname;
        public string Pesel => _pesel;
        public string PaidFees => _paidFees;
        public string UnPaidFees => _unpaidFees;

        public Account(int accountID)
        {
            _account = new AccountDataAccess();
            SetAccount(accountID);
            SetPaidUnPaidFees(accountID);
        }

        /// <summary>
        /// Change the account's password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ChangePassword(string password)
        {
            if (!PasswordVerification.CanBeAPassword(password))
                throw new ArgumentException("Zły format hasła.");

            try
            {
                bool _isPassEqual = CheckOldPassword(password);
                if(_isPassEqual)
                {
                    //If password is the same like that in database and returns false.
                    return !_isPassEqual;
                }

                return await SetsNewPassword(password);
            }
            catch(ArgumentNullException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Checks password from db and password attempted.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckOldPassword(string password)
        {
            try
            {
                return PasswordVerification.CompareHashedPassword(password, _account.GetAccountPassword(ID));
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// It's initalizes the fields data.
        /// </summary>
        /// <param name="accountID"></param>
        private void SetAccount(int accountID)
        {
            try
            {
                AccountDTO account = null;
                account = _account.GetAccount(accountID);

                _id = account.Id;
                _name = account.Name;
                _lastname = account.LastName;
                _pesel = account.PESEL.ToString();
            }
            catch(ArgumentNullException)
            {
                throw;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Sets amount of unpaid and paid invoices.
        /// </summary>
        /// <param name="accountID"></param>
        private void SetPaidUnPaidFees(int accountID)
        {
            try
            {
                _paidFees = string.IsNullOrEmpty(_account.GetAccountInfo(accountID).PaidFees.ToString()) == true ? "0" : _account.GetAccountInfo(accountID).PaidFees.ToString();
                _unpaidFees = string.IsNullOrEmpty(_account.GetAccountInfo(accountID).UnPaidFees.ToString()) == true ? "0" : _account.GetAccountInfo(accountID).UnPaidFees.ToString();
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Sets new Password.
        /// </summary>
        /// <param name="pass"></param>
        /// <returns></returns>
        private async Task<bool> SetsNewPassword(string pass)
        {
            try
            {
                int _res = await _account.SetsNewPassword(ID, PasswordVerification.CreatePasswordHash(pass));

                if (_res == 1)
                    return true;

                return false;
            }
            catch(Exception)
            {
                throw;
            }
        }
       
    }
}

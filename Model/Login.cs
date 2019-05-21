using Model.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Login : ILogin
    {
        private readonly IAccountDataAccess _account;
        private int _accountID;

        public Login()
        {
            _account = new AccountDataAccess();
        }

        public int AccountID { get => _accountID;  }

        /// <summary>
        /// Logs to your account.
        /// </summary>
        /// <param name="pesel">Number Pesel</param>
        /// <param name="password">Enter the password.</param>
        /// <returns></returns>
        async Task<bool> ILogin.Login(string pesel, string password)
        {
            bool _result = false;

            try
            {
                long PESEL = Convert.ToInt64(pesel);
                LoginDTO login = await _account.GetAccount(PESEL) ?? throw new ArgumentNullException("Brak Użytkownika.");
                _accountID = login.AccountID;
                
               return _result = PasswordVerification.CompareHashedPassword(password, login.Password);
            }
            catch (FormatException)
            {
                throw new FormatException("PESEL niepoprawny.");
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}

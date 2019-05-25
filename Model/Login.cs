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

        /// <summary>
        /// Creates new Account
        /// </summary>
        /// <param name="pesel">Pesel must have 11 chars.</param>
        /// <param name="name"></param>
        /// <param name="lastname"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> CreateAccount(string pesel, string name, string lastname, string password)
        {
            bool _result = false;

            try
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentException("Wpisz imię.");

                if (string.IsNullOrEmpty(lastname))
                    throw new ArgumentException("Wpisz nazwisko.");

                //Checks the PESEL
                if (!PESEL.Check(pesel.Trim()))
                    throw new ArgumentException("Wpisz poprawny PESEL");

                //Checks the password
                if (!PasswordVerification.CanBeAPassword(password.Trim()))
                    throw new ArgumentException("Hasło nie spełnia wymagań.");

                var _newAcc = new LoginDTO()
                    {
                        PESEL = Int64.Parse(pesel),
                        Name = name ?? ,
                        LastName = lastname,
                        Password = PasswordVerification.CreatePasswordHash(password.Trim())
                    };

                int _callback = await _account.CreateAccount(_newAcc);

                if (_callback == 1)
                    _result = true;

                
                
                return _result;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}

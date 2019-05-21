using System;
using System.Linq;

namespace Model.Service
{
    public class AccountService : IAccountService
    {
        /// <summary>
        /// It's gets the Account's pesel and password.
        /// </summary>
        /// <param name="PESEL"></param>
        /// <returns></returns>
        public AccountDTO GetAccount(long PESEL)
        {
            using (var context = new OplatyEntities1())
            {
                AccountDTO account = null;
                try
                {
                     var query = context.Klienci.Where(x => x.PESEL == PESEL)
                      .Select(x => new AccountDTO
                      {
                          AccountID = x.KlientID,
                          PESEL = x.PESEL,
                          Password = x.Haslo
                      }).AsEnumerable()
                      .FirstOrDefault();

                    account = query ?? throw new ArgumentNullException("Bak użytkownika.");                 
                }
                catch(ArgumentNullException e)
                {
                    throw new ArgumentNullException($"Błąd bazy: {e.Message}");
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message.ToString());
                }

                return account;
            }
        }
    }
}

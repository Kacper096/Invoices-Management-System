using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataAccess
{
    public class AccountDataAccess : IAccountDataAccess
    {
        /// <summary>
        /// It gets the Account's pesel and password. It's necessary to Log IN.
        /// </summary>
        /// <param name="PESEL"></param>
        /// <returns></returns>
        public async Task<LoginDTO> GetAccount(long PESEL)
        {
            return await Task.Run(() =>
               {
                   if (PESEL.ToString().Length < 11)
                       throw new ArgumentException("PESEL nie może mieć mniej niż 11 znaków.");
                   using (var context = new OplatyEntities())
                   {
                       LoginDTO account = null;
                       try
                       {
                           var query = context.Klienci.Where(x => x.PESEL == PESEL)
                            .Select(x => new LoginDTO
                            {
                                AccountID = x.KlientID,
                                PESEL = x.PESEL,
                                Password = x.Haslo
                            }).AsEnumerable()
                            .FirstOrDefault();

                           account = query ?? throw new ArgumentNullException("Brak użytkownika.");
                       }
                       catch (ArgumentNullException)
                       {
                           throw;
                       }
                       catch (Exception e)
                       {
                           throw new Exception(e.Message.ToString());
                       }

                       return account;
                   }
               });
        }

        /// <summary>
        /// It gets the information about account.
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountDTO GetAccount(int accountID)
        {
            AccountDTO account = null;
            using (var context = new OplatyEntities())
            {
                try
                {
                    var query = context.Klienci.Where(x => x.KlientID == accountID)
                      .Select(x => new AccountDTO
                      {
                          Id = x.KlientID,
                          Name = x.Imie,
                          LastName = x.Nazwisko,
                          PESEL = x.PESEL
                      })
                      .FirstOrDefault();

                    account = query ?? throw new ArgumentNullException("Brak użytkownika");
                }
                catch(ArgumentNullException)
                {
                    throw;
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    context.Dispose();
                }
            }

            return account;
        }

        /// <summary>
        /// It gets the information about account.
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public AccountInfoDTO GetAccountInfo(int accountID)
        {
            using (var context = new OplatyEntities())
            {
                try
                {
                    var query = context
                      .Klienci
                      .Where(x => x.KlientID == accountID)
                      .Where(x => x.Faktury.Any())
                      .AsEnumerable()
                      .Select(x => new AccountInfoDTO
                      {
                          PaidFees = x.Faktury.Where(y => y.Status.ToString() == "Opłacone").Count(),
                          UnPaidFees = x.Faktury.Where(y => y.Status.ToString() != "Opłacone").Count()
                      })
                      .FirstOrDefault();

                    return query;
                }
                catch(ArgumentNullException)
                {
                    throw new ArgumentNullException("Account info is empty.");
                }
                finally
                {
                    context.Dispose();
                }
            }
        }

        /// <summary>
        /// It gets the information about user's invoices.
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceBusinessDTO> GetInvoicesInfo(int accountID, int categoryID)
        {
            using (var context = new OplatyEntities())
            {
                try
                {
                    var query = context.Faktury
                      .Where(x => x.KlientID == accountID)
                      .Where(x => x.KategoriaID == categoryID)
                      .AsEnumerable()
                      .Select(x => new InvoiceBusinessDTO
                      {
                          ID = x.FakturaID,
                          CategoryID = x.KategoriaID,
                          Number = x.Numer,
                          InvoiceDate = x.DataWystawienia,
                          PaidDate = x.DataZaplaty.HasValue ? x.DataZaplaty.Value : new DateTime(),
                          Deadline = x.TerminPlatnosci,
                          Paid = x.Zaplacono.HasValue ? x.Zaplacono.Value : 0,
                          ToPay = x.DoZaplaty,
                          ToPayInWords = x.DoZaplatySlownie.ToString(),
                          LeftToPay = x.PozostaloDoZaplaty.HasValue ? x.PozostaloDoZaplaty.Value : 0,
                          Status = x.Status,
                          Currency = x.Waluta.Contains("Zł") ? "Zł" : "Euro",
                          BusinessName = x.Firmy.NazwaFirmy,
                          NIP = x.Firmy.NIP,
                          Regon = x.Firmy.Regon,
                          Country = x.Firmy.Kraj,
                          City = x.Firmy.Miasto,
                          Street = x.Firmy.Adres,
                          PostalCode = x.Firmy.KodPocztowy,
                          BuisnessBankName = x.Firmy.NazwaBanku,
                          BuisnessBankAccountNumber = x.Firmy.NumerKontaBank
                      });

                    return query.ToList();
                }
                catch(ArgumentNullException)
                {
                    throw new ArgumentNullException("No invoices.");
                }
                finally
                {
                    context.Dispose();
                }
            }
        }

        /// <summary>
        /// It gets the information about invoice. It contains amount, netto value, Vat value etc.
        /// </summary>
        /// <param name="invoiceID"></param>
        /// <returns></returns>
        public IEnumerable<InvoiceDetailsDTO> GetInvoiceDetails(int invoiceID)
        {
         
            using (var context = new OplatyEntities())
            {
                try
                {
                    var query = context.FakturaSzczegoly
                          .Where(x => x.FakturaID == invoiceID)
                          .Select(x => new InvoiceDetailsDTO
                          {
                              ServiceName = x.Uslugi.Nazwa,
                              Amount = x.Ilosc,
                              UnitPrice = x.CenaJednostkowa,
                              NetValue = x.WartoscNetto,
                              VatRate = x.StawkaVAT,
                              VatValue = x.WartoscVAT,
                              Value = x.WartoscBrutto
                          });



                    return query.ToList();
                }
                catch (ArgumentNullException)
                {
                    throw new ArgumentNullException("Invoice Details is empty.");
                }
                catch (Exception)
                { throw; }
                finally
                {
                    context.Dispose();
                }
            }
            
        }

        /// <summary>
        /// Gets the account password.
        /// </summary>
        /// <param name="accountID"></param>
        /// <returns></returns>
        public byte[] GetAccountPassword(int accountID)
        {
            using (var context = new OplatyEntities())
            {
                try
                {
                    var query = context.Klienci.Where(x => x.KlientID == accountID).Single().Haslo;

                    return query ?? throw new ArgumentNullException("We can't find your password.");
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
        }

        /// <summary>
        /// It pays invoice.
        /// </summary>
        /// <param name="invoiceID">Your invoice to pay.</param>
        /// <param name="value">Money to pay.</param>
        public async Task<bool> PayInvoice(int invoiceID, decimal value)
        {
            using (var context = new OplatyEntities())
            {
                try
                {
                    return await Task.Run(() =>
                    {
                        int _result = context.ZaplacFakture(value, invoiceID);

                        if (_result > 0)
                            return true;
                        return false;

                    });

                }
                catch(InvalidOperationException)
                {
                    throw new InvalidOperationException("Your price is wrong.");
                }
                catch(ArgumentNullException)
                {
                    throw new ArgumentNullException("Parameter cannot be empty.");
                }
                catch(ArgumentException)
                {
                    throw new ArgumentException("Wrong Parameters.");
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    context.Dispose();
                }
            }
        }

        /// <summary>
        /// Sets new password.
        /// </summary>
        /// <param name="accountID"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public async Task<int> SetsNewPassword(int accountID, byte[] pass)
        {
            using (var context = new OplatyEntities())
            {
                try
                {
                    var account = context.Klienci.SingleOrDefault(x => x.KlientID == accountID);

                    if(account != null)
                        account.Haslo = pass;

                    int _result =  await context.SaveChangesAsync();

                    return _result;
                }
                catch(ArgumentNullException)
                {
                    throw new ArgumentNullException("Your account isn't available.");
                }
                catch(InvalidOperationException)
                {
                    throw new InvalidOperationException("You can't  do this operation");
                }
                catch(Exception)
                {
                    throw;
                }
            }
        }
    }
}

﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model.DataAccess
{
    public interface IAccountDataAccess
    {
        Task<int> CreateAccount(LoginDTO accountInfo);
        AccountDTO GetAccount(int accountID);
        Task<LoginDTO> GetAccount(long PESEL);
        AccountInfoDTO GetAccountInfo(int accountID);
        byte[] GetAccountPassword(int accountID);
        IEnumerable<BusinessDTO> GetComapnies();
        IEnumerable<InvoiceDetailsDTO> GetInvoiceDetails(int invoiceID);
        IEnumerable<InvoiceBusinessDTO> GetInvoicesInfo(int accountID, int categoryID);
        Task<bool> PayInvoice(int invoiceID, decimal value);
        Task<int> SetsNewPassword(int accountID, byte[] pass);
    }
}
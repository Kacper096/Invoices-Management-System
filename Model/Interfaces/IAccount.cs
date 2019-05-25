using System.Threading.Tasks;

namespace Model
{
    public interface IAccount
    {
        int ID { get; }
        string LastName { get; }
        string Name { get; }
        string PaidFees { get; }
        string Pesel { get; }
        string UnPaidFees { get; }

        Task<bool> ChangePassword(string password);
        bool CheckOldPassword(string password);
    }
}
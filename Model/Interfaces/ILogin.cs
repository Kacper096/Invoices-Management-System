using System.Threading.Tasks;

namespace Model
{
    public interface ILogin
    {
        int AccountID { get; }

        Task<bool> Login(string pesel, string password);

        Task<bool> CreateAccount(string pesel, string name, string lastname, string password);
    }
}
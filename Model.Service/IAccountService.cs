
namespace Model.Service
{
    public interface IAccountService
    {
        AccountDTO GetAccount(long clientID);
    }
}
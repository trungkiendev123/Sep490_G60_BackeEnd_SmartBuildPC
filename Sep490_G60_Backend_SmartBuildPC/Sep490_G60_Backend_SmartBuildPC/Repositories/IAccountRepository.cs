using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Requests;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public interface IAccountRepository
    {
        public void register(RegisterFormRequest request);

        public Task<Account> GetAccount(string email,string password);

        public Task<Account> GetAccountByEmail(string email);

        public void Update(Account account);

        public void AddAccount(AddAccountRequest account);
    }
}

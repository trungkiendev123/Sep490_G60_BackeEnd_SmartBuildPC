using Microsoft.EntityFrameworkCore;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Requests;
using Sep490_G60_Backend_SmartBuildPC.Responses;
using Sep490_G60_Backend_SmartBuildPC.Service;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SMARTPCContext _context;
        public AccountRepository(SMARTPCContext context)
        {
            _context = context;
        }
        public void register(RegisterFormRequest request)
        {
            try
            {
                Account account = new Account()
                {
                    AccountId = Guid.NewGuid(),
                    Password = DecryptPassword.ComputeMD5Hash(request.Password),
                    Username = request.Username,
                    Email = request.Email,
                    AccountType = "CUSTOMER"
                };
                _context.Accounts.Add(account);
                _context.SaveChanges();


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while do this action", ex);
            }
        }
    }
}

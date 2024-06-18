using Microsoft.EntityFrameworkCore;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Requests;
using Sep490_G60_Backend_SmartBuildPC.Service;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly SMARTPCContext _context;
        public CartRepository(SMARTPCContext context)
        {
            _context = context;
        }
        public void AddCart(string email,AddCartRequest request)
        {
            try
            {
                var account =  _context.Accounts.FirstOrDefault(x => x.Email.Equals(email));
                var customer =  _context.Customers.FirstOrDefault(x => x.AccountId.Equals(account.AccountId));
                var cartUser =  _context.Carts.FirstOrDefault(x => x.CustomerId.ToString().ToUpper().Equals(customer.CustomerId.ToString().ToUpper()) && x.ProductId == request.ProductID);
                if(cartUser == null)
                {
                    Cart cart = new Cart()
                    {
                        CustomerId = customer.CustomerId,
                        ProductId = request.ProductID,
                        Quantity = request.Quantity
                    };
                    _context.Carts.Add(cart);
                }
                else
                {
                    cartUser.Quantity += request.Quantity;
                    _context.Carts.Update(cartUser);
                }
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while do this action", ex);
            }
        }
    }
}

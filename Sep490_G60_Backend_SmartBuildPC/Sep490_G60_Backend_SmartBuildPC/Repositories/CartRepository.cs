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
        public async void AddCart(string email,AddCartRequest request)
        {
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(x => x.Email.Equals(email));
                var customer = await _context.Customers.FirstOrDefaultAsync(x => x.AccountId.Equals(account.AccountId));
                var cartUser = await _context.Carts.FirstOrDefaultAsync(x => x.CustomerId.Equals(customer.CustomerId) && x.ProductId == request.ProductID);
                if(cartUser == null)
                {
                    Cart cart = new Cart()
                    {
                        CustomerId = customer.CustomerId,
                        ProductId = request.ProductID,
                        Quantity = request.Quantity
                    };
                    _context.Carts.Add(cart);
                    _context.SaveChanges();
                }
                else
                {
                    cartUser.Quantity += request.Quantity;
                    _context.Carts.Update(cartUser);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while do this action", ex);
            }
        }
    }
}

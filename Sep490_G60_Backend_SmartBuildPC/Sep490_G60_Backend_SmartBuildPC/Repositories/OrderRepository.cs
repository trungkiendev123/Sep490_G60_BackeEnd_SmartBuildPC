using Microsoft.EntityFrameworkCore;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Responses;
using System.Threading.Tasks;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SMARTPCContext _context;

        public OrderRepository(SMARTPCContext context)
        {
            _context = context;
        }

        

        public async Task UpdateOrderStatusAsync(UpdateOrderStatusDTO updateOrderStatusDto)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == updateOrderStatusDto.OrderID);
            if (order != null)
            {
                order.OrderStatus = updateOrderStatusDto.OrderStatus;
                await _context.SaveChangesAsync();
            }
        }

        
        


    }
}

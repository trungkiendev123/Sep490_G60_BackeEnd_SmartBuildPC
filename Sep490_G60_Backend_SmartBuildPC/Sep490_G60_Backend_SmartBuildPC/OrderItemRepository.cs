using Microsoft.EntityFrameworkCore;
using Sep490_G60_Backend_SmartBuildPC.Models;
using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly SMARTPCContext _context;

        public OrderItemRepository(SMARTPCContext context)
        {
            _context = context;
        }

        public async Task<List<OrderItemDTO>> GetOrderItems(int orderId)
        {
            var orderItems = await _context.OrderItems
                .Where(oi => oi.OrderId == orderId)
                .Include(oi => oi.Warranty)
                    .ThenInclude(pw => pw.Product)
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.Customer)
                .Select(oi => new OrderItemDTO
                {
                    OrderItemID = oi.OrderItemId,
                    OrderDate = oi.Order.OrderDate,
                    OrderAddress = oi.Order.OrderAddress,
                    OrderStatus = oi.Order.OrderStatus,
                    ReceiveDate = (DateTime)oi.Order.ReceiveDate,
                    TotalAmount = oi.Order.TotalAmount,
                    Quantity = oi.Quantity,
                    PricePerItem = oi.PricePerItem,
                    ProductName = oi.Warranty.Product.ProductName,
                    Description = oi.Warranty.Product.Description,
                    Price = oi.Warranty.Product.Price,
                    Warranty = oi.Warranty.Product.Warranty,
                    Brand = oi.Warranty.Product.Brand,
                    WarrantySentDate = (DateTime)oi.Warranty.WarrantySentDate,
                    WarrantyReceive = (DateTime)oi.Warranty.WarrantyReceive,
                    CustomerName = oi.Order.Customer.FullName,
                    CustomerPhone = oi.Order.Customer.Phone
                })
                .ToListAsync();

            return orderItems;
        }





        public async Task<List<OrderItemDTO>> GetAllOrderItems(int page, int pageSize)
        {
            var orderItems = await _context.OrderItems
                .Include(oi => oi.Warranty)
                    .ThenInclude(pw => pw.Product)
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.Customer)
                .OrderByDescending(oi => oi.OrderItemId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(oi => new OrderItemDTO
                {
                    OrderItemID = oi.OrderItemId,
                    OrderDate = oi.Order.OrderDate,
                    OrderAddress = oi.Order.OrderAddress,
                    OrderStatus = oi.Order.OrderStatus,
                    ReceiveDate = (DateTime)oi.Order.ReceiveDate,
                    TotalAmount = oi.Order.TotalAmount,
                    Quantity = oi.Quantity,
                    PricePerItem = oi.PricePerItem,
                    ProductName = oi.Warranty.Product.ProductName,
                    Description = oi.Warranty.Product.Description,
                    Price = oi.Warranty.Product.Price,
                    Warranty = oi.Warranty.Product.Warranty,
                    Brand = oi.Warranty.Product.Brand,
                    WarrantySentDate = (DateTime)oi.Warranty.WarrantySentDate,
                    WarrantyReceive = (DateTime)oi.Warranty.WarrantyReceive,
                    CustomerName = oi.Order.Customer.FullName,
                    CustomerPhone = oi.Order.Customer.Phone
                })
                .ToListAsync();

            return orderItems;


        }
    }
}

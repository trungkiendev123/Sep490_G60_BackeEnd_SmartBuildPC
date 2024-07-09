using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderDTO> GetOrderByIdAsync(int orderId);
        Task UpdateOrderStatusAsync(UpdateOrderStatusDTO updateOrderStatusDto);
        Task<BillDTO> GetBillByOrderIdAsync(int orderId);
        Task<AccountDTO> GetAccountByOrderIdAsync(int orderId);
    }
}

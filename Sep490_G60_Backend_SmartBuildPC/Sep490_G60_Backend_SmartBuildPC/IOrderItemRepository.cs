using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItemDTO>> GetOrderItems(int orderId);
        Task<List<OrderItemDTO>> GetAllOrderItems(int page, int pageSize);
    }
}

using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public interface IBillRepository
    {
        Task CreateBillAsync(CreateBillDTO createBillDTO);
        Task<List<BillDTO>> GetAllBillsAsync();
    }
}

using Sep490_G60_Backend_SmartBuildPC.Responses;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public interface IStoreRepository
    {
        public Task<List<StoreDTO>> ListStore();
    }
}

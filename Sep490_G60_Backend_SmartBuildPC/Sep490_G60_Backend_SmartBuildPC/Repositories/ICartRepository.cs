using Sep490_G60_Backend_SmartBuildPC.Requests;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public interface ICartRepository
    {
        public void AddCart(AddCartRequest request);
    }
}

using Sep490_G60_Backend_SmartBuildPC.Requests;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public interface ICartRepository
    {
        public void AddCart(string email, ChangeCartRequest request);

        public void UpdateCart(string email, ChangeCartRequest request);
    }
}

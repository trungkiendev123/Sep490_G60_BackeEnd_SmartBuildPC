using Sep490_G60_Backend_SmartBuildPC.Requests;

namespace Sep490_G60_Backend_SmartBuildPC.Repositories
{
    public interface IAccountRepository
    {
        public void register(RegisterFormRequest request);
    }
}

namespace Sep490_G60_Backend_SmartBuildPC.Requests
{
    public class RegisterFormRequest
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;


    }
}

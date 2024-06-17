namespace Sep490_G60_Backend_SmartBuildPC.Requests
{
    public class AddAccountRequest
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FullName { get; set; }

        public int? StoreID { get; set; }

        public string AccounType { get; set; } = null!;
    }
}

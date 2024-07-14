namespace Sep490_G60_Backend_SmartBuildPC.Responses
{
    public class CreateBillDTO
    {
        public int? OrderId { get; set; }
        public DateTime? BillDate { get; set; }
        public int? TaxIn { get; set; }
        public string? Address { get; set; }
    }
}

namespace Sep490_G60_Backend_SmartBuildPC.Responses
{
    public class CreateProductDTO
{
    public int CategoryID { get; set; }  
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Warranty { get; set; }
    public string Brand { get; set; }
    public string Tag { get; set; }
    public int TDP { get; set; }
    public string ImageLink { get; set; }
    public List<StoreStockDTO> StoreStocks { get; set; }
}

public class StoreStockDTO
{
    public int StoreID { get; set; } 
    public int StockQuantity { get; set; }
}
}

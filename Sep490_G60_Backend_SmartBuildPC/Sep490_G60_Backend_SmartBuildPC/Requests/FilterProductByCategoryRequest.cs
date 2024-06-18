namespace Sep490_G60_Backend_SmartBuildPC.Requests
{
    public class FilterProductByCategoryRequest
    {
        public int cate_id { get; set; }

        public List<string> filters { get; set; }
    }
}

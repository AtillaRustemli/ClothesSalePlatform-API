namespace ClothesSalePlatform.DTOs.ProductDTOs
{
    public class ReturnProductListDto
    {
        public int TotalCount { get; set; }
        public List<ReturnProductDto> Items { get; set; }
    }
}

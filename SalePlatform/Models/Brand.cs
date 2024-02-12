namespace ClothesSalePlatform.Models
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public int FoundedYear { get; set; }
        public string Description { get; set; }
        public string FoundedCountry { get; set; }
        public List<Product> Products { get; set; }
    }
}

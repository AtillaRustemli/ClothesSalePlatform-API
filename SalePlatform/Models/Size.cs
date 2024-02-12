namespace ClothesSalePlatform.Models
{
    public class Size:BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Product { get; set; }
    }
}

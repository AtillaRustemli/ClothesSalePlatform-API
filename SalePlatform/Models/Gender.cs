namespace ClothesSalePlatform.Models
{
    public class Gender:BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Product { get; set; }
    }
}

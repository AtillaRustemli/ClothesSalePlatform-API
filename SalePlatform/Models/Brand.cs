using ClothesSalePlatform.Models.ReletionTables;

namespace ClothesSalePlatform.Models
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public int FoundedYear { get; set; }
        public string Description { get; set; }
        public string FoundedCountry { get; set; }
        public List<Product> Products { get; set; }
        public List<BrandCategory> BrandCategory { get; set; }
        public List<BrandStore> BrandStore { get; set; }
    }
}

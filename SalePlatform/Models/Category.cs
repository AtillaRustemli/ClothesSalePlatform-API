using ClothesSalePlatform.Models.ReletionTables;

namespace ClothesSalePlatform.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }
        public List<BrandCategory> BrandCategory { get; set; }
        public List<StoreCategory> StoreCategory { get; set; }

    }
}

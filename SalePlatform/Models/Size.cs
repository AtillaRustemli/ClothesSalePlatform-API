using ClothesSalePlatform.Models.ReletionTables;

namespace ClothesSalePlatform.Models
{
    public class Size:BaseEntity
    {
        public string Name { get; set; }
        public List<ProductSize> ProductSize { get; set; }
    }
}

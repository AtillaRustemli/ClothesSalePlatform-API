using ClothesSalePlatform.Models.ReletionTables;

namespace ClothesSalePlatform.Models
{
    public class Store:BaseEntity
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public List<Product> Products { get; set; }
        public string OpeningHours { get; set; }
        public string ClosingHours { get; set; }
        public List<BrandStore> BrandStore { get; set; }
        public List<StoreCategory> StoreCategory { get; set; }
        public List<StoreSubscriber> StoreSubscriber { get; set; }


    }
}

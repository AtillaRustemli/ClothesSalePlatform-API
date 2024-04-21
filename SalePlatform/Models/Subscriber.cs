using ClothesSalePlatform.Models.ReletionTables;

namespace ClothesSalePlatform.Models
{
    public class Subscriber:BaseEntity
    {
        public string Email { get; set; }
        public List<BrandSubscriber> BrandSubscriber { get; set; }
        public List<CategorySubscriber> CategorySubscriber { get; set; }
        public List<StoreSubscriber> StoreSubscriber { get; set; }
    }
}

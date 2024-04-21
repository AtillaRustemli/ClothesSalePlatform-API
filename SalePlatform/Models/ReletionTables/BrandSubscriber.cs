namespace ClothesSalePlatform.Models.ReletionTables
{
    public class BrandSubscriber:BaseEntity
    {
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}

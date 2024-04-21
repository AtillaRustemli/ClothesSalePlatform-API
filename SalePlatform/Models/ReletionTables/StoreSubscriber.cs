namespace ClothesSalePlatform.Models.ReletionTables
{
    public class StoreSubscriber:BaseEntity
    {
        public int SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}

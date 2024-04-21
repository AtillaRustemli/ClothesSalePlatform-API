namespace ClothesSalePlatform.Models.ReletionTables
{
    public class CategorySubscriber:BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int SubscriberId { get; set; }
        public Subscriber Subscriber { get; set; }
    }
}

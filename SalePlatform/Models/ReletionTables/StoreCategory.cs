namespace ClothesSalePlatform.Models.ReletionTables
{
    public class StoreCategory:BaseEntity
    {
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

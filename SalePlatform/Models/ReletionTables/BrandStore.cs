namespace ClothesSalePlatform.Models.ReletionTables
{
    public class BrandStore:BaseEntity
    {
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

    }
}

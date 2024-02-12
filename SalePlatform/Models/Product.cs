namespace ClothesSalePlatform.Models
{
    public class Product:BaseEntity
    {
       
        public string Name { get; set; }
        public string Color { get; set; }
        public int ProductCount { get; set; }
        public double Price { get; set; }
        public bool InStock { get; set; }
        public int GenderId { get; set; }
        public Gender Gender { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int SizeId { get; set; }
        public Size Size { get; set; }
    }
}

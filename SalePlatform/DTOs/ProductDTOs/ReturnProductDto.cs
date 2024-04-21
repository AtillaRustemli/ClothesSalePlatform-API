namespace ClothesSalePlatform.DTOs.ProductDTOs
{
    public class ReturnProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int ProductCount { get; set; }
        public bool InStock { get; set; }
        public double Price { get; set; }
        public CategoryInProductDTO categoryInProductDTO { get; set; }
        public List<SizeInProductDTO> sizeInProductDTO { get; set; }
        public BrandInProductDTO brandInProductDTO { get; set; }
        public GenderInProductDTO genderInProductDTO { get; set; }
        public StoreInProductDTO storeInProductDTO { get; set; }
        public class CategoryInProductDTO
        {
            public string Name { get; set; }
            public int ProductCount { get; set; }

        }
        public class SizeInProductDTO
        {
            public string Name { get; set; }
            public int ProductCount { get; set; }

        }
        public class BrandInProductDTO
        {
            public string Name { get; set; }
            public int ProductCount { get; set; }

        }
        public class GenderInProductDTO
        {
            public string Name { get; set; }
            public int ProductCount { get; set; }

        }
        public class StoreInProductDTO
        {
            public string Name { get; set; }
            public int ProductCount { get; set; }

        }
    }
}

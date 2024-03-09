namespace ClothesSalePlatform.DTOs.CategoryDTOs
{
    public class ReturnCategoryDto
    {
        public string Name{ get; set; }
        public int ProductCount { get; set; }
        public BrandInCategoryDto[] BrandInCategoryDto { get; set; }
        public StoreInCategoryDto[] StoreInCategoryDto { get; set; }
    }

    public class BrandInCategoryDto
    {
        public string Name { get; set; }
        public int FoundedYear { get; set; }
        public string Description { get; set; }
        public string FoundedCountry { get; set; }
        public int CategoryProductCount { get; set; }
    }
    public class StoreInCategoryDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string OpeningHours { get; set; }
        public string ClosingHours { get; set; }
        public int CategoryProductCount { get; set; }
    }

}

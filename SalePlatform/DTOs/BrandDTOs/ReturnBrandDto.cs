using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace ClothesSalePlatform.DTOs.BrandDTOs
{
    public class ReturnBrandDto
    {
        public ReturnBrandDto()
        {
            CategoriesInBrandDto = new();
        }
        public string Name { get; set; }
        public int FoundedYear { get; set; }
        public string Description { get; set; }
        public string FoundedCountry { get; set; }
        public int ProductCount { get; set; }
        public StoreInBrandDto[] StoresInBrandDto { get; set; }
        public List<CategoryInBrandDto> CategoriesInBrandDto { get; set; }


    }
    public class StoreInBrandDto
    {
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }
    public class CategoryInBrandDto
    {
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }
}

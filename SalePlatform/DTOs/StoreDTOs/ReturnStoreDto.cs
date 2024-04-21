using ClothesSalePlatform.Models.ReletionTables;
using ClothesSalePlatform.Models;

namespace ClothesSalePlatform.DTOs.StoreDTOs
{
    public class ReturnStoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int ProductCount { get; set; }
        public string OpeningHours { get; set; }
        public string ClosingHours { get; set; }
        public List<BrandInStoreDto> BrandInStoreDto { get; set; }
        public List<CategoryInStoreDto> CategoryInStoreDto { get; set; }
    }
    public class BrandInStoreDto
    {
        public string Name { get; set; }
        public string FoundedYear { get; set; }
        public int ProductCount { get; set; }

    }
    public class CategoryInStoreDto
    {
        public string Name { get; set; }
        public int ProductCount { get; set; }

    }
}

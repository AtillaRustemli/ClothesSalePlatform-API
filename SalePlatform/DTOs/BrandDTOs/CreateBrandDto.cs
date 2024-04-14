using ClothesSalePlatform.Models.ReletionTables;
using ClothesSalePlatform.Models;

namespace ClothesSalePlatform.DTOs.BrandDTOs
{
    public class CreateBrandDto
    {
        public string Name { get; set; }
        public int FoundedYear { get; set; }
        public string Description { get; set; }
        public string FoundedCountry { get; set; }
        public List<int> Category { get; set; }
        public List<int> Store { get; set; }
    }
}

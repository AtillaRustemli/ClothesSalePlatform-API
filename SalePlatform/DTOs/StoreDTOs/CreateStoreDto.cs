using ClothesSalePlatform.Models;
using ClothesSalePlatform.Models.ReletionTables;

using ClothesSalePlatform.Models;
using ClothesSalePlatform.Enums;

namespace ClothesSalePlatform.DTOs.StoreDTOs
{
    public class CreateStoreDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int ProductCount { get; set; }
        public string OpeningHours { get; set; }
        public string ClosingHours { get; set; }
        public List<int> Brands { get; set; }
        public List<int> Categories { get; set; }
        public ChoiceEnum Choice { get; set; }
    }
}

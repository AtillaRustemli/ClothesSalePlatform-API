namespace ClothesSalePlatform.DTOs.CategoryDTOs
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; }
        public int[] Store { get; set; }
        public int[] Brand { get; set; }
        public int[] Products { get; set; }
    }
}

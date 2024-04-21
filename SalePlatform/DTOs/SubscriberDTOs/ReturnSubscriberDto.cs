namespace ClothesSalePlatform.DTOs.SubscriberDTOs
{
    public class ReturnSubscriberDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public List<BrandInSubscriberDto> BrandInSubscriberDto { get; set; }
        public List<CategoryInSubscriberDto> CategoryInSubscriberDto { get; set; }
        public List<StoreInSubscriberDto> StoreInSubscriberDto { get; set; }

    }
    public class BrandInSubscriberDto
    {
        public string Name{ get; set; }
        public string Description { get; set; }
        public int ProductCount { get; set; }
    }
    public class CategoryInSubscriberDto
    {
        public string Name{ get; set; }
        public int ProductCount { get; set; }
    }
    public class StoreInSubscriberDto
    {
        public string Name{ get; set; }
        public string Description { get; set; }
        public int ProductCount { get; set; }
    }
}

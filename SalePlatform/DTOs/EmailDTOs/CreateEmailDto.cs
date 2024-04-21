namespace ClothesSalePlatform.DTOs.EmailDTOs
{
    public class CreateEmailDto
    {
        public string Subject { get; set; }
        public List<string> Addresses { get; set; }
        public string Source { get; set; }
        public string ProductName { get; set; }
        public string ProductPrice { get; set; }
        public string ProductColor { get; set; }
    }
}

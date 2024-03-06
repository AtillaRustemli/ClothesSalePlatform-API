namespace ClothesSalePlatform.DTOs.AccountDTOs
{
    public class ReturnUserListDto
    {
        public int UserCount { get; set; }
        public List<ReturnUserDto> Values { get; set; }
    }
}

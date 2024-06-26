﻿namespace ClothesSalePlatform.DTOs.ProductDTOs
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int ProductCount { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        public List<int> SizeId { get; set; }
        public int GenderId { get; set; }
        public int StoreId { get; set; }
        public IFormFile[] Photos { get; set; }
    }
}

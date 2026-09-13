namespace lhhLesson04.Models
{
    public class Category
    {
        public string CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
    public class  Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductImages { get; set; }
        public int ProductPrice { get; set; } = 0;
        public int ProductSalePrice { get; set; } = 0;
        public string CategoryId { get; set; }
        public string ProductDescription { get; set; }
        public string ProductStatus { get; set; }
        public DateTime ProductCreatedDate { get; set; }
    }
}

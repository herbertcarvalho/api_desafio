namespace Backend.Erp.Skeleton.Application.DTOs.Request.Product
{
    public class CreateProductRequest
    {
        public int IdCompany { get; set; }
        public int IdCategory { get; set; }
        public bool Status { get; set; }
        public string Img { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}

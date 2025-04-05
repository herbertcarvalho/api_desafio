using Backend.Erp.Skeleton.Application.DataAnnotation;
using Backend.Erp.Skeleton.Domain.Extensions;

namespace Backend.Erp.Skeleton.Application.DTOs.Request.Products
{
    public class GetProductsQuery : PageOption
    {
        [ValidateInt]
        public int? IdCompany { get; set; }

        [ValidateInt]
        public int? IdCategory { get; set; }

        [ValidateDecimal]
        public decimal? MinPrice { get; set; }

        [ValidateDecimal]
        public decimal? MaxPrice { get; set; }

        public bool Active { get; set; } = true;
    }
}

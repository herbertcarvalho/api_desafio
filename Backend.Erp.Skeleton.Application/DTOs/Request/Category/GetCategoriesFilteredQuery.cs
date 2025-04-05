using Backend.Erp.Skeleton.Application.DataAnnotation;
using Backend.Erp.Skeleton.Domain.Extensions;

namespace Backend.Erp.Skeleton.Application.DTOs.Request.Category
{
    public class GetCategoriesFilteredQuery : PageOption
    {
        [ValidateString(isRequired: false)]
        public string Name { get; set; }
    }
}

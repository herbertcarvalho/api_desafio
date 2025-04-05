using Backend.Erp.Skeleton.Domain.Entities;

namespace Backend.Erp.Skeleton.Application.DTOs.Response.Category
{
    public class GetCategoriesFilteredResponse : BaseId
    {
        public string Name { get; set; }

        public GetCategoriesFilteredResponse(Categories category)
        {
            Name = category.Name;
            Id = category.Id;
        }
    }
}

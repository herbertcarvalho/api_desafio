using Backend.Erp.Skeleton.Application.DataAnnotation;

namespace Backend.Erp.Skeleton.Application.DTOs
{
    public class BaseIdQuery
    {
        [ValidateInt]
        public int Id { get; set; }
    }
}

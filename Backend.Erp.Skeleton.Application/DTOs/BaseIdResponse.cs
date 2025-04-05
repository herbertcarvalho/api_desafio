using System.ComponentModel.DataAnnotations;

namespace Backend.Erp.Skeleton.Application.DTOs
{
    public class BaseIdResponse
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Id inválido.")]
        public int Id { get; set; }
    }
}

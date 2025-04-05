using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Erp.Skeleton.Domain.Extensions
{
    public class PageOption
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Número da página inválido")]
        public int Page { get; set; }

        [Required]
        [InList(10, 25, 50, 100)]
        public int PageSize { get; set; }
    }
}

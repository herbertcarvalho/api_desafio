using System;

namespace Backend.Erp.Skeleton.Domain.Extensions
{
    internal interface IEntity
    {
        public int Id { get; set; }
        public int IdCreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IdLastModifiedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
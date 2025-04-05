using System;

namespace Backend.Erp.Skeleton.Domain.Extensions
{
    public abstract class Entity : IEntity
    {
        protected Entity()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public int IdCreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int IdLastModifiedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
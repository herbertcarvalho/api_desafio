using System;

namespace Backend.Erp.Skeleton.Domain.Extensions
{
    internal interface IEntity
    {
        public int id { get; set; }
        public int idCreatedBy { get; set; }
        public DateTime createdAt { get; set; }
        public int idLastModifiedBy { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
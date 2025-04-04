using Backend.Erp.Skeleton.Domain.Extensions;
using System.Collections.Generic;

namespace Backend.Erp.Skeleton.Domain.Entities
{
    public class Categories : Entity
    {
        public string name { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}

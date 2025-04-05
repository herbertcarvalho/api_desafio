using Backend.Erp.Skeleton.Domain.Extensions;
using System.Collections.Generic;

namespace Backend.Erp.Skeleton.Domain.Entities
{
    public class Categories : Entity
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value.Trim(); }
        }

        public virtual ICollection<Products> Products { get; set; }
    }
}

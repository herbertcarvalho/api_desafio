using Backend.Erp.Skeleton.Domain.Extensions;
using System.Collections.Generic;

namespace Backend.Erp.Skeleton.Domain.Entities
{
    public class Company : Entity
    {
        public string cnpj { get; set; }
        public string name { get; set; }
        public string image { get; set; }

        public virtual ICollection<Persons> Persons { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}

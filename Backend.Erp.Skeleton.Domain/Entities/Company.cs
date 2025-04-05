using Backend.Erp.Skeleton.Domain.Extensions;
using System.Collections.Generic;

namespace Backend.Erp.Skeleton.Domain.Entities
{
    public class Company : Entity
    {
        private string _name;
        public string Cnpj { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value.Trim(); }
        }
        public string Image { get; set; }

        public virtual ICollection<Persons> Persons { get; set; }
        public virtual ICollection<Products> Products { get; set; }
    }
}

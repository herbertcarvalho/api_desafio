using Backend.Erp.Skeleton.Domain.Extensions;

namespace Backend.Erp.Skeleton.Domain.Entities
{
    public class Products : Entity
    {
        public int idCompany { get; set; }
        public int idCategory { get; set; }
        public bool status { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public decimal price { get; set; }

        public virtual Company Company { get; set; }
        public virtual Categories Category { get; set; }
    }
}

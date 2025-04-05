using Backend.Erp.Skeleton.Domain.Extensions;

namespace Backend.Erp.Skeleton.Domain.Entities
{
    public class Products : Entity
    {
        private string _name;

        public int IdCompany { get; set; }
        public int IdCategory { get; set; }
        public bool Status { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value.Trim(); }
        }
        public string Image { get; set; }
        public decimal Price { get; set; }

        public virtual Company Company { get; set; }
        public virtual Categories Category { get; set; }
    }
}

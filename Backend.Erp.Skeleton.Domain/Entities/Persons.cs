using Backend.Erp.Skeleton.Domain.Extensions;

namespace Backend.Erp.Skeleton.Domain.Entities
{
    public class Persons : Entity
    {
        private string _name;

        public int IdUser { get; set; }
        public int IdUserType { get; set; }
        public int? IdCompany { get; set; }
        public string Cpf { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value.Trim(); }
        }

        public virtual Company Company { get; set; }
    }
}

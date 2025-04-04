using Backend.Erp.Skeleton.Domain.Extensions;

namespace Backend.Erp.Skeleton.Domain.Entities
{
    public class Persons : Entity
    {
        public int? idCompany { get; set; }
        public string cpf { get; set; }
        public string name { get; set; }

        public virtual Company company { get; set; }
    }
}

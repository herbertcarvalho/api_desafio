using Backend.Erp.Skeleton.Application.DTOs.Request.baseRequest;

namespace Backend.Erp.Skeleton.Application.DTOs.Request.Authorization
{
    public class RegisterUserRequest : LoginBaseRequest
    {
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
    }
}

using Backend.Erp.Skeleton.Application.DTOs.Request.baseRequest;

namespace Backend.Erp.Skeleton.Application.DTOs.Request.Authorization
{
    public class RegisterUserRequest : LoginBaseRequest
    {
        public string cpf { get; set; }
        public string cnpj { get; set; }
        public string companyName { get; set; }
        public string name { get; set; }
    }
}

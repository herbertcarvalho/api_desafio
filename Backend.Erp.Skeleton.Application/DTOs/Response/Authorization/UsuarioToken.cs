using System;

namespace Backend.Erp.Skeleton.Application.DTOs.Response.Authorization
{
    public class UsuarioToken
    {
        public bool Authenticated { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
    }
}

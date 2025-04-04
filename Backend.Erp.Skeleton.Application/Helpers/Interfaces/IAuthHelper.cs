using Backend.Erp.Skeleton.Application.DTOs.Response.Authorization;
using Backend.Erp.Skeleton.Domain.Entities;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Helpers.Interfaces
{
    public interface IAuthHelper
    {
        Task<UsuarioToken> GenerateToken(Persons person);
    }
}

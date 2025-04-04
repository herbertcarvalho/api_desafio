using Backend.Erp.Skeleton.Application.DTOs.Response.Authorization;
using Backend.Erp.Skeleton.Application.Helpers.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Erp.Skeleton.Application.Helpers
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AuthHelper(IConfiguration configuration,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<UsuarioToken> GenerateToken()
        {
            /*var role = await _roleManager.FindByNameAsync(((UserTypeEnum)person.IdUserType).StringValue());*/
            ///var claim = await _roleManager.GetClaimsAsync(role);

            var claims = new List<Claim>
            {
                /*new Claim(JwtRegisteredClaimNames.UniqueName, person.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("IdUser", person.IdUser.ToString())*/
            };

            //claims.AddRange(claim);

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracao = _configuration["TokenConfiguration:ExpireHours"];
            var expiration = DateTime.Now.AddHours(double.Parse(expiracao));

            JwtSecurityToken token = new(
              issuer: _configuration["TokenConfiguration:Issuer"],
              audience: _configuration["TokenConfiguration:Audience"],
              claims: claims,
              expires: expiration,
              signingCredentials: credenciais);

            //retorna os dados com o token e informacoes
            return new UsuarioToken()
            {
                Authenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}

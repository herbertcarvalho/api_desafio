using System.Collections.Generic;

namespace Backend.Erp.Skeleton.Infrastructure.Roles;
public static class Role
{
    public const string Company = "Empresa";
    public const string Client = "Cliente";


    public static readonly IEnumerable<string> Roles = [Company, Client];
}

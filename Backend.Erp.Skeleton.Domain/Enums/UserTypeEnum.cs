using System.ComponentModel;

namespace Backend.Erp.Skeleton.Domain.Enums
{
    public enum UserTypeEnum
    {
        [Description("Empresa")]
        Company = 1,

        [Description("Cliente")]
        Client = 2
    }
}
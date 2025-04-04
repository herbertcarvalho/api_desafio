using System;

namespace Backend.Erp.Skeleton.Application.Helpers
{
    public class ValidationsHelper
    {
        public static bool EnumIsValid<T>(int value)
            => Enum.IsDefined(typeof(T), value);

        public static string EnumIsNotDefined<T>(int value)
            => $"O valor {value} não é válido para o enumerador {typeof(T).Name}";
    }
}

using System;

namespace Backend.Erp.Skeleton.Application.Helpers
{
    public static class ValidationsHelper
    {
        public const string EntryObjectCantBeNull = "O objeto de entrada não pode ser nulo.";
        public const string EntryObjectCantBeEmpty = "O objeto de entrada não pode ser vazio.";

        public static string InvalidMessage(string value)
            => $"O {value} deve ser válido.";

        public static string NotEmptyMessage(string value)
            => $"Campo {value} não pode ser vazio";

        public static string GreaterThanZeroMessage(string value)
            => $"O {value} deve ser maior que 0";

        public static string NotNullMessage(string value)
            => $"{value} não pode ser nulo";

        public static string InvalidTwoNamesMessage()
            => $"É necessário preencher nome e sobrenome";

        public static string InvalidPasswordMessage(string value)
            => $"{value} não pode ter menos que seis caracteres";

        public static string InvalidDateTimeMessage(string value)
            => $"{value} é mais recente que a data atual.";

        public static string LesserThanZeroMessage(string value)
            => $"O {value} não deve ser menor que 0";

        public static bool ValidBirthDate(this DateTime dateTime)
            => dateTime.CompareTo(DateTime.UtcNow).Equals(-1);
        public static bool GreaterThanZero(this int value) =>
            value > 0;

        public static bool GreaterThanZero(this decimal value) =>
            value > 0;

        public static bool EnumIsValid<T>(int value)
            => Enum.IsDefined(typeof(T), value);

        public static string EnumIsNotDefined<T>(int value)
            => $"O valor {value} não é válido para o enumerador {typeof(T).Name}";
    }
}

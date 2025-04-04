using Backend.Erp.Skeleton.Application.Exceptions;
using System;
using System.ComponentModel;
using System.Linq;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;

namespace Backend.Erp.Skeleton.Application.Extensions
{
    public static class IntExtensions
    {
        public static string GetEnumDescription<TEnum>(this int enumValue) where TEnum : Enum
        {
            if (!EnumIsValid<TEnum>(enumValue))
                throw new ApiException(EnumIsNotDefined<TEnum>(enumValue));

            var typeOfEnum = typeof(TEnum);

            var enumName = Enum.GetName(typeOfEnum, enumValue);
            var fieldInfo = typeOfEnum.GetField(enumName);

            var attribute = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                          .Cast<DescriptionAttribute>()
                          .FirstOrDefault();

            return attribute?.Description ?? enumName;
        }
    }
}

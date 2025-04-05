using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Backend.Erp.Skeleton.Application.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool IsEmail(this string str) => MailAddress.TryCreate(str, out _);

        /// <summary>
        /// Valida a senha conforme critérios de complexidade.
        /// </summary>
        /// <param name="input">A senha a ser validada.</param>
        /// <returns>Uma mensagem de erro se a senha for inválida ou null se for válida.</returns>
        public static bool IsValidPassword(this string input)
        {
            var uppercaseRegExp = new GeneratedRegexAttribute(@"[A-Z]");
            var numberRegExp = new GeneratedRegexAttribute(@"[0-9]");
            var letterRegExp = new GeneratedRegexAttribute(@"[a-zA-Z]");
            var symbolRegExp = new GeneratedRegexAttribute(@"[!@#$%^&*(),.?""':{}|<>]");

            var missingCriteria = new List<string>();

            if (string.IsNullOrEmpty(input))
                return false;

            if (input.Length < 6)
                return false;

            if (!uppercaseRegExp.Match(input))
                return false;

            if (!numberRegExp.Match(input))
                return false;

            if (!letterRegExp.Match(input))
                return false;

            if (!symbolRegExp.Match(input))
                return false;

            if (missingCriteria.Count > 0)
                return false;

            return true;
        }

        /// <summary>
        /// Valida se a string fornecida é um CPF (Cadastro de Pessoas Físicas) válido.
        /// </summary>
        /// <param name="cpf">A string do CPF a ser validada.</param>
        /// <returns>Retorna verdadeiro se o CPF for válido, caso contrário, retorna falso.</returns>
        public static bool IsValidCPF(this string cpf)
        {
            cpf = cpf?.Replace(".", "").Replace("-", "");

            if (cpf?.Length != 11 || !long.TryParse(cpf, out _))
                return false;

            if (new string(cpf[0], 11) == cpf)
                return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += (cpf[i] - '0') * (10 - i);
            }

            int remainder = sum % 11;
            int firstCheckDigit = remainder < 2 ? 0 : 11 - remainder;

            if (cpf[9] != firstCheckDigit.ToString()[0])
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += (cpf[i] - '0') * (11 - i);
            }

            remainder = sum % 11;
            int secondCheckDigit = remainder < 2 ? 0 : 11 - remainder;

            return cpf[10] == secondCheckDigit.ToString()[0];
        }

        /// <summary>
        /// Valida se a string fornecida é um CNPJ (Cadastro Nacional da Pessoa Jurídica) válido.
        /// </summary>
        /// <param name="cnpj">A string do CNPJ a ser validado.</param>
        /// <returns>Retorna verdadeiro se o CNPJ for válido, caso contrário, retorna falso.</returns>
        public static bool IsValidCNPJ(this string cnpj)
        {
            // Remove caracteres não numéricos (por exemplo, pontos, barras, traços)
            cnpj = cnpj?.Replace(".", "").Replace("/", "").Replace("-", "");

            // O CNPJ deve ter exatamente 14 dígitos
            if (cnpj?.Length != 14 || !long.TryParse(cnpj, out _))
                return false;

            // O CNPJ não pode ser composto apenas por dígitos idênticos (ex: 11111111111111, 22222222222222, etc.)
            if (new string(cnpj[0], 14) == cnpj)
                return false;

            // Calcula o primeiro dígito verificador
            int[] firstMultipliers = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            int sum = 0;
            for (int i = 0; i < 12; i++)
            {
                sum += (cnpj[i] - '0') * firstMultipliers[i];
            }

            int remainder = sum % 11;
            int firstCheckDigit = remainder < 2 ? 0 : 11 - remainder;

            // Valida o primeiro dígito verificador
            if (cnpj[12] != firstCheckDigit.ToString()[0])
                return false;

            // Calcula o segundo dígito verificador
            int[] secondMultipliers = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
            sum = 0;
            for (int i = 0; i < 13; i++)
            {
                sum += (cnpj[i] - '0') * secondMultipliers[i];
            }

            remainder = sum % 11;
            int secondCheckDigit = remainder < 2 ? 0 : 11 - remainder;

            // Valida o segundo dígito verificador
            return cnpj[13] == secondCheckDigit.ToString()[0];
        }

        /// <summary>
        /// Valida se a string fornecida possui tamanho máximo menor que o fornecido.
        /// </summary>
        /// <param name="input">A string a ser validada.</param>
        /// <param name="length">tamanho máximo.</param>
        /// <returns>Retorna verdadeiro se o CNPJ for válido, caso contrário, retorna falso.</returns>
        public static bool IsValidStringWithLength(this string input, int length, bool alphaNumeric = false)
        {
            if (alphaNumeric && !input.IsValidAlphanumeric())
                return false;

            return !input.IsNullOrEmpty() && input.Length <= length;
        }

        /// <summary>
        /// Valida se as string's fornecidas são iguais.
        /// </summary>
        /// <param name="input">Primeira string</param>
        /// <param name="value">Segunda string</param>
        /// <returns>Retorna verdadeiro se iguais .</returns>
        public static bool EqualsCase(this string input, string value)
            => string.Equals(input, value, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Valida se a string fornecido é válida 
        /// </summary>
        /// <param name="input">String</param>
        /// <returns>Retorna verdadeiro se iguais .</returns>
        public static bool IsValidAlphanumeric(this string input)
            => !string.IsNullOrEmpty(input) && input.All(c => char.IsLetterOrDigit(c) || c == ' ');

        /// <summary>
        /// Valida se a string fornecido é um base64 com tamanho até 5mb
        /// </summary>
        /// <param name="input">String</param>
        /// <returns>Retorna verdadeiro se iguais .</returns>
        public static bool IsBase64StringAndLengthValid(this string input)
        {
            if (input.IsNullOrEmpty()) return false;
            var isBase64 = Convert.TryFromBase64String(input, new Span<byte>(new byte[input.Length]), out int bytes);
            return isBase64 && bytes <= 5 * 1024 * 1024;
        }
    }
}

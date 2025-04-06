using Backend.Erp.Skeleton.Application.DTOs.Request.Authorization;
using Backend.Erp.Skeleton.Application.Validators.Authorization;
using NUnit.Framework;
using Tests.Helpers;
using Tests.Interfaces;
using static Backend.Erp.Skeleton.Application.Helpers.ValidationsHelper;
using static Backend.Erp.Skeleton.Application.Validators.Authorization.AuthorizationConstants;
using static Tests.TestUtils;

namespace Tests.ValidatorQueryTests.Authorization
{
    public class RegisterUserRequestValidatorTest() : BaseValidators, ITest
    {
        private string EMAIL_NULL_MESSAGE = NotNullMessage(email);
        private string EMAIL_EMPTY_MESSAGE = NotEmptyMessage(email);
        private string EMAIL_INVALID_MESSAGE = InvalidMessage(email);

        private const string PASSWORD_EMPTY_MESSAGE = "A senha não pode ser vazia.";
        private const string PASSWORD_CHARACTERES_COUNT_MESSAGE = "A senha deve conter mais de 6 caracteres.";
        private const string PASSWORD_CHARACTERES_UPPERCASE_MESSAGE = "É necessário inserir uma letra maiúscula.";
        private const string PASSWORD_CHARACTERES_NUMBER_MESSAGE = "É necessário inserir um número.";
        private const string PASSWORD_CHARACTERES_LETTER_MESSAGE = "É necessário inserir uma letra.";
        private const string PASSWORD_CHARACTERES_SIMBOL_MESSAGE = "É necessário inserir um símbolo.";

        private string CPF_NULL_MESSAGE = NotNullMessage(cpf);
        private string CPF_EMPTY_MESSAGE = NotEmptyMessage(cpf);
        private string CPF_INVALID_MESSAGE = InvalidMessage(cpf);

        private string CNPJ_INVALID_MESSAGE = InvalidMessage(cnpj);

        private string COMPANY_NAME_EMPTY_MESSAGE = NotEmptyMessage(companyName);
        private string COMPANY_NAME_NULL_MESSAGE = NotNullMessage(companyName);
        private string COMPANY_NAME_INVALID_MESSAGE = StringLesserThanInput(companyName, 100);

        private string NAME_EMPTY_MESSAGE = NotEmptyMessage(name);
        private string NAME_NULL_MESSAGE = NotNullMessage(name);
        private string NAME_INVALID_MESSAGE = StringLesserThanInput(name, 100);

        private RegisterUserRequest request = new()
        {
            Password = "123Abc*!"
        };

        private readonly RegisterUserRequestValidator _validator = new();

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("RegisterUserRequestValidatorTest");
            await ExecuteAssert(EmailNull, report);
            await ExecuteAssert(EmailEmpty, report);
            await ExecuteAssert(EmailInvalid, report);
            await ExecuteAssert(PasswordEmpty, report);
            await ExecuteAssert(PasswordCharacterCountLowerThanSix, report);
            await ExecuteAssert(PasswordNotContainsLetterInUppercase, report);
            await ExecuteAssert(PasswordNotContainsOneNumber, report);
            await ExecuteAssert(PasswordNotContainsOneLetter, report);
            await ExecuteAssert(PasswordNotContainsOneSymbol, report);
            await ExecuteAssert(CpfNull, report);
            await ExecuteAssert(CPFEmpty, report);
            await ExecuteAssert(CPFInvalidContainsCharacter, report);
            await ExecuteAssert(CPFInvalidContainsSymbols, report);
            await ExecuteAssert(CPFInvalidLessThan11Digits, report);
            await ExecuteAssert(CPFInvalidMoreThan11Digits, report);
            await ExecuteAssert(CnpjInvalidContainsCharacter, report);
            await ExecuteAssert(CnpjInvalidContainsSymbols, report);
            await ExecuteAssert(CnpjInvalidLessThan11Digits, report);
            await ExecuteAssert(CnpjInvalidMoreThan11Digits, report);
            await ExecuteAssert(CompanyNameNull, report);
            await ExecuteAssert(CompanyNameEmpty, report);
            await ExecuteAssert(CompanyNameMoreThan100Characters, report);
            await ExecuteAssert(NameNull, report);
            await ExecuteAssert(NameEmpty, report);
            await ExecuteAssert(NameMoreThan100Characters, report);
        }

        [Test]
        private void EmailNull()
        {
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(EMAIL_NULL_MESSAGE, response);
        }

        [Test]
        private void EmailEmpty()
        {
            request.Email = string.Empty;
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(EMAIL_EMPTY_MESSAGE, response);
        }

        [Test]
        private void EmailInvalid()
        {
            request.Email = "olawkowqkqwko";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(EMAIL_INVALID_MESSAGE, response);
        }

        [Test]
        private void PasswordEmpty()
        {
            request.Password = string.Empty;
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PASSWORD_EMPTY_MESSAGE, response);
        }

        [Test]
        private void PasswordCharacterCountLowerThanSix()
        {
            request.Password = "33333";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PASSWORD_CHARACTERES_COUNT_MESSAGE, response);
        }

        [Test]
        private void PasswordNotContainsLetterInUppercase()
        {
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PASSWORD_CHARACTERES_UPPERCASE_MESSAGE, response);
        }

        [Test]
        private void PasswordNotContainsOneNumber()
        {
            request.Password = "kasoasko";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PASSWORD_CHARACTERES_NUMBER_MESSAGE, response);
        }

        [Test]
        private void PasswordNotContainsOneLetter()
        {
            request.Password = "123456";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PASSWORD_CHARACTERES_LETTER_MESSAGE, response);
        }

        [Test]
        private void PasswordNotContainsOneSymbol()
        {
            request.Password = "123456";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PASSWORD_CHARACTERES_SIMBOL_MESSAGE, response);
        }

        [Test]
        private void CpfNull()
        {
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(CPF_NULL_MESSAGE, response);
        }

        [Test]
        private void CPFEmpty()
        {
            request.Cpf = string.Empty;
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(CPF_EMPTY_MESSAGE, response);
        }

        [Test]
        private void CPFInvalidContainsCharacter()
        {
            request.Cpf = "1234567894e";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(CPF_INVALID_MESSAGE, response);
        }

        [Test]
        private void CPFInvalidContainsSymbols()
        {
            request.Cpf = "1234567894*";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(CPF_INVALID_MESSAGE, response);
        }

        [Test]
        private void CPFInvalidLessThan11Digits()
        {
            request.Cpf = "1234567894";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(CPF_INVALID_MESSAGE, response);
        }

        [Test]
        private void CPFInvalidMoreThan11Digits()
        {
            request.Cpf = "1234567894333333";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(CPF_INVALID_MESSAGE, response);
        }

        [Test]
        private void CnpjInvalidContainsCharacter()
        {
            request.Cnpj = "2221234567894e";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(CNPJ_INVALID_MESSAGE, response);
        }

        [Test]
        private void CnpjInvalidContainsSymbols()
        {
            request.Cnpj = "2221234567894*";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(CNPJ_INVALID_MESSAGE, response);
        }

        [Test]
        private void CnpjInvalidLessThan11Digits()
        {
            request.Cnpj = "1234567894";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(CNPJ_INVALID_MESSAGE, response);
        }

        [Test]
        private void CnpjInvalidMoreThan11Digits()
        {
            request.Cnpj = "123456789433333333333333333";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(CNPJ_INVALID_MESSAGE, response);
        }

        [Test]
        private void CompanyNameNull()
        {
            request.Cnpj = "47067087000188";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(COMPANY_NAME_NULL_MESSAGE, response);
        }

        [Test]
        private void CompanyNameEmpty()
        {
            request.CompanyName = string.Empty;
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(COMPANY_NAME_EMPTY_MESSAGE, response);
        }

        [Test]
        private void CompanyNameMoreThan100Characters()
        {
            request.CompanyName = GenerateFixedLengthString(101);
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(COMPANY_NAME_INVALID_MESSAGE, response);
        }

        [Test]
        private void NameNull()
        {
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(NAME_NULL_MESSAGE, response);
        }

        [Test]
        private void NameEmpty()
        {
            request.Name = string.Empty;
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(NAME_EMPTY_MESSAGE, response);
        }

        [Test]
        private void NameMoreThan100Characters()
        {
            request.Name = GenerateFixedLengthString(101);
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(NAME_INVALID_MESSAGE, response);
        }
    }
}

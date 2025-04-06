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
    public class LoginRequestValidatorTest() : BaseValidators, ITest
    {
        private string EMAIL_EMPTY_MESSAGE = NotEmptyMessage(email);
        private string EMAIL_NULL_MESSAGE = NotNullMessage(email);
        private string EMAIL_INVALID_MESSAGE = InvalidMessage(email);

        private const string PASSWORD_EMPTY = "A senha não pode ser vazia.";
        private const string PASSWORD_CHARACTERES_COUNT = "A senha deve conter mais de 6 caracteres.";
        private const string PASSWORD_CHARACTERES_UPPERCASE = "É necessário inserir uma letra maiúscula.";
        private const string PASSWORD_CHARACTERES_NUMBER = "É necessário inserir um número.";
        private const string PASSWORD_CHARACTERES_LETTER = "É necessário inserir uma letra.";
        private const string PASSWORD_CHARACTERES_SIMBOL = "É necessário inserir um símbolo.";

        private LoginRequest request = new()
        {
            Password = "123Abc*!"
        };

        private readonly LoginRequestValidator _validator = new();

        public async Task ExecuteTestsAsync(Tuple<List<int>, List<string>> report)
        {
            WriteTextCmd("LoginRequestValidatorTest");
            await ExecuteAssert(EmailNull, report);
            await ExecuteAssert(EmailEmpty, report);
            await ExecuteAssert(EmailInvalid, report);
            await ExecuteAssert(PasswordEmpty, report);
            await ExecuteAssert(PasswordCharacterCountLowerThanSix, report);
            await ExecuteAssert(PasswordNotContainsLetterInUppercase, report);
            await ExecuteAssert(PasswordNotContainsOneNumber, report);
            await ExecuteAssert(PasswordNotContainsOneLetter, report);
            await ExecuteAssert(PasswordNotContainsOneSymbol, report);
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
            CustomAssert.Contains(PASSWORD_EMPTY, response);
        }

        [Test]
        private void PasswordCharacterCountLowerThanSix()
        {
            request.Password = "33333";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PASSWORD_CHARACTERES_COUNT, response);
        }

        [Test]
        private void PasswordNotContainsLetterInUppercase()
        {
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PASSWORD_CHARACTERES_UPPERCASE, response);
        }

        [Test]
        private void PasswordNotContainsOneNumber()
        {
            request.Password = "kasoasko";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PASSWORD_CHARACTERES_NUMBER, response);
        }

        [Test]
        private void PasswordNotContainsOneLetter()
        {
            request.Password = "123456";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PASSWORD_CHARACTERES_LETTER, response);
        }

        [Test]
        private void PasswordNotContainsOneSymbol()
        {
            request.Password = "123456";
            var response = RunAssertValidatorsTest(request, _validator);
            CustomAssert.Contains(PASSWORD_CHARACTERES_SIMBOL, response);
        }
    }
}

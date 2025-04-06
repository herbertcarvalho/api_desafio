using FluentValidation;

namespace Tests.Helpers
{
    public class BaseValidators()
    {
        public List<string> RunAssertValidatorsTest<T>(T obj, AbstractValidator<T> validator)
        {
            var response = validator.Validate(obj);
            return response.Errors.Select(x => x.ErrorMessage).ToList();
        }
    }
}

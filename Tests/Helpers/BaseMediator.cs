using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.Helpers
{
    public class BaseMediator
    {
        public IServiceProvider _serviceProvider;

        protected BaseMediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected IMediator Mediator => _serviceProvider.GetRequiredService<IMediator>();

        public async Task<string?> RunUnitTestAsync(object func)
        {
            try
            {
                await Mediator.Send(func);
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static async Task<string?> RunUnitTestAsync(Task func)
        {
            try
            {
                await func;
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

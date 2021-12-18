using AgilityWeb.Api.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace AgilityWeb.Api
{
    public static class StartupMiddlewares
    {
        public static void ConfigureMiddlewares(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ExceptionHandlingMiddleware>();
        }
    }
}
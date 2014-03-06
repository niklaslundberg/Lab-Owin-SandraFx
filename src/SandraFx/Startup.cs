using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

namespace SandraFx
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use((context, next) =>
            {
                GetAsync(context).Wait();

                return next();
            });
        }

        async Task GetAsync(IOwinContext context)
        {
            var path = context.Request.Path.Value;

            IReadOnlyCollection<NancyModule> modules = GetModules();

            var foundModule = modules.SingleOrDefault(module => module.Get.ContainsKey(path));

            if (foundModule == null)
            {
                context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return;
            }

            Func<object, string> routeFunc = foundModule.Get[path];

            string result = routeFunc(new object());

            await context.Response.WriteAsync(result);
        }

        IReadOnlyCollection<NancyModule> GetModules()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes()
                    .Where(type => typeof (NancyModule).IsAssignableFrom(type) && !type.IsAbstract));

            var nancyModules = types.Select(type => (NancyModule) Activator.CreateInstance(type)).ToList();

            return nancyModules;
        }
    }
}
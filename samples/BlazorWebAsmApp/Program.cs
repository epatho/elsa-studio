using Elsa.Studio.Contracts;
using Elsa.Studio.Core.BlazorWasm.Extensions;
using Elsa.Studio.Dashboard.Extensions;
using Elsa.Studio.Extensions;
using Elsa.Studio.Login.BlazorWasm.Extensions;
using Elsa.Studio.Login.HttpMessageHandlers;
using Elsa.Studio.Shell;
using Elsa.Studio.Shell.Extensions;
using Elsa.Studio.WorkflowContexts.Extensions;
using Elsa.Studio.Workflows.Designer.Extensions;
using Elsa.Studio.Workflows.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorWebAsmApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var configuration = builder.Configuration;

            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.RootComponents.RegisterCustomElsaStudioElements();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddCore();
            builder.Services.AddShell();

            builder.Services.AddRemoteBackend(
                elsaClient => elsaClient.AuthenticationHandler = typeof(AuthenticatingApiHttpMessageHandler),
                options => configuration.GetSection("Backend").Bind(options));


            builder.Services.AddLoginModule();
            builder.Services.AddDashboardModule();
            builder.Services.AddWorkflowsModule();
            
            builder.Services.AddWorkflowContextsModule();

            var app = builder.Build();

            // Run each startup task.
            var startupTaskRunner = app.Services.GetRequiredService<IStartupTaskRunner>();
            await startupTaskRunner.RunStartupTasksAsync();

            await app.RunAsync();
        }
    }
}

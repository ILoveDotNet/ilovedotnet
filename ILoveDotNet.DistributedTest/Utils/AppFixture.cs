using Aspire.Hosting;

[assembly: AssemblyFixture(typeof(ILoveDotNet.DistributedTest.Utils.AppFixture))]
namespace ILoveDotNet.DistributedTest.Utils;

public class AppFixture : IAsyncLifetime
{
    private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(60);
    public DistributedApplication App { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        var cancellationToken = new CancellationTokenSource(_defaultTimeout).Token;
        var appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.ILoveDotNet_AppHost>(
                args: ["DcpPublisher:RandomizePorts=false"],
                configureBuilder: (appOptions, hostSettings) =>
                {
                    appOptions.DisableDashboard = false;
                },
                cancellationToken);

        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        App = await appHost
            .BuildAsync(cancellationToken)
            .WaitAsync(_defaultTimeout, cancellationToken);

        await App.StartAsync(cancellationToken)
            .WaitAsync(_defaultTimeout, cancellationToken);

        await App.ResourceNotifications
            .WaitForResourceHealthyAsync("ILoveDotNet-Web", TestContext.Current.CancellationToken)
            .WaitAsync(_defaultTimeout, TestContext.Current.CancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        if (App is not null)
        {
            await App.StopAsync(TestContext.Current.CancellationToken)
                .WaitAsync(_defaultTimeout, TestContext.Current.CancellationToken);
            await App.DisposeAsync();
        }
    }
}

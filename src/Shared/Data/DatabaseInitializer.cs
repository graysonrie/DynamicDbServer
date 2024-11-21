using Serilog;

namespace DynamicDbServer.src.Shared.Data;
public class DatabaseInitializer(IServiceProvider serviceProvider) : IHostedService
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await context.Database.EnsureDeletedAsync(cancellationToken);
        Log.Warning("Database tables have been reset");

        await context.Database.EnsureCreatedAsync(cancellationToken);
        Log.Information("Database has been created");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
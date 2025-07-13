using EvDb.Core;
using Xunit.Abstractions;
#pragma warning disable S3881 // "IDisposable" should be implemented correctly

namespace EvDbQuickStart.Funds.IntegrationTests;

public abstract class BaseTests : IAsyncLifetime, IDisposable, IAsyncDisposable
{
    protected readonly ITestOutputHelper _output;
    protected abstract IEvDbStorageAdmin Admin { get; }

    protected BaseTests(ITestOutputHelper output)
    {
        _output = output;
    }

    public async Task InitializeAsync()
    {
        await Admin.CreateEnvironmentAsync();
    }


    void IDisposable.Dispose()
    {
        DisposeAsync().Wait();
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await DisposeAsync();
    }

    public virtual async Task DisposeAsync()
    {
        await Admin.DestroyEnvironmentAsync();
    }

    ~BaseTests()
    {
        DisposeAsync().Wait();
    }
}
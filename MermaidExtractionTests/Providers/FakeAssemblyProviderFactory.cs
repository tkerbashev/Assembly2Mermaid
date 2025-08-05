using MermaidExtractionEngine.Interfaces;

namespace MermaidExtractionTests.Providers;

internal class FakeAssemblyProviderFactory : IAssemblyProviderFactory
{
    public IAssemblyProvider NewAssemblyProvider => new FakeAssemblyProvider();
}

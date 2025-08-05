using MermaidExtractionEngine.Interfaces;

namespace MermaidExtractionEngine.Providers;

public class AssemblyProviderFactory : IAssemblyProviderFactory
{
    public IAssemblyProvider NewAssemblyProvider => new AssemblyProvider();
}

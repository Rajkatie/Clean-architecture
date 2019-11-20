using Core.Interfaces;
using Infrastructure.Data;
using StructureMap;

namespace Infrastructure
{
    public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            Scan(scan => 
                {
                    scan.TheCallingAssembly();
                    scan.AssemblyContainingType<IProductRepository>(); // Core
                    scan.WithDefaultConventions();
                });

            For<IProductRepository>().Use<InMemoryProductRepository>();
        }
    }
}
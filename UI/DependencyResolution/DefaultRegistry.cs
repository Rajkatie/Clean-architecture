using Core.Interfaces;
using StructureMap;
using StructureMap.Graph;

namespace UI.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {

            Scan(loadAssemblies);
          
        }

        private void loadAssemblies(IAssemblyScanner obj)
        {
            obj.TheCallingAssembly();

            obj.AssemblyContainingType<IProductRepository>(); // Core

            obj.Assembly("Infrastructure"); // the Infrastructure DLL

            obj.WithDefaultConventions();

            obj.With(new ControllerConvention());

            obj.LookForRegistries();


        }
    }
}
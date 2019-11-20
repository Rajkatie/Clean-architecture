using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using StructureMap;
using System.Web.Mvc;
using UI.App_Start;
using UI.DependencyResolution;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(StructuremapMvc), "Start")]
[assembly: ApplicationShutdownMethod(typeof(StructuremapMvc), "End")]

namespace UI.App_Start
{
    public class StructuremapMvc
    {
        public static StructureMapDependencyScope StructureMapDependencyScope { get; set; }


        public static void End()
        {
            StructureMapDependencyScope.Dispose();
        }

        public static void Start()
        {
            IContainer container = IoC.Initialize();

            StructureMapDependencyScope = new StructureMapDependencyScope(container);

            DependencyResolver.SetResolver(StructureMapDependencyScope);

            DynamicModuleUtility.RegisterModule(typeof(StructureMapScopeModule));
        }
    }
}
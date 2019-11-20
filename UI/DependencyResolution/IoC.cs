using StructureMap;

namespace UI.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize() 
            => new Container(c => c.AddRegistry<DefaultRegistry>());
        
    }
}
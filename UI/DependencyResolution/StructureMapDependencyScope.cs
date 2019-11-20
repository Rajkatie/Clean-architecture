using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using System.Web;
using System.Linq;

namespace UI.DependencyResolution
{
    public class StructureMapDependencyScope : ServiceLocatorImplBase
    {
        private const string NestedContainerKey = "Nested.Container.Key";

        public IContainer Container { get; set; }

        public StructureMapDependencyScope(IContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            Container = container;
        }

        private HttpContextBase HttpContext
        {
            get
            {
                var ctx = Container.TryGetInstance<HttpContextBase>();
                return ctx ?? new HttpContextWrapper(System.Web.HttpContext.Current);
            }
        }


        public IContainer CurrentNestedContainer
        {
            get
            {
                return (IContainer)HttpContext.Items[NestedContainerKey];
            }

            set
            {
                HttpContext.Items[NestedContainerKey] = value;
            }
        }


        public void CreateNestedContainer()
        {
            if (CurrentNestedContainer != null)
            {
                return;
            }
            CurrentNestedContainer = Container.GetNestedContainer();
        }

        public void Dispose()
        {
            DisposeNestedContainer();
            Container.Dispose();
        }
        public void DisposeNestedContainer()
        {
            if (CurrentNestedContainer != null)
            {
                CurrentNestedContainer.Dispose();
                CurrentNestedContainer = null;
            }
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        =>  (CurrentNestedContainer ?? Container).GetAllInstances(serviceType).Cast<object>();

        protected override object DoGetInstance(Type serviceType, string key)
        {
            IContainer container = (CurrentNestedContainer ?? Container);

            if (string.IsNullOrEmpty(key))
            {
                return serviceType.IsAbstract || serviceType.IsInterface
                    ? container.TryGetInstance(serviceType)
                    : container.GetInstance(serviceType);
            }

            return container.GetInstance(serviceType, key);
        }
    }
}
using LogSpy.Core.Infrastructure;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using StructureMap;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Composite.Presentation.Regions.Behaviors;
using Microsoft.Practices.Composite.Logging;

namespace LogSpy.UI
{
    public class LogSpyBootstrapper
    {
        protected virtual void SetupLogger()
        {
            var logger = new Log4NetLogger();
            ObjectFactory.Configure(x => x.ForRequestedType<ILoggerFacade>().TheDefault.IsThis(logger));
            var loggerFacade = ObjectFactory.GetInstance<ILoggerFacade>();
            Log.Use(loggerFacade);
        }

        protected virtual void SetupContainer()
        {
            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator());
            ObjectFactory.Configure(
                x => x.ForRequestedType<IServiceLocator>().TheDefault.Is.Object(ServiceLocator.Current));
            ObjectFactory.Configure(x => x.Scan(s =>
                                                    {
                                                        s.AssemblyContainingType(GetType());
                                                        s.WithDefaultConventions();
                                                    }));
        }

        public void Run()
        {
            SetupContainer();
            SetupRegionRequirements();
            SetupLogger();
        }

        protected virtual void SetupRegionRequirements()
        {
            ObjectFactory.Configure(x =>
                                        {
                                            x.ForRequestedType<RegionAdapterMappings>().AsSingletons();
                                            x.ForRequestedType<IRegionManager>().TheDefaultIsConcreteType<RegionManager>
                                                ().AsSingletons();
                                            x.ForRequestedType<IRegionViewRegistry>().TheDefaultIsConcreteType
                                                <RegionViewRegistry>().AsSingletons();
                                            x.ForRequestedType<IRegionBehaviorFactory>().TheDefaultIsConcreteType
                                                <RegionBehaviorFactory>().AsSingletons();
                                        });
            var regionBehaviorFactory = ObjectFactory.GetInstance<IRegionBehaviorFactory>();
            regionBehaviorFactory.AddIfMissing(AutoPopulateRegionBehavior.BehaviorKey,
                                               typeof (AutoPopulateRegionBehavior));
            regionBehaviorFactory.AddIfMissing(BindRegionContextToDependencyObjectBehavior.BehaviorKey,
                                               typeof (BindRegionContextToDependencyObjectBehavior));
            regionBehaviorFactory.AddIfMissing(RegionActiveAwareBehavior.BehaviorKey, typeof (RegionActiveAwareBehavior));
            regionBehaviorFactory.AddIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey,
                                               typeof (SyncRegionContextWithHostBehavior));
            regionBehaviorFactory.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey,
                                               typeof (RegionManagerRegistrationBehavior));
        }
    }
}
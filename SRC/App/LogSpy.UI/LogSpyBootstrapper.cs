using System.Windows;
using LogSpy.Core.Infrastructure;
using LogSpy.UI.Views.Dialogs;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using StructureMap;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Composite.Presentation.Regions.Behaviors;
using Microsoft.Practices.Composite.Logging;
using System.Windows.Controls;
using LogSpy.Core.Model;

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
            //A bug in Structure map occurs when running this in a different order and using the ForSingletonOf method
            ObjectFactory.Configure(x => x.Scan(s =>
                                                    {
                                                        s.AssemblyContainingType(GetType());
                                                        s.AssemblyContainingType(typeof(ILogProvider));
                                                        s.WithDefaultConventions();
                                                        s.ConnectImplementationsToTypesClosing(typeof(IDialog<>));
                                                    }));
            ObjectFactory.Configure(x =>
            {
                                            x.ForSingletonOf<IDialogLauncher>();
                                            x.ForRequestedType<IServiceLocator>().TheDefault.Is.Object(
                                                ServiceLocator.Current);
            });
        }

        public void Run()
        {
            SetupContainer();
            SetupRegionRequirements();
            SetupLogger();
            SetupShell();
        }

        protected virtual void SetupShell()
        {
            var controller = ObjectFactory.GetInstance<ApplicationController>();
            ObjectFactory.Configure(x=>{
                                            x.ForRequestedType<IMenu>().TheDefault.IsThis(controller.ShellView.Menu);
                                            x.ForRequestedType<IMenuController>().TheDefaultIsConcreteType
                                                <MenuController>().AsSingletons();
                                        });
            controller.Initialize();
            var shell = controller.ShellView;
            RegionManager.SetRegionManager((DependencyObject) shell, ObjectFactory.GetInstance<IRegionManager>());

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
            var adapterMappings = ObjectFactory.GetInstance<RegionAdapterMappings>();
            adapterMappings.RegisterMapping(typeof (ContentControl),
                                            ObjectFactory.GetInstance<ContentControlRegionAdapter>());
            adapterMappings.RegisterMapping(typeof(ItemsControl),
                                            ObjectFactory.GetInstance<ItemsControlRegionAdapter>());
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
using WPFCaliburnApp.Main;

namespace WPFCaliburnApp.Autofac
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using Caliburn.Micro;

    using global::Autofac;

    /// <summary>The app bootstrapper.</summary>
    public class AppBootstrapper : BootstrapperBase
    {
        #region Constructors and Destructors

        /// <summary> Initializes a new instance of the <see cref="AppBootstrapper" /> class. </summary>
        public AppBootstrapper()
        {
            Initialize();
        }

        #endregion

        #region Public Properties

        /// <summary> Gets the IoC container. </summary>
        public static IContainer Container { get; private set; }

        /// <summary>Method for creating the event aggregator</summary>
        public Func<IEventAggregator> CreateEventAggregator { get; set; }

        /// <summary>Method for creating the window manager</summary>
        public Func<IWindowManager> CreateWindowManager { get; set; }

        #endregion

        #region Methods

        /// <summary>Do not override unless you plan to full replace the logic. This is how the framework retrieves services from the Autofac container.</summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected override void BuildUp(object instance)
        {
            Container.InjectProperties(instance);
        }

        /// <summary>Override to configure the framework and setup your IoC container.</summary>
        protected override void Configure()
        {
            base.Configure();

            CreateWindowManager = () => new WindowManager();
            CreateEventAggregator = () => new EventAggregator();

            var builder = new ContainerBuilder();
            builder.RegisterModule(new MainAutofacModule());

            builder.Register(c => CreateWindowManager()).InstancePerLifetimeScope();
            builder.Register(c => CreateEventAggregator()).InstancePerLifetimeScope();

            ConfigureContainer(builder);
            Container = builder.Build();
        }

        /// <summary>Override to include your own Autofac configuration after the framework has finished its configuration, but before the container is created.</summary>
        /// <param name="builder">The Autofac configuration builder.</param>
        protected virtual void ConfigureContainer(ContainerBuilder builder)
        {
        }

        /// <summary>Do not override unless you plan to full replace the logic. This is how the framework retrieves services from the Autofac container.</summary>
        /// <param name="service">The service to locate.</param>
        /// <returns>The located services.</returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return Container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        /// <summary>Do not override unless you plan to full replace the logic. This is how the framework retrieves services from the Autofac container.</summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns>The located service.</returns>
        protected override object GetInstance(Type service, string key)
        {
            object instance;
            if (string.IsNullOrWhiteSpace(key))
            {
                if (Container.TryResolve(service, out instance))
                {
                    return instance;
                }
            }
            else
            {
                if (Container.TryResolveNamed(key, service, out instance))
                {
                    return instance;
                }
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? service.Name));
        }

        /// <summary>Override this to add custom behavior to execute after the application starts.</summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The args.</param>
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }

        /// <summary>Checks whether the specified assembly should be loaded for Caliburn and Autofac.</summary>
        /// <param name="assemblyPath">The assembly path.</param>
        /// <returns><see langword="true"/> to load the assembly; otherwise, <see langword="false"/></returns>
        protected virtual bool SatisfiesAssemblyFilter(string assemblyPath)
        {
            return true;
        }

        #endregion
    }
}

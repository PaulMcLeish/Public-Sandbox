using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFCaliburnApp.Autofac
{
    using System.Linq;

    using Caliburn.Micro;

    using global::Autofac;

    /// <summary>The main autofac module.</summary>
    public class MainAutofacModule : Module
    {
        #region Methods

        /// <summary>The load.</summary>
        /// <param name="builder">The builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            // register data models
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray()).Where(type => type.Name.EndsWith("DataModel")).Where(type => !string.IsNullOrWhiteSpace(type.Namespace)).AsSelf().InstancePerDependency();

            // register view models
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray()).Where(type => type.Name.EndsWith("ViewModel")).Where(type => !string.IsNullOrWhiteSpace(type.Namespace)).AsSelf().InstancePerDependency();

            // register views
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray()).Where(type => type.Name.EndsWith("View")).Where(type => !string.IsNullOrWhiteSpace(type.Namespace)).AsSelf().InstancePerDependency();
        }

        #endregion
    }
}

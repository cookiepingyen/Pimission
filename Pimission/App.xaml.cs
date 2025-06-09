using Microsoft.Extensions.DependencyInjection;
using Pimission.Contracts;
using Pimission.Presenters;
using Pimission.Service;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Pimission
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IOCServiceCollection.ServiceProvider provider = null;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IOCServiceCollection.ServiceCollection collection = new IOCServiceCollection.ServiceCollection();

            collection.AddTransient<IPIMissionService, PimissionService>();
            collection.AddTransient<IPiMissionPresenter, PimissionPresenter>();
            collection.AddTransient<IPiMissionWindow, MainWindow>();

            collection.AddSingleton<MainWindow, MainWindow>();
            provider = collection.BuildServiceProvider();

            MainWindow windows = provider.GetService<MainWindow>();
            windows?.Show();
        }
    }

}

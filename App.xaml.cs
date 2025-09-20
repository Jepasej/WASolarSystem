using System.Configuration;
using System.Data;
using System.Windows;
using WASolarSystem;
using WASolarSystem.View;
using WASolarSystem.ViewModel;

namespace WASolarSystem
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Handles application startup logic, initializing the main window and its data context.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow m = new();
            m.DataContext = new MainWindowViewModel();
            m.Show();
        }
        
    }

}

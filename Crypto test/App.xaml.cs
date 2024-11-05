using Crypto_test.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;
using Crypto_test.View;
namespace Crypto_test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainViewModel = new MainViewModel();
            var mainView = new MainView { DataContext = mainViewModel };
            mainView.Show();
        }
    }

}

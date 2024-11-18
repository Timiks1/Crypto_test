using Crypto_test.ViewModel;
using System.Windows;
using Crypto_test.View;
namespace Crypto_test
{

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainViewModel = new MainViewModel();
            var mainView = new MainView { DataContext = mainViewModel };


            mainView.Show();
        }
        public void ChangeTheme(string theme)
        {
            try
            {
                var uri = new Uri($"Resources/Themes/{theme}Theme.xaml", UriKind.Relative);
                var themeResource = new ResourceDictionary { Source = uri };
                Application.Current.Resources.MergedDictionaries.Clear();
                Application.Current.Resources.MergedDictionaries.Add(themeResource);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при переключении темы: {ex.Message}");
            }

        }


    }

}

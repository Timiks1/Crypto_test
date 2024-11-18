using System.Windows;

namespace Crypto_test.View
{

    public partial class MainView : Window
    {
        public MainView()
        {

            InitializeComponent();

            MainFrame.Navigate(new CryptoListPage());


        }
    }
}

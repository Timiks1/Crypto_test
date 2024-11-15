using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_test.Resources
{
    public class LocalizationManager : INotifyPropertyChanged
    {
        private static LocalizationManager _instance;
        public static LocalizationManager Instance => _instance ??= new LocalizationManager();

        private ResourceManager _resourceManager;
        private CultureInfo _currentCulture;

        public event PropertyChangedEventHandler PropertyChanged;

        public LocalizationManager()
        {
            _resourceManager = Resources.ResourceManager;
            _currentCulture = CultureInfo.CurrentUICulture;
        }

        public string this[string key] => _resourceManager.GetString(key, _currentCulture);

        public void SetCulture(string cultureCode)
        {
            _currentCulture = new CultureInfo(cultureCode);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(string.Empty));
        }
    }
}
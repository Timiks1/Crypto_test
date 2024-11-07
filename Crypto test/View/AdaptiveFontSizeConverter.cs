using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Crypto_test.View
{
    public class AdaptiveFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
            {
                // Устанавливаем базовый размер шрифта и масштабируем относительно ширины
                return Math.Max(16, width / 30); // Здесь можно настроить делитель для контроля размера
            }
            return 16; // Базовый размер по умолчанию
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

   M S B A M L   `   `   `  ����F  BCrypto test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Crypto_test.UtilsCrypto_test.Utils  5      %    Crypto_test.View.CoinDetailPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      1utilsclr-namespace:Crypto_test.Utils     5      R NOxyPlot.Wpf, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0Y UOxyPlot.Wpf.Shared, Version=2.2.0.0, Culture=neutral, PublicKeyToken=75e952ba404cdbb0"oxyhttp://oxyplot.org/wpf   5        N� Title$  CoinDetailPage��5       � 	Resources 5      1   +Crypto_test.Utils.AdaptiveFontSizeConverter   AdaptiveFontSizeConverter��%   &	          5	   
   6*   5
      .��5      �   PrimaryBackgroundColor#���  6   .+�5   
   �� $
��Auto=�6   .��5      �� $��10q�6   $��Stretch=�6%   $��Stretch=�6C   .+�5      �� -   5         ��Name$ 
BackButton��5      $��
Назад��6-   $
��Left=�5      $	��Top=�5      $��10q�5        ButtonBackground#���  5        ButtonForeground#���  5      5      �� 5      $
��Bold"�5      $��Center=�5      $
��0,10q�5        	TextColor#���  5       ��5      �� *	Coin.name+��5      �� *ActualWidth+ �� RelativeSource ��  �� AncestorType Page �� 	Converter# [  5         OxyPlot.Wpf.PlotView  5"      -   5          Name$ CandleStickChart��5"      $��Stretch=�5#      $	��Top=�5$      $	��300��5%      $	��600��5&      $
��0,20q�5'        OxyPlot.Wpf.PlotViewBase   Model 5"   9   �� *CandleStickModel+5'      � 5)      $��10q�6   $��Center=�6#    � ColumnDefinitions 5*      �� 5+      $��* �6+   �� 5,      $��50 �6+   �� 5-      $��* �6+   	 � RowDefinitions	 5/      �� 50      $
��Auto �6(   �� 51      $
��Auto �6(   �� 52      $
��Auto �6(   �� 53      $
��Auto �6(   .+�56      �� $��Current Price:��6    $
��Bold"�66   $��16(�57       $��0��58       $��0��59       $��0,0,0,5q�5:       #���  5;       �� 5<      $��14(�5=       $��0��5>       $��1��5?       $��0,0,0,10q�5@       #���  5A       ��5<       �� *Coin.quote.USD.price+
 �� StringFormat$
 C��5A       �� 5C      $��Market Cap:��6    $
��Bold"�63   $��16(�5D       $��2��5E       $��0��5F       $��0,0,0,5q�5G       #���  5H       �� 5I      $��14(�5J       $��2��5K       $��1��5L       $��0,0,0,10q�5M       #���  5N       ��5I       �� *Coin.quote.USD.market_cap+$
 C��5N       �� 5P      $��24h Volume:��6    $
��Bold"�63   $��16(�5Q       $��0��5R       $��2��5S       $��0,10,0,5q�5T       #���  5U       �� 5V      $��14(�5W       $��0��5X       $��3��5Y       $��0,0,0,10q�5Z       #���  5[       ��5V       �� *Coin.quote.USD.volume_24h+$
 C��5[       �� 5]      $��Price Change (24h):��6    $
��Bold"�6;   $��16(�5^       $��2��5_       $��2��5`       $��0,10,0,5q�5a       #���  5b       �� 5c      $��14(�5d       $��2��5e       $��3��5f       $��0,0,0,10q�5g       #���  5h       ��5c       �� *#!Coin.quote.USD.percent_change_24h+$
 F2��5h       5i      �� 5k      $��Markets��6   $
��Bold"�5l      $��18(�5m      $��Center=�5n      $��	0,20,0,10q�5o      #���  5p      �� 5q      $��0,0,0,10q�6?   ��6   �� *	Markets+��5r      �� 5s      .R�5t      ��  �� Orientation$ 
Horizontal=�6)   $��5q�6B   .+�5u   "   �� $
��Bold"�6H   $��14(�5v   ,   $	��5,0q�5w   ,   #���  5x   ,   ��5u   ,   �� *
exchangeId+5x   ,   �� 5y   "   $��14(�5z   ,   $	��5,0q�5{   ,   #���  5|   ,   ��5y   ,   �� *
baseSymbol+5|   ,   �� 5}   "   $��/��6,   $��14(�5~   ,   $	��5,0q�5   ,   #���  5�   ,   �� 5�   "   $��14(�5�   ,   $	��5,0q�5�   ,   #���  5�   ,   ��5�   ,   �� *quoteSymbol+5�   ,   �� 5�   "   $��Price: $��6,   $��14(�5�   ,   $	��5,0q�5�   ,   #���  5�   ,   �� 5�   "   $��14(�5�   ,   $	��5,0q�5�   ,   #���  5�   ,   ��5�   ,   �� *
priceUsd+$
 N2��5�   ,   5�      5�      5�      5�      5�      5�      5�      
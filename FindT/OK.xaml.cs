using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Linq;

namespace FindT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class OK : Window
    {
        public OK()
        {
            InitializeComponent();
            SetmultiBinding();
            //ControlTemplate baseWindowTemplate = (ControlTemplate)App.Current.Resources["MyWindowTemplate"];
        }

        public void SetmultiBinding()
        {
            Binding b1 = new Binding
            {
                Path = new PropertyPath("Text"),
                Source = textbox1
            };
            Binding b2 = new Binding
            {
                Path = new PropertyPath("Text"),
                Source = textbox2
            };
            Binding b3 = new Binding
            {
                Path = new PropertyPath("Text"),
                Source = textbox3
            };
            Binding b4 = new Binding
            {
                Path = new PropertyPath("Text"),
                Source = textbox4
            };

            MultiBinding multibinding = new MultiBinding()
            {
                Mode = BindingMode.OneWay
            };
            multibinding.Bindings.Add(b1);
            multibinding.Bindings.Add(b2);
            multibinding.Bindings.Add(b3);
            multibinding.Bindings.Add(b4);
            multibinding.Converter = new Multibindingconverter();
            button.SetBinding(Button.IsEnabledProperty, multibinding);
        }
    }

    public class Multibindingconverter:IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!values.Cast<string>().Any(text=>string.IsNullOrEmpty(text))&&values[0].ToString()==values[1].ToString() && values[2].ToString() == values[3].ToString())
            {
                return  true;
            }
            return false;
            //throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

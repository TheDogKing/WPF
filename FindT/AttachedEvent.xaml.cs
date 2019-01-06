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
    public partial class AttachedEvent : Window
    {
        public AttachedEvent()
        {
            InitializeComponent();
            grid1.AddHandler(AE.NameChangedEvent, new RoutedEventHandler(AENameChangedHandler));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AE ae = new AE()
            {
                Name = "ok"
            };
            RoutedEventArgs ok = new RoutedEventArgs(AE.NameChangedEvent, ae);
            this.Button.RaiseEvent(ok);
        }

        private void AENameChangedHandler(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((e.OriginalSource as AE).Name.ToString());
        }
    }

    public class AE
    {
        //注册声明
        public static readonly RoutedEvent NameChangedEvent = EventManager.RegisterRoutedEvent("NameChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AE));

        public String Name { get; set; }
    }


}

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
    public partial class RouterEventT : Window
    {
        public RouterEventT()
        {
            InitializeComponent();
            gridRoot.AddHandler(Button.ClickEvent, new RoutedEventHandler(Showmessage));
        }
        private void Showmessage(object obj, RoutedEventArgs e)
        {
            MessageBox.Show((e.OriginalSource as FrameworkElement).Name);
        }

        public void ReportTimeHandle(object sender, ReportTimeEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            string timeStr = e.ClickTime.ToLongTimeString();
            string content = string.Format("{0}到达{1}", timeStr, element.Name);
            MessageBox.Show(content);

            //this.lb_view.Items.Add(content);
        }
    }

   public class ReportTimeEventArgs : RoutedEventArgs
    {
        public ReportTimeEventArgs(RoutedEvent routedEvent,object source) : base(routedEvent, source) { }
        public DateTime ClickTime { get; set; }
    }

    public class TimeButton:Button
    {
        //声明注册路由事件
        public static readonly RoutedEvent ReportTimeEvent = EventManager.RegisterRoutedEvent("ReportTime", RoutingStrategy.Bubble, typeof(EventHandler<ReportTimeEventArgs>), typeof(TimeButton));

        //CLR事件封装
        public event RoutedEventHandler ReportTime
        {
            add { this.AddHandler(ReportTimeEvent, value); }
            remove { this.RemoveHandler(ReportTimeEvent, value); }
        }

        //激发路由事件，借用click事件激发方法
        protected override void OnClick()
        {
            base.OnClick();

            ReportTimeEventArgs ReportTimeEventArgs = new ReportTimeEventArgs(ReportTimeEvent, this)
            {
                ClickTime = DateTime.Now
            };
            this.RaiseEvent(ReportTimeEventArgs);
        }
    }
}

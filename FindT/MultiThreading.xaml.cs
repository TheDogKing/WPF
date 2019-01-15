using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FindT
{
    /// <summary>
    /// MultiThreading.xaml 的交互逻辑
    /// </summary>
    public partial class MultiThreading : Page
    {
        public MultiThreading()
        {
            InitializeComponent();
        }

        //回调
        private delegate void settextbox(string value);
        private settextbox setitem;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            setitem = new settextbox(SetValuesss);
            Thread ThreadT = new Thread(new ThreadStart(Newthread));
            ThreadT.Start();
        }

        private void SetValuesss(string value)
        {
            this.TB.Text = value.ToString();
        }

        public void Newthread()
        {
            //TB.Text = DateTime.Now.ToLongTimeString()+"无参";
            TB.Dispatcher.Invoke(setitem,DateTime.Now.ToLongDateString());
        }
    }
}

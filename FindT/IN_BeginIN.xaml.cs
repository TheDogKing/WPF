using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
    /// IN_BeginIN.xaml 的交互逻辑
    /// </summary>
    public partial class IN_BeginIN : Page
    {
        public IN_BeginIN()
        {
            InitializeComponent();
        }

        private void BeginIN_Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToLongTimeString() + "————Start");
            //int x = 0;
            Action<int> ok = Ok;
            for (int i=0;i<5;i++)
            {
                ok.BeginInvoke(i,null, null);
            }
            Console.WriteLine(DateTime.Now.ToLongTimeString() + "————End");

        }

        private void IN_Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(DateTime.Now.ToLongTimeString() + "————Sync Start");
            Action<int> ok = Ok;
            for (int i = 0; i < 5; i++)
            {
                ok.Invoke(i);
            }
            Console.WriteLine(DateTime.Now.ToLongTimeString() + "————Sync End");
        }

        private void Ok(int i)
        {
            Console.WriteLine($"OK{i} Strat "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            for(int j=0;j<1000000000;j++)
            {

            }
            Console.WriteLine($"OK{i} End " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }
    }
}

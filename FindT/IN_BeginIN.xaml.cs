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
using System.ComponentModel;

namespace FindT
{
    /// <summary>
    /// IN_BeginIN.xaml 的交互逻辑
    /// </summary>
    public partial class IN_BeginIN : Page
    {

        private BackgroundWorker bw = new BackgroundWorker();
        Progress ok = new Progress();
        public IN_BeginIN()
        {
            InitializeComponent();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;//支持取消
            bw.DoWork += new DoWorkEventHandler(BGWorker_DoWork);//开始工作
            bw.ProgressChanged += new ProgressChangedEventHandler(bgWorker_ProgessChanged);//进度改变事件
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_WorkerCompleted);//进度完成事件
        }


        private void BGWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //在这里执行耗时的运算。
            //(string)e.Argument == "参数";
            for (int i = 0; i <= 100; i++)
            {

                if (bw.CancellationPending)
                {//用户取消了工作
                    e.Cancel = true;
                    return;
                }
                else
                {
                    bw.ReportProgress(i, "Working");//报告进度，触发ProgressChanged事件
                    Thread.Sleep(10);//模拟工作
                }
            }
        }

        public void bgWorker_ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            //(string)e.UserState=="Working"
           ok.progressbar.Value= e.ProgressPercentage;
            //progressbar.Value = e.ProgressPercentage;//取得进度更新控件，不用Invoke了
        }

        public void bgWorker_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //e.Error == null 是否发生错误
            //e.Cancelled 完成是由于取消还是正常完成
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
            Action testaction = test;
            App.Current.Dispatcher.BeginInvoke(testaction);

            //Console.WriteLine(DateTime.Now.ToLongTimeString() + "————Sync Start");
            //Action<int> ok = Ok;
            //for (int i = 0; i < 5; i++)
            //{
            //    ok.Invoke(i);
            //}
            //Console.WriteLine(DateTime.Now.ToLongTimeString() + "————Sync End");
        }

        private void Ok(int i)
        {
            Console.WriteLine($"OK{i} Strat "+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            for(int j=0;j<1000000000;j++)
            {

            }
            Console.WriteLine($"OK{i} End " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }

        private void test()
        {
            //后台工作运行中，避免重入
            if (bw.IsBusy) return;
            Window x = new Window
            {
                Content = ok
            };
            x.Show();
            bw.RunWorkerAsync("参数");
        }
        
    }
}

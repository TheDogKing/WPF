using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// BackgroundWorkT.xaml 的交互逻辑
    /// </summary>
    public partial class BackgroundWorkT : Page
    {
        private BackgroundWorker bw = new BackgroundWorker();
        Progress ok = new Progress();
        Window x = new Window();
        Window windowAdnore;
        public BackgroundWorkT()
        {
            InitializeComponent();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;//支持取消
            bw.DoWork += new DoWorkEventHandler(BGWorker_DoWork);//开始工作
            bw.ProgressChanged += new ProgressChangedEventHandler(BgWorker_ProgessChanged);//进度改变事件
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BgWorker_WorkerCompleted);//进度完成事件
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
        public void BgWorker_ProgessChanged(object sender, ProgressChangedEventArgs e)
        {
            //(string)e.UserState=="Working"
            ok.progressbar.Value= e.ProgressPercentage;
            //progressbar.Value = e.ProgressPercentage;//取得进度更新控件，不用Invoke了
        }

        public void BgWorker_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var collections = Application.Current.Windows;
            //e.Error == null 是否发生错误
            //e.Result
            //x.Content = null;
            x.Close();
            windowAdnore.Close();
            collections = Application.Current.Windows;
            foreach (Window item in collections)
            {
                item.Close();
                break;
            }
            collections = Application.Current.Windows;
            //e.Cancelled 完成是由于取消还是正常完成
        }

        private void Test()
        {
            //后台工作运行中，避免重入
            var collections = Application.Current.Windows;
            if (bw.IsBusy) return;
            x = new Window
            {
                Content = ok
            };
            x.Show();
            windowAdnore = Application.Current.Windows[Application.Current.Windows.Count - 1];
            bw.RunWorkerAsync("参数");
        }

        private void Backgroudwork_Click(object sender, RoutedEventArgs e)
        {
            Action testaction = Test;
            App.Current.Dispatcher.BeginInvoke(testaction);
        }
    }
}

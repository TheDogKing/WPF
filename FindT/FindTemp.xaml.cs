using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FindT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Findtemp : Window
    {
        Student aaaa;

        public Hashtable AnswerHashTable = new Hashtable();
        public ArrayList HashtableList = new ArrayList();
        public Findtemp()
        {
            InitializeComponent();
            aaaa = new Student();
            Binding binding = new Binding();

            binding.Source = aaaa;
            binding.Path = new PropertyPath("Name");

            BindingOperations.SetBinding(this.box, TextBox.TextProperty, binding);
            //ControlTemplate baseWindowTemplate = (ControlTemplate)App.Current.Resources["MyWindowTemplate"];
        }

        private void FindT_Click(object sender, RoutedEventArgs e)
        {
            string AnswerT = "Error";
            try
            {
                double T1 = Convert.ToDouble(FT.Text.Trim());
                double Dc = Convert.ToDouble(DC.Text.Trim());
                int[] C = { 4000, 8000, 12000 };
                double[] K = new double[3];
                double Lc = 100;
                double T2 = 0;
                HashtableList = new ArrayList();
                while (Lc > Dc)
                {
                    AnswerHashTable = new Hashtable();
                    double Ft = 0;
                    double Zs = -(4644.7) / (1.8 * T1 + 492);
                    for (int i = 0; i < 3; i++)
                    {
                        K[i] = C[i]*Math.Pow(Math.E, Zs);
                        Ft += K[i];
                        AnswerHashTable.Add("K"+i, K[i]);
                    }

                    double St = Ft * (1.8 * 4644.7) / (Math.Pow(1.8 * T1 + 492, 2))/3;
                    Lc = Ft / 3-1;
                    T2 = T1 - (Lc / St);
                    T1 = T2;
                    AnswerHashTable.Add("ST", St);
                    AnswerHashTable.Add("LC", Lc);
                    AnswerHashTable.Add("T2", T2);
                    HashtableList.Add(AnswerHashTable);
                }
                AnswerT = T2.ToString();
                Answer.Text = AnswerT;
            }
            catch (Exception ex)
            {
                MessageBox.Show("*_*! 啊哦，似乎出错了！请检查数据格式哈"+ex, "Error", MessageBoxButton.OK);
            }
            finally
            {

            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            aaaa.Name += "Name";
            //foreach  (Hashtable item  in HashtableList)
            //{
            //    //item["asd"];
            //}
        }

        
        
        
    }

    public class Student
    {
        public string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}

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
    public partial class DependencyPropertyT : Window
    {
        DependencyStudent stu;
        public DependencyPropertyT()
        {
            InitializeComponent();
            stu = new DependencyStudent();
            stu.SetBinding(DependencyStudent.NameProperty, new Binding("Text") { Source = textbox1 });
            textbox2.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = stu });
        }
    }

    public class DependencyStudent : DependencyObject
    {
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(DependencyStudent));
        public BindingExpressionBase SetBinding(DependencyProperty dp, BindingBase binding)
        {
            return BindingOperations.SetBinding(this, dp, binding);
        }
    }
}

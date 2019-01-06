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
    public partial class Property : Window
    {
        public Property()
        {
            InitializeComponent();
            Testproperty();
        }

        public void Testproperty()
        {
            MessageBox.Show(new Human().Age.ToString());

            Human human = new Human
            {
                Age = 99
            };
            human.years = 100;
            MessageBox.Show(human.Age.ToString());
            human.Age = 103;
            MessageBox.Show(human.Age.ToString());
        }
    }

    public class Human
    {
        private int age=70;
        public int years;
        public int Age
        {
            get => age;
            set
            {
                if (value > 100)
                    age = 100;
                else
                    age = value;
            }
        }
    }
}

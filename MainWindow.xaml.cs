using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.IO;
using System.Threading;

 

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {      
            InitializeComponent();
        }
            
        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            string text = textbox1.Text; 
            //разбиваем строку на массив
            string[] segments = text.Split();

            string symbol = textbox2.Text;
            if (symbol == "")
            {
                MessageBox.Show(text);
            }
            else
            {
                char[] firstChar = { symbol[0] }; //формируем массив символов (нам нужен 0 элемент этого массива);
                string first = new string(firstChar);

                //количество вхождений firstChar для каждого элемента массива segments;

                for (int n = 0; n < segments.Length; n++)
                {
                    int count = (segments[n].Length - segments[n].Replace(first, "").Length) / first.Length;
                    for (int i = 0; i < count; i++) //цикл должен продолжаться, пока i < числа найденных firstChar;
                    {
                        int let = segments[n].IndexOfAny(firstChar);
                        if (let == -1)
                        {
                            MessageBox.Show(text);
                            this.Close();
                            break;

                        }
                        string before = segments[n].Substring(0, let + 1);
                        string after = segments[n].Substring(let + 1);

                        segments[n] = String.Concat(before, symbol, after);
                    }
                }
            }

            string text1 = String.Join(' ', segments);
            MessageBox.Show(text1);
            
            string user = Environment.UserName;
            string writePath = @"C:/Users/" + user + "/Desktop/App1.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, Encoding.Default))
                {
                    sw.WriteLine(text1);
                }
            }
            catch (Exception c)
            {
                Console.WriteLine(c.Message);
            }
            
            this.Close();
        }
    }
}

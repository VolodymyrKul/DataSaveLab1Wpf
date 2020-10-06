using System;
using System.Collections.Generic;
using System.IO;
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

namespace DataSaveLab1Wpf
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
            ConsoleTB.Text = "";
            PeriodTB.Text = "Period is ";
            long Param_A = Convert.ToInt64(ParamA_TB.Text), 
                Param_C = Convert.ToInt64(ParamC_TB.Text), 
                Param_X = Convert.ToInt64(ParamX0_TB.Text), 
                Param_M = Convert.ToInt64(ParamM_TB.Text);
            if ((bool)ConsoleCheckBox.IsChecked)
            {
                if ((bool)FileCheckBox.IsChecked)
                {
                    RandFunc(Param_A, Param_C, Param_M, Param_X, true, true);
                }
                else
                {
                    RandFunc(Param_A, Param_C, Param_M, Param_X, false, true);
                }
            }
            else
            {
                if ((bool)FileCheckBox.IsChecked)
                {
                    RandFunc(Param_A, Param_C, Param_M, Param_X, true, false);
                }
                else
                {
                    RandFunc(Param_A, Param_C, Param_M, Param_X, false, false);
                }
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ConsoleTB.Text = "This is console...";
            PeriodTB.Text = "Period is ...";
        }

        public void RandFunc(long Param_A, long Param_C, long Param_M, long Param_X, bool WriteFile, bool WriteConsole)
        {
            long Param_XN = 0, Start_X=Param_X;
            bool Period = false;
            string writePath = @"D:\lab.txt";
            
            if (WriteFile)
            {
                WriteInFile(writePath, Param_X.ToString(), false);
            }
            if (WriteConsole)
            {
                ConsoleTB.Text += Param_X.ToString() + "\n";
            }
            for (long i = 0; i < 100000; i++)
            {
                Param_XN = (Param_A * Param_X + Param_C) % Param_M;
                if (WriteConsole)
                {
                    ConsoleTB.Text += Param_XN.ToString() + "\n";
                }
                if (WriteFile)
                {
                    WriteInFile(writePath, Param_XN.ToString(), true);
                }
                if (Param_XN == Start_X && !Period)
                {
                    Period = true;
                    PeriodTB.Text += (i + 1).ToString();
                    break;
                }
                Param_X = Param_XN;
            }
            if (!Period)
            {
                PeriodTB.Text = "Period not found";
            }
        }
        
        public void WriteInFile(string path, string text, bool mode)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, mode, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

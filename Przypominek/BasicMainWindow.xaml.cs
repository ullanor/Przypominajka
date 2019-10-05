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
using System.Windows.Shapes;
using System.IO;
using System.Reflection;

namespace Przypominek
{
    /// <summary>
    /// Logika interakcji dla klasy BasicMainWindow.xaml
    /// </summary>
    public partial class BasicMainWindow : Window
    {
        public void UpdateWindow()
        {

            if (File.Exists(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
            {

            }
            else
            {

                using (StreamWriter sw = File.CreateText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
                { }

            }

            //string AllEvDataOut;
            string EvDEADLINEOut;
            int HowManyDeadLines;
            int EvDEADLINEWeekOut;
            int NumberOfAllEvents;
            int EvDEADLINEMonthOut;
            int EvDEADLINE3MonthOut;
            HowMuchLeft.MuchLeft(/*out AllEvDataOut, */out EvDEADLINEOut, out HowManyDeadLines, out EvDEADLINEWeekOut, out NumberOfAllEvents, out EvDEADLINEMonthOut, out EvDEADLINE3MonthOut);

            string Text_Text = "NADCHODZĄCE WYDARZENIA:";
            if (HowManyDeadLines > 0)
            {
                Text_Text += "\nDo " + HowManyDeadLines + " pozostało mniej niż dwa dni!";
            }
            if (EvDEADLINEWeekOut > 0)
            {
                Text_Text += "\nDo "+ EvDEADLINEWeekOut+ " pozostał niecały tydzień! ";
            }
            //mniej niz miesiac
            if (EvDEADLINEMonthOut > 0)
            {
                Text_Text += "\nDo " + EvDEADLINEMonthOut + " pozostało mniej niż miesiąc! ";
            }
            //mniej niz trzy miesiace
            if (EvDEADLINE3MonthOut > 0)
            {
                Text_Text += "\nDo " + EvDEADLINE3MonthOut + " pozostało mniej niż 3 miesiące! ";
            }
            if (HowManyDeadLines == 0 && EvDEADLINEWeekOut == 0 && EvDEADLINEMonthOut == 0 && EvDEADLINE3MonthOut == 0)
            {
                Text_Text += "\nBrak :)";
            }
            wydarzeniaInfo.Text = Text_Text;
            



            /*pierwsze uruchomienie catch (Exception ex)
            {
                MessageBox.Show("Error Message: " + ex.Message);
            }*/

        }


        public BasicMainWindow()
        {
            InitializeComponent();
            Left = System.Windows.SystemParameters.WorkArea.Width - Width;
            Top = System.Windows.SystemParameters.WorkArea.Height - Height;
          
            UpdateWindow();
            
            
            
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MoveToMain_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow MainWindowOfP = new MainWindow();
            MainWindowOfP.uruchomCzyAktualizowac = true;
            MainWindowOfP.Show();
            Close();


        }
    }
}

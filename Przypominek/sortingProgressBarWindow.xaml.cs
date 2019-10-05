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
using System.ComponentModel;
using System.Threading;
using System.IO;

namespace Przypominek
{
    /// <summary>
    /// Logika interakcji dla klasy sortingProgressBarWindow.xaml
    /// </summary>
    public partial class sortingProgressBarWindow : Window
    {
        testowa test;
        public sortingProgressBarWindow(testowa test)
        {
            InitializeComponent();
            zakonczButton.Visibility = Visibility.Collapsed;
            this.test = test;

            BackgroundWorker worker = new BackgroundWorker();
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.RunWorkerAsync();

        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            TestProgressBar.Value = e.ProgressPercentage;
            ProgressTextBlock.Text = (string)e.UserState;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            worker.ReportProgress(0, String.Format("Zaczynam"));

            //Thread.Sleep(1000);
            int liczbaSwapow;
            int i = 0;
            int iteracja = 0;
            do
            {
                iteracja++;
                i++;
                if (i == 100)
                {
                    i = 1;
                }
                Sortownik SortownikSortuje = new Sortownik(out liczbaSwapow);
                worker.ReportProgress(i, String.Format("Iteracja: {0}   Liczba Swapów: {1}.",iteracja, liczbaSwapow));
                
                //Thread.Sleep(1000);

            } while (liczbaSwapow != 0);
            worker.ReportProgress(100, String.Format("Wydarzenia zostały posortowane !"));




        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("Wydarzenia zostały posortowane !");
            //TestProgressBar.Value = 0;
            //ProgressTextBlock.Text = "";

            zakonczButton.Visibility = Visibility.Visible;
        }

        private void zakonczButton_Click(object sender, RoutedEventArgs e)
        {
            test.IamNeeded = false;
            test = null;
            Close();
        }
    }
}

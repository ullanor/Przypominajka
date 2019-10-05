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
using System.Media;
using System.Windows.Threading;

namespace Przypominek
{

    public partial class MainWindow : Window
    {
        testowa test;

        public bool sprawdzilUpdate { get; set; }
        public bool uruchomCzyAktualizowac { get; set; }
        public int NumberOfAllEvents;

        public bool playOnceAnvil = false;
        public bool LampIsON = false;
        public int LampCounter = 0;
        private List<Eventin> eventins;       
        private DispatcherTimer aTimer;       

        public int HowManyDeadLines;
        //public string AllEvDataOut;
        public string EvDEADLINEOut;
        public int EvDEADLINEWeekOut;
        public int EvDEADLINEMonthOut;
        public int EvDEADLINE3MonthOut;
        // z obiema poniższymi trzeba zrobić porządek TO DO
        public void UpdateWindow()
        {

            HowMuchLeft.MuchLeft(/*out AllEvDataOut, */ out EvDEADLINEOut, out HowManyDeadLines, out EvDEADLINEWeekOut, out NumberOfAllEvents, out EvDEADLINEMonthOut, out EvDEADLINE3MonthOut);

            if ((HowManyDeadLines + EvDEADLINEWeekOut) > 1)
            {
                showAmount.Text = "Do " + (HowManyDeadLines + EvDEADLINEWeekOut).ToString() + " wydarzeń pozostało mniej niż tydzień !";

            }
            else if ((HowManyDeadLines + EvDEADLINEWeekOut) == 1)
            {
                showAmount.Text = "Do " + (HowManyDeadLines + EvDEADLINEWeekOut).ToString() + " wydarzenia pozostało mniej niż tydzień !";
            }
            else
            {
                showAmount.Text = "Zostało jeszcze dużo czasu :)";
            }


            iloscWydarzenBlock.Text = NumberOfAllEvents.ToString();
            SHOWEVENT.Text = EvDEADLINEOut;
            FillGridView();

        }
        public void FillGridView()
        {
            eventins = new List<Eventin>();
            string[] ArrayFromTextFile = File.ReadAllLines(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt");
            string EventType = string.Empty;
            foreach (string lineOfText in ArrayFromTextFile)
            {
                string[] separatorX = lineOfText.Split('X');
                if (separatorX.Length != 2)
                {
                    continue;
                }
                EventType = string.Empty;
                if (separatorX[0].StartsWith("~895M"))
                {
                    separatorX[0] = separatorX[0].Remove(0, 5);
                    EventType = "~895M";
                }
                else if (separatorX[0].StartsWith("~"))
                {
                    separatorX[0] = separatorX[0].TrimStart('~');
                    EventType = "~";
                }
                    
                int daysLeft = DataCounterObliczacz.CounterObliczacz(separatorX[1]);
                if (daysLeft == -1)
                    eventins.Add(new Eventin() { Type = EventType, Name = separatorX[0], Date = separatorX[1], Left = "Dzisiaj",Zaznacz = false });
                else if (daysLeft == -99)
                    eventins.Add(new Eventin() { Type = EventType, Name = separatorX[0], Date = separatorX[1], Left = "Przestarzałe", Zaznacz = false });
                else eventins.Add(new Eventin() { Type = EventType, Name = separatorX[0], Date = separatorX[1], Left = daysLeft.ToString() + " dni", Zaznacz = false });

            }
            lvUsers.ItemsSource = eventins;

        }

        public void SetMyProgramTimer()
        {
            aTimer = new DispatcherTimer();
            aTimer.Tick += new EventHandler(OnTimedEvent);
            aTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            aTimer.Start();
           
        }
        private void OnTimedEvent(object sender, EventArgs e)
        {
            if (test != null && test.IamNeeded == false)
            {
                test = null;
                UpdateWindow();
            }
            //sprawdza na stronie geldonia.cba.pl czy są aktualizacje
            
            if (sprawdzilUpdate == false && uruchomCzyAktualizowac == true)
            {
                this.sprawdzilUpdate = true;
               
                if (przypominajkaUpdate.checkForP_Updates() == true)
                {
                    Environment.Exit(0);
                }
                
            }

            if ((HowManyDeadLines > 0))
            {
                LampCounter += 1;
                if (LampIsON == false)
                {
                    MIGACZ.Visibility = Visibility.Visible;
                    LampIsON = true;
                }
                else if (LampCounter % 3 == 0)
                {
                    MIGACZ.Visibility = Visibility.Collapsed;
                    LampIsON = false;
                }

                if (playOnceAnvil == false)
                {
                    SoundPlayer sndPlayer = new SoundPlayer(Przypominek.Properties.Resources.ANVILHIT01);
                    sndPlayer.Play();
                    playOnceAnvil = true;
                }
            }

            if (HowManyDeadLines == 0)
            {

                MIGACZ.Visibility = Visibility.Collapsed;
                playOnceAnvil = false;
                LampCounter = 0;

            }

        }
   
        public MainWindow()
        {

            InitializeComponent();
            okieneczko.Height = 652;//637
            HandDeleteButton.IsEnabled = false;
            odswiezButtonRare.IsEnabled = false;
            lvUsers.Visibility = Visibility.Collapsed;            
            Top = System.Windows.SystemParameters.WorkArea.Height - Height;
            if (!File.Exists(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
            {
                using (StreamWriter sw = File.CreateText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
                { }
            }
            DopisywanieWydarzenCyklicznych.DopiszWydarzenia();

            //-----------------------------------------||
            try
            {
                //odswiezButtonRare.Visibility = Visibility.Collapsed;
                UpdateWindow();
            }
            catch (Exception extra)
            {
                MessageBox.Show(extra.ToString());
            }
            //-----------------------------------------||

            SetMyProgramTimer();
            StartowyAutostart.StartowyAuto();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            newEventButton.IsEnabled = false;
            okieneczko.Height = 768;
            okieneczko.Top -= 100;
            nameOfEvent.Text = "Nazwa Wydarzenia";
            wybranaData.SelectedDate = DateTime.Now;
        }

        private void DeleteOldButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteOldEntry.DeleteEntry();           
            UpdateWindow();
        }

        private void AutostartButton_Click(object sender, RoutedEventArgs e)
        {
            RegistryAdderDeleter.AutostartEditor();
        }

        private void HandDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedOnes = eventins.Where(x => x.Zaznacz == true).ToList();
            foreach (Eventin Event in selectedOnes)
            {
                eventins.Remove(Event);
                
            }

            File.Copy(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt", @"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\OLDnazwaIdataDIR.txt", true);
            File.Delete(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt");

            using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
            {
                foreach (Eventin Event in eventins)
                {
                    sw.WriteLine(Event.Type + Event.Name + "X" + Event.Date);
                }
            }
            lvUsers.Items.Refresh();
            UpdateWindow();

        }

        private void exiter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CONTENTCHANGER_Click(object sender, RoutedEventArgs e)
        {
            if ((string)CONTENTCHANGER.Content == "Pokaż wszystkie wydarzenia")
            {
                lvUsers.Visibility = Visibility.Visible;
                HandDeleteButton.IsEnabled = true;
                odswiezButtonRare.IsEnabled = true;
                SHOWEVENT.Visibility = Visibility.Collapsed;
                CONTENTCHANGER.Content = "Pokaż najbliższe DEADLINY";
            }
            else
            {
                HandDeleteButton.IsEnabled = false;
                odswiezButtonRare.IsEnabled = false;
                lvUsers.Visibility = Visibility.Collapsed;
                SHOWEVENT.Visibility = Visibility.Visible;
                //SHOWEVENT.Foreground = new SolidColorBrush(Color.FromArgb(255, (byte)164, (byte)1, (byte)1));
                CONTENTCHANGER.Content = "Pokaż wszystkie wydarzenia";
            }
            
        }

        private void sortownikButton_Click(object sender, RoutedEventArgs e)
        {
            test = new testowa();
            sortingProgressBarWindow showMeSortProgress = new sortingProgressBarWindow(test);
            showMeSortProgress.Show();
            //ustawia szerokosc na auto :D
            nColumn.Width = Double.NaN;
        }
        private void odswiezButtonRare_Click(object sender, RoutedEventArgs e)
        {
            var selectedOnes = eventins.Where(x => x.Zaznacz == true).ToList();
            if (selectedOnes.Count == 0)
                return;
            if (selectedOnes.Count > 1)
            {
                MessageBox.Show("Edytujemy wydarzenia pojedynczo");
                return;
            }
            eventins.Remove(selectedOnes[0]);
                File.Copy(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt", @"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\OLDnazwaIdataDIR.txt", true);
            File.Delete(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt");

            using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
            {
                foreach (Eventin Event in eventins)
                {
                    sw.WriteLine(Event.Type + Event.Name + "X" + Event.Date);
                }
            }
            lvUsers.Items.Refresh();
            UpdateWindow();

            string trimmedNameOfEvent = selectedOnes[0].Name.Remove(selectedOnes[0].Name.Length-7);
            if (trimmedNameOfEvent.EndsWith("."))
                trimmedNameOfEvent = trimmedNameOfEvent.Remove(trimmedNameOfEvent.Length - 6);
            else
                trimmedNameOfEvent = trimmedNameOfEvent.Remove(trimmedNameOfEvent.Length - 1);
            nameOfEvent.Text = trimmedNameOfEvent;
            string[] konwersja = selectedOnes[0].Date.Split('.','#');
            int year = Convert.ToInt32(konwersja[3]);
            int month = Convert.ToInt32(konwersja[2]);
            int day = Convert.ToInt32(konwersja[1]);
            DateTime dateczka = new DateTime(year, month, day);

            wybranaData.SelectedDate = dateczka;

            okieneczko.Height = 768;
            okieneczko.Top -= 100;

        }
        // ----------------- dodawanie nowego wydarzenia for win 10 inna kontrolka date picker
        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            string EvnameToWrite = nameOfEvent.Text;
            string EvdateToWrite = wybranaData.Text;           
            char[] dateCorrection = EvdateToWrite.ToCharArray();
            EvdateToWrite = dateCorrection[0] + "" + dateCorrection[1] + "."
                + dateCorrection[3] + "" + dateCorrection[4] + "."
                + dateCorrection[6] + "" + dateCorrection[7] + "" + dateCorrection[8] + "" + dateCorrection[9];
            char[] NameTester = EvnameToWrite.ToCharArray();
            foreach (char znak in NameTester)
            {
                if (znak == 'X' || znak == '#')
                {
                    return;
                }
            }
            //ustawia szerokosc na auto :D
            nColumn.Width = Double.NaN;
            if (cykliczne.IsChecked == true)
            {
                string dataCykliczna = EvdateToWrite.Substring(0, EvdateToWrite.Length - 5);
                using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
                {


                    sw.WriteLine("~" + EvnameToWrite + " " + "- " + dataCykliczna + "X" + "#" + EvdateToWrite);


                }
            }
            else if (coMiesieczne.IsChecked == true)
            {
                string dataCykliczna = EvdateToWrite.Substring(0, EvdateToWrite.Length - 5);
                using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
                {
                    sw.WriteLine("~895M" + EvnameToWrite + " " + "- " + dataCykliczna + "X" + "#" + EvdateToWrite);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
                {


                    sw.WriteLine(EvnameToWrite + " " + "- " + EvdateToWrite + "X" + "#" + EvdateToWrite);


                }
            }
            okieneczko.Height = 652;
            okieneczko.Top += 100;
            UpdateWindow();
            newEventButton.IsEnabled = true;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            okieneczko.Height = 652;
            okieneczko.Top += 100;
            newEventButton.IsEnabled = true;
        }

        private void nazwaNowaFocus(object sender, RoutedEventArgs e)
        {
            if ((string)nameOfEvent.Text == "Nazwa Wydarzenia")
                nameOfEvent.Text = "";
        }

        private void coroczneClicked(object sender, RoutedEventArgs e)
        {
            coMiesieczne.IsChecked = false;
        }

        private void comiesieczneClicked(object sender, RoutedEventArgs e)
        {
            cykliczne.IsChecked = false;           
        }
    }
}

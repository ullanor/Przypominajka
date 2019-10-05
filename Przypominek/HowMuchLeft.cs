using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace Przypominek
{
    class HowMuchLeft
    {
        
        public static void MuchLeft(/*out string AllEvDataOut, */out string EvDEADLINEOut, out int HowManyDeadLines, out int EvDEADLINEWeekOut,out int NumberOfAllEvents, out int EvDEADLINEMonthOut, out int EvDEADLINE3MonthOut)
        {
            bool juzPozapisywalCykliczne = true;
            if (File.Exists(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\wydarzeniaCykliczne.txt"))
            {
                if (new FileInfo(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\wydarzeniaCykliczne.txt").Length == 0)
                {
                    juzPozapisywalCykliczne = false;
                }
            }

            HowManyDeadLines = 0;

            //string wydarzenia dla ktorego akurat liczymy date
            string obecnewydarzenieNazapas = "";

            string EvDEADLINE = "";
            int EvDEADLINEWeek = 0;
            int NumberOfAllEventsIn = 0;
            EvDEADLINEMonthOut = 0;
            EvDEADLINE3MonthOut = 0;
            //---------------------------------------------------------


            const string poczatekDaty = "#";

            const string znakCyklicznosci= "~";
            bool dodajDatedoCyklicznego = false;
            
            string[] splitter = new string[2];



            string[] ArrayFromTextFile = File.ReadAllLines(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt");
            // MessageBox.Show("Ilość lini tekstu : " + ArrayFromTextFile.Length);

            foreach (string LineOfText in ArrayFromTextFile)
            {

                splitter = LineOfText.Split(new Char[] { 'X' });

                foreach (string nazwaIdata in splitter)
                {
                    if (nazwaIdata.StartsWith(poczatekDaty) == true)
                    {
                        //oddziel # od daty, poprawnie przekonwertuj zapisaną datę i oblicz ile dni pozostało do wydarzenia
                        //zapisz do wyswietlanego stringa datę + (przejście do następnej lini)
                        NumberOfAllEventsIn += 1;


                        int ilePozostaloDni = DataCounterObliczacz.CounterObliczacz(nazwaIdata);
                        string nazwaCyklicznaBezFalki = obecnewydarzenieNazapas;
                        if (obecnewydarzenieNazapas.StartsWith(znakCyklicznosci) == true)
                        {
                            if (obecnewydarzenieNazapas.StartsWith("~895M"))
                                nazwaCyklicznaBezFalki = obecnewydarzenieNazapas.Remove(0, 5);
                            else
                                nazwaCyklicznaBezFalki = obecnewydarzenieNazapas.TrimStart('~');
                        }

                            string poprawnaOdmiana = "dni.";
                        if (ilePozostaloDni == 0)
                        {
                            poprawnaOdmiana = "Niecały dzień.";
                            HowManyDeadLines += 1;
                            //LastChanceMessages();

                            //podkresla wydarzenie ktore niebawem sie konczy
                            //------------------------------cykliczne bez ~ ~

                                EvDEADLINE += nazwaCyklicznaBezFalki + " JUTRO DEADLINE!" + "\n" + "\n";

                            //-------------------------------------------- ~ ~
                            
                        }
                        else if (ilePozostaloDni == 1)
                        {
                            poprawnaOdmiana = "dzień.";
                            HowManyDeadLines += 1;
                            //------------------------------cykliczne bez ~ ~
                                EvDEADLINE += nazwaCyklicznaBezFalki + " POJUTRZE DEADLINE!" + "\n" + "\n";
                               
                        }
                        else if (ilePozostaloDni <= 6 && ilePozostaloDni > 1)
                        {


                            EvDEADLINEWeek += 1;
                            poprawnaOdmiana = "dni.";


                                EvDEADLINE += nazwaCyklicznaBezFalki + " Pozostało mniej niż tydzień!" + "\n" + "\n";
                        }
                        // mniej niz miesiac
                        else if (ilePozostaloDni <= 27 && ilePozostaloDni > 13)
                        {


                            EvDEADLINEMonthOut += 1;
                            poprawnaOdmiana = "dni.";


                                EvDEADLINE += nazwaCyklicznaBezFalki + " Pozostało mniej niż miesiąc!" + "\n" + "\n";

                        }
                        //mniej niz dwa tygodnie
                        else if (ilePozostaloDni <= 13 && ilePozostaloDni > 6)
                        {


                            EvDEADLINEMonthOut += 1;
                            poprawnaOdmiana = "dni.";

                                EvDEADLINE += nazwaCyklicznaBezFalki + " Pozostało mniej niż dwa tygodnie!" + "\n" + "\n";

                        }
                        //mniej niz 3 miesiace
                        else if (ilePozostaloDni <= 88 && ilePozostaloDni > 27)
                        {


                            EvDEADLINE3MonthOut += 1;
                            poprawnaOdmiana = "dni.";


                                EvDEADLINE += nazwaCyklicznaBezFalki + " Pozostało mniej niż 3 miesiące!" + "\n" + "\n";

                        }

                        else if (ilePozostaloDni == -1)
                        {
                            ilePozostaloDni = 0;
                            poprawnaOdmiana = "Dzisiaj";
                            //Dzisiaj jest DZIEŃ SĄDU!!!
                            HowManyDeadLines += 1;


                                EvDEADLINE += nazwaCyklicznaBezFalki + " DZISIAJ DEADLINE!" + "\n" + "\n";

                        }
                        else if (ilePozostaloDni == -99)
                        {
                            //MessageBox.Show("UWAGA stare wydarzenie! Powinniśmy je usunąć!");
                            //ratowanie wydarzenia cyklicznego
                            if (dodajDatedoCyklicznego == true && juzPozapisywalCykliczne == false && obecnewydarzenieNazapas.StartsWith(znakCyklicznosci) == true)
                            {
                                //MessageBox.Show(nazwaIdata);
                                //-------------------------------------- dodawanie roku
                                string puste = string.Empty;
                                //MessageBox.Show(obecnewydarzenieNazapas);
                                DeleteTargetEntry entry = new DeleteTargetEntry(obecnewydarzenieNazapas, false, out puste);

                                string[] splicher = new string[20];
                                splicher = nazwaIdata.Split(new Char[] { '#', '.' });

                                //przypisuje wartosci stringow do integerow

                                string dzien = splicher[1];
                                int miesiac = Convert.ToInt32(splicher[2]);
                                int rok = Convert.ToInt32(splicher[3]);
                                string nowaDataRokJedenWiekszy;
                                string newMiesiac;


                                if (obecnewydarzenieNazapas.StartsWith("~895M"))
                                {
                                    int newDay = Convert.ToInt32(dzien);
                                    if (newDay > 28)
                                        dzien = "28";

                                    miesiac += 1;
                                    if (miesiac == 13)
                                    {
                                        rok += 1;
                                        miesiac = 1;
                                    }
                                    obecnewydarzenieNazapas = obecnewydarzenieNazapas.Remove(obecnewydarzenieNazapas.Length-8);
                                    obecnewydarzenieNazapas += " - " + dzien + "." + miesiac.ToString("00");
                                }
                                else
                                    rok += 1;

                                newMiesiac = miesiac.ToString("00");
                                nowaDataRokJedenWiekszy = "#" + dzien + "." + newMiesiac + "." + rok;
                                //----------------------------------------------------
                                using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\wydarzeniaCykliczne.txt"))
                                {


                                    sw.WriteLine(obecnewydarzenieNazapas + "X" + nowaDataRokJedenWiekszy/*+"n"*/);


                                }

                                dodajDatedoCyklicznego = false;
                                //usun tylko to konkretne wydarzenie cykliczne! jako hand delete dla niego 
                                //----------------------------------
                                DopisywanieWydarzenCyklicznych.DopiszWydarzenia();

                                
                            }
                            else
                            {

                                ilePozostaloDni = 0;
                                poprawnaOdmiana = "--- Przestarzałe";
                            }
                        }

                    }
                    else
                    {
                        //dla cyklicznych wydarzen
                        if (nazwaIdata.StartsWith(znakCyklicznosci) == true)
                        {
                            //MessageBox.Show("Znalazlem wydarzenie cykliczne!");
                            dodajDatedoCyklicznego = true;
                            obecnewydarzenieNazapas = nazwaIdata;
                        }
                        else
                        {
                            //dodatkowy string z nazwa na wszelki wypadek
                            obecnewydarzenieNazapas = nazwaIdata;
                        }
                    }

                }

            }
            
            //AllEvDataOut = AllEvData;
            EvDEADLINEOut = EvDEADLINE;
            EvDEADLINEWeekOut = EvDEADLINEWeek;
            NumberOfAllEvents = NumberOfAllEventsIn;
            //EvDEADLINEMonthOut = 56;
            //pozostalo miesiac i 3 miesiace



        }
    }
}

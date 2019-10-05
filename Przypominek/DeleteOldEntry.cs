using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Przypominek
{
    class DeleteOldEntry
    {
        public static void DeleteEntry()
        {
            string nowaDataMiesiacJedenWiekszy = string.Empty;
            string obecnewydarzenieNazapas = "";
            const string poczatekDaty = "#";
            string[] splitter = new string[2];

            string[] ArrayFromTextFile = File.ReadAllLines(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt");

            //kopiowanie starych danych do nowego pliku i usuwanie starego pliku pod nowy zapis
            File.Copy(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt", @"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\OLDnazwaIdataDIR.txt", true);
            File.Delete(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt");
            using (StreamWriter sw = File.CreateText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
            { }
            //-------------------------------------------------------


            foreach (string LineOfText in ArrayFromTextFile)
            {

                splitter = LineOfText.Split(new Char[] { 'X' });


                foreach (string nazwaIdata in splitter)
                {
                    if (nazwaIdata.StartsWith(poczatekDaty) == true)
                    {
                        //oddziel # od daty, poprawnie przekonwertuj zapisaną datę i oblicz ile dni pozostało do wydarzenia
                        //zapisz do wyswietlanego stringa datę + (przejście do następnej lini)



                        int ilePozostaloDni = DataCounterObliczacz.CounterObliczacz(nazwaIdata);


                        

                        if (ilePozostaloDni == -99)
                        {
                            if (obecnewydarzenieNazapas.StartsWith("~"))
                            {
                                continue;
                            }


                            //MessageBox.Show("UWAGA wykryłem i usunąłem stare wydarzenie!");
                            /*var result = MessageBox.Show("Czy dodać wydarzenie: " + obecnewydarzenieNazapas + " na następny miesiąc?", "Wykryłem stare wydarzenie do usunięcia!", MessageBoxButtons.YesNo);
                                if (result == DialogResult.Yes)
                                {
                                    string[] splicher = nazwaIdata.Split(new Char[] { '#', '.' });



                                    string dzien = splicher[1];
                                    int miesiac = Convert.ToInt32(splicher[2]);
                                    int rok = Convert.ToInt32(splicher[3]);
                                    if (Convert.ToInt32(dzien) > 28)
                                    {
                                        dzien = "28";
                                    }
                                    if (miesiac == 12)
                                    {
                                        miesiac = 1;
                                        rok += 1;
                                    }
                                    else
                                    {
                                        miesiac += 1;

                                    }

                                    if (miesiac < 10)
                                    {

                                        nowaDataMiesiacJedenWiekszy = "#" + dzien + ".0" + miesiac + "." + rok;
                                    }
                                    else
                                    {
                                        nowaDataMiesiacJedenWiekszy = "#" + dzien + "." + miesiac + "." + rok;
                                    }

                                    obecnewydarzenieNazapas = obecnewydarzenieNazapas.Remove(obecnewydarzenieNazapas.Length - 10);
                                    obecnewydarzenieNazapas += nowaDataMiesiacJedenWiekszy.TrimStart('#');
                                    using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
                                    {
                                        sw.WriteLine(obecnewydarzenieNazapas + "X" + nowaDataMiesiacJedenWiekszy);

                                    }
                                }*/
                            

                        }
                        else
                        {
                            using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
                            {
                                sw.WriteLine(obecnewydarzenieNazapas + "X" + nazwaIdata);

                            }
                        }



                    }
                    else
                    {
                        //dodatkowy string z nazwa do zapisu jako calosc w nowym wierszu
                        obecnewydarzenieNazapas = nazwaIdata;

                    }
                }
            }


          
        }
    }
}
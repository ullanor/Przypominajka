using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Przypominek
{
    class Sortownik
    {
        //public string DataDoObliczen = string.Empty;
        public int liczbaDni;

        //dla wydarzen porownywarka/ swapy
        public string A = string.Empty;
        public string B = string.Empty;

        public int A_days;
        public int B_days;

        public bool FirstEvent = true;

        public Sortownik(out int liczbaSwapow)
        {
            liczbaSwapow = 0;


            string[] ArrayFromTextFile = File.ReadAllLines(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt");
            //usun plik i stworz nowy
            File.Copy(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt", @"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\OLDnazwaIdataDIR.txt", true);
            File.Delete(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt");
            using (StreamWriter sw = File.CreateText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
            { }
            //------------------------------------------------
            foreach (string LINE in ArrayFromTextFile)
            {
                string[] separatorX = LINE.Split('X');


                if (separatorX.Length != 2)
                {
                    continue;
                }

                //DataDoObliczen = separatorX[1].TrimStart('#');
                //int liczbaDni = wrzucam datę do obliczenia liczby dni -----!!!!!----- zwraca liczbę dni pozostałych DAYS
                liczbaDni = DataCounterObliczacz.CounterObliczacz(separatorX[1]);

                if (FirstEvent == true)
                {
                    A = LINE;
                    A_days = liczbaDni;
                    FirstEvent = false;

                }
                else
                {
                    //2 przejscie foreacha - 2 wydarzenie
                    B = LINE;
                    B_days = liczbaDni;

                    //zapisuje wartosc mniejsza do nowego pliku
                    using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
                    {
                        if (A_days <= B_days)
                        {
                            sw.WriteLine(A);
                            A = B;
                            A_days = B_days;


                        }
                        else
                        {
                            sw.WriteLine(B);
                            liczbaSwapow += 1;
                        }

                    }


                }


            }
            using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
            {

                sw.WriteLine(A);

            }
            Console.WriteLine(liczbaSwapow);





        }
    }
}

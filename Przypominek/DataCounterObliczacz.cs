using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Przypominek
{
    class DataCounterObliczacz
    {
        public static int CounterObliczacz(string dataDoObliczenia)
        {
            //rozdziela zapisana date na 4 stringi (pusty, dzien, miesiac, rok)
            string[] splitter = new string[4];
            splitter = dataDoObliczenia.Split(new Char[] { '#', '.' });

            //przypisuje wartosci stringow do integerow
            int dzien = Convert.ToInt32(splitter[1]);
            int miesiac = Convert.ToInt32(splitter[2]);
            int rok = Convert.ToInt32(splitter[3]);

            //pobiera obecny czas z zegara systemowego i przypisuje przekonwertowane wartosci do daty naszego wydarzenia
            DateTime ObecnaData = DateTime.Now;
            DateTime dataWydarzenia = new DateTime(rok, miesiac, dzien, 0, 0, 0);

            //odejmuje daty i zwraca wartosc pozostalych dni
            TimeSpan pozostalyCzas = dataWydarzenia - ObecnaData;

            //przypadek kiedy mamy ten sam dzien albo kiedy juz po ptokach
            if (ObecnaData.Day == dataWydarzenia.Day & ObecnaData.Month == dataWydarzenia.Month & ObecnaData.Year == dataWydarzenia.Year)
            {
                
                return -1;
            }
            else if (ObecnaData.Day > dataWydarzenia.Day & ObecnaData.Month == dataWydarzenia.Month & ObecnaData.Year == dataWydarzenia.Year)
            {
                //tutaj zrobic funkcje pytajaca o usuniecie wydarzenia z pamieci bo jest nieaktualne
                
                return -99;
            }
            else if (pozostalyCzas.Days < 0)
            {
                //tutaj zrobic funkcje pytajaca o usuniecie wydarzenia z pamieci bo jest nieaktualne
                return -99;
            }

                return pozostalyCzas.Days;
        }
    }
}

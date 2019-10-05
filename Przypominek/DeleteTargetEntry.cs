using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace Przypominek
{
    class DeleteTargetEntry
    {
        
        public DeleteTargetEntry(string analizator,bool wyswietlaj,out string wydarzenieDoEdycji)
        {

            wydarzenieDoEdycji = string.Empty;
            string[] splitter = new string[2];
            bool znalazlWydarzenieDoEdycji = false;
            bool foundSthAtAll = false;
            //-----------------------------------------------


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

                if (splitter.Length != 2)
                {
                    continue;
                }


                if (splitter[0] == analizator && wyswietlaj == false)
                {
                    continue;
                }
                else if (splitter[0].StartsWith(analizator) == true && znalazlWydarzenieDoEdycji != true)
                {

                    foundSthAtAll = true;
                    //------------------------------------------||

                    if (wydarzenieDoEdycji != string.Empty)
                    {
                        znalazlWydarzenieDoEdycji = true;
                    }
                    //--------------------------------------------

                }
                else
                {

                    using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
                    {
                      sw.WriteLine(LineOfText);

                    }

                }

        

            }
            //wypisz jezeli nie znalazlo zadnej odpowiadajcej frazy
            if (foundSthAtAll == false && wyswietlaj == true)
            {
                MessageBox.Show("Nie znalazłem takiego wydarzenia!");
            }


        }
                
    }


}


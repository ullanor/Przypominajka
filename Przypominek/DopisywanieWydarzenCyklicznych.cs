using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;


namespace Przypominek
{
    class DopisywanieWydarzenCyklicznych
    {
        public static void DopiszWydarzenia()
        {
            if (!File.Exists(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\wydarzeniaCykliczne.txt"))
            { 
                using (StreamWriter sw = File.CreateText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\wydarzeniaCykliczne.txt"))
                { }

            }
            
            if (new FileInfo(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\wydarzeniaCykliczne.txt").Length != 0)
            {
                
                string[] ArrayFromTextFile = File.ReadAllLines(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\wydarzeniaCykliczne.txt");

                foreach (string lineOfCykliczneWydarzenia in ArrayFromTextFile)
                {

                        using (StreamWriter sw = File.AppendText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\nazwaIdataDIR.txt"))
                        {


                            sw.WriteLine(lineOfCykliczneWydarzenia +"\n");
                        


                        }

                }


                File.Delete(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\wydarzeniaCykliczne.txt");
                using (StreamWriter sw = File.CreateText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\wydarzeniaCykliczne.txt"))
                { }
                
            }
        }
    }
}

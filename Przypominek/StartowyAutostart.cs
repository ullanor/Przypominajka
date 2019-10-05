using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Win32;
using System.Security.Permissions;

namespace Przypominek
{
    class StartowyAutostart
    {
        public static bool StartowyAuto()
        {

            if (File.Exists(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\PrzypominekDATA.txt"))
            {
                return true;
            }
            else
            {

                using (StreamWriter sw = File.CreateText(@"C:\Program Files (x86)\Ullanor Company\PrzyPominajka\PrzypominekDATA.txt"))
                { }

                using (RegistryKey Run = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    
                    Run.SetValue("Przypominek", System.Reflection.Assembly.GetExecutingAssembly().Location);
                }

                return true;
            }



        }
    }
}

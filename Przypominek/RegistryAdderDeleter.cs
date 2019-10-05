using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Microsoft.Win32;
using System.Security.Permissions;

namespace Przypominek
{
    class RegistryAdderDeleter
    {
        public static void AutostartEditor()
        {
             bool IsStartupItem()
             {
                RegistryKey Run = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (Run.GetValue("Przypominek") == null)
                    // Jeżeli nie została znaleziona taka wartość w rejestrze
                    return false;
                else
                    // Jeżeli została znaleziona taka wartość w rejestrze
                    return true;
             }
            if (IsStartupItem() == true)
            {
                Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true).DeleteValue("Przypominek");
                MessageBox.Show("Aplikacja usunięta z AutoStartu");
            }
            else
            {

                //dodawanie
                using (RegistryKey Run = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    // Create data for the Autorun of our program.
                    Run.SetValue("Przypominek", System.Reflection.Assembly.GetExecutingAssembly().Location);
                }
                MessageBox.Show("Aplikacja dodana do AutoStartu");
            }
        }
    }
}

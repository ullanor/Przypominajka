using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace Przypominek
{
    class przypominajkaUpdate
    {
        
        public static bool checkForP_Updates()
        {
            var oknoCzyUpdate = MessageBox.Show("Czy chcesz sprawdzić aktualizacje?", "Witamy w Przypominajce", MessageBoxButtons.YesNo);
            if (oknoCzyUpdate == DialogResult.No)
            {
                return false;
            }

            string myCurrentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //MessageBox.Show(Assembly.GetExecutingAssembly().GetName().Version.ToString());

            string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string pathApp = path + @"\VirtualStore\Program Files (x86)\Ullanor Company\PrzyPominajka\UninstallPrzypominajka.msi";
            path += @"\VirtualStore\Program Files (x86)\Ullanor Company\PrzyPominajka\currentVersionCheck.txt";


            try
            {
                WebClient wc = new WebClient();
                wc.DownloadFile("http://geldonia.cba.pl/PrzypominajkaUpdate/PPCurrentV.txt", path);

                //MessageBox.Show("Pobrano!");
                string[] ArrayFromTextFile = File.ReadAllLines(path);


                if (ArrayFromTextFile[0] != myCurrentVersion)
                {
                    //MessageBox.Show("Dostępna jest nowsza wersja!");
                    var result = MessageBox.Show("Dostępna jest nowsza wersja programu!\nPolecam zainstalować teraz :)", "Wykryto nowszą PRZYPOMINAJKĘ!", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {

                        wc.DownloadFile("http://geldonia.cba.pl/PrzypominajkaUpdate/UninstallPrzypominajka.msi", pathApp);
                        Process.Start(new ProcessStartInfo()
                        {
                            FileName = pathApp,
                            UseShellExecute = true
                        });
                        File.Delete(path);
                        return true;


                    }
                    else { MessageBox.Show("Szkoda!", "", MessageBoxButtons.OK, MessageBoxIcon.Error); File.Delete(path); return false; }

                   
                }
                else
                {
                    MessageBox.Show("Posiadamy najnowszą wersję przypominajki!");
                    File.Delete(path);
                    return false;
                    
                }



            }
            catch (Exception ex)
            {
                //brak polaczenia z netem lub blad servera
                MessageBox.Show("Brak połączenia z internetem lub błąd servera\nPrzypominajka działa w trybie Offline\n\n"+ex.ToString());
                //MessageBox.Show(ex.ToString());
                return false;
                
            }
        }

    }
}

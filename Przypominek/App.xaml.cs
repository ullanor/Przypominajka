using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;



namespace Przypominek
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary> StartupUri="BasicMainWindow.xaml">
    public partial class App : Application
    {
        public static bool LaunchedViaStartup { get; set; }

        [STAThread]
        static void Main(string[] args)
        {
            LaunchedViaStartup = args != null && args.Any(arg => arg.Equals("startup", StringComparison.CurrentCultureIgnoreCase));
            
            RunApplication();
        }
        private static void RunApplication()
        {


            if (LaunchedViaStartup == true)
            {
                //MessageBox.Show("przez startup shortcata");

                var application = new App();
                application.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
                application.InitializeComponent();
                application.Run();


                //SetMyProgramTimer();
            }
            else
            {
                var application = new App();
                application.StartupUri = new System.Uri("BasicMainWindow.xaml", System.UriKind.Relative);
                application.InitializeComponent();
                application.Run();
            }
        }
       
    }
}

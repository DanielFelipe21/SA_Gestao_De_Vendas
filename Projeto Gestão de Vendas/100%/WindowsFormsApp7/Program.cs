using System;
using System.Windows.Forms;
using WindowsFormsApp7.Forms;

namespace WindowsFormsApp7
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new menuPrincipal());
        }
    }
}

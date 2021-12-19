using System;
using System.Windows.Forms;

namespace RconTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try { Application.Run(new App()); }
            catch (System.ObjectDisposedException e) { return; }
        }
    }
}

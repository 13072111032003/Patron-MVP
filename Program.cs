using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PatrónMVP.Models;
using PatrónMVP.Presenters;
using PatrónMVP._Repositories;
using PatrónMVP.Views;
using System.Configuration;

namespace PatrónMVP
{
    static class Program
    {
        // El principal punto de entrada para la aplicación.

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            IMainView view = new MainView();           
            new MainPresenter(view,sqlConnectionString);
            Application.Run((Form)view);
        }
    }
}

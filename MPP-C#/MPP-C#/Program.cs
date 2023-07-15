using log4net.Config;
using MPP_C_.domain;
using MPP_C_.repository.repos;
using System.Configuration;

namespace MPP_C_
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.


            XmlConfigurator.Configure(new System.IO.FileInfo("Test.txt"));
            IDictionary<string,string> props = new SortedList<string,string>();
            props.Add("ConnectionString", GetConnectionStringByName("Competition"));
            Application.Run(new LogInForm(props));
        }

        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}
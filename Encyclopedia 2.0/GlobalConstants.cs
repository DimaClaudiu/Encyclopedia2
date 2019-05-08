using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encyclopedia_2._0
{
    class GlobalConstants
    {
        static string version = "0.92";
        public static string Version { get { return version; } }

        static string resourcesPath = @"Resources";
        public static string ResourcesPath { get { return resourcesPath; } }

        static string databaseConnectionPath = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\QuickEntries.mdf;Integrated Security=True";
        public static string DatabaseConnectionPath { get { return databaseConnectionPath; } }

        static bool minimizeToTray = true;
        public static bool MinimizeToTray { get { return minimizeToTray; } set { minimizeToTray = value; } }

    }
}

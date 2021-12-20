using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebSite.App_Code
{
    /// <summary>
    /// Logging all details of downloads to DownloadsLog.txt
    /// LogDownloadDetailsWin() is called in DbConnections.GetDownloadDetailsWin()
    /// LogDownloadDetailsMac() is called in DbConnections.GetDownloadDetailsMac()
    /// </summary>
    public sealed class DownloadDetailsUtility
    {
        // All methods are static, so this can be private
        private DownloadDetailsUtility()
        { }

        // Log all Windows downloads
        public static void LogDownloadDetailsWin(string downloadDetails)
        {
            // Include logic for logging download details
            // Get the absolute path to the log file
            string logFile = "~/App_Data/DownloadsLog.txt";
            logFile = HttpContext.Current.Server.MapPath(logFile);

            // Open the log file for append and write the log
            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            if (downloadDetails != null)
            {
                sw.WriteLine("Download Details - Windows:");
                sw.WriteLine(downloadDetails.ToString());
            }
            sw.Close();
        }

        // Log all Mac downloads
        public static void LogDownloadDetailsMac(string downloadDetails)
        {
            // Include logic for logging download details
            // Get the absolute path to the log file
            string logFile = "~/App_Data/DownloadsLog.txt";
            logFile = HttpContext.Current.Server.MapPath(logFile);

            // Open the log file for append and write the log
            StreamWriter sw = new StreamWriter(logFile, true);
            sw.WriteLine("********** {0} **********", DateTime.Now);
            if (downloadDetails != null)
            {
                sw.WriteLine("Download Details - Mac:");
                sw.WriteLine(downloadDetails.ToString());
            }
            sw.Close();
        }
    }
}
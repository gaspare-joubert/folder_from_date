using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// GlobalVariables
/// </summary>
namespace WebSite
{
    public class GlobalVariables
    {
        public const string _errMsgStd = "An error has occurred.";
        public static string _errMsgClr = string.Empty;
        private static string _errMsg;
        private static string _appLanguage = "English";
        private static string _appName = "Sort Those Pics";
        private static string _appGuid = "e7f5ce6e-875c-43b3-918f-cb5c7c8e41b4";
        private static string _appVersion = "1.0.0.0.";
        private static string _stpSmtp = "mail.sort-those-pics.com"; //Use mail.sort-those-pics.com with SSL Disabled; herzog.aserv.co.za with SSL Enabled.
        private static string _stpPort = "587"; //Use 587 with SSL Disabled; 465 with SSL Enabled; 25 with localhost.
        private static string _stpEmailUsername = "sales@sort-those-pics.com";
        private static string _stpEmailPassw = "W:;F^S~j2e=dM"; //Escape characters were used (\).
        private static string _cmpnyName = "Gasco Solutions (Pty) Ltd.";
        private static string _templateFilePathContactUsHtml = HttpContext.Current.Server.MapPath("Resources/stp_contact_us_html_template.html");
        
        public GlobalVariables()
        {
            
        }
        
        public static string ErrMsg
        {
            get { return _errMsg; }
            set { _errMsg = value; }
        }

        public static string AppLanguage
        {
            get { return _appLanguage; }
            set { _appLanguage = value; }
        }

        public static string AppName
        {
            get { return _appName; }
            set { _appName = value; }
        }

        public static string AppGuid
        {
            get { return _appGuid; }
            set { _appGuid = value; }
        }

        public static string AppVersion
        {
            get { return _appVersion; }
            set { _appVersion = value; }
        }

        public static string StpSmtp
        {
            get { return _stpSmtp; }
            set { _stpSmtp = value; }
        }

        public static string StpPort
        {
            get { return _stpPort; }
            set { _stpPort = value; }
        }

        public static string StpEmailUsername
        {
            get { return _stpEmailUsername; }
            set { _stpEmailUsername = value; }
        }

        public static string StpEmailPassw
        {
            get { return _stpEmailPassw; }
            set { _stpEmailPassw = value; }
        }

        public static string CmpnyName
        {
            get { return _cmpnyName; }
            set { _cmpnyName = value; }
        }

        public static string TemplateFilePathContactUsHtml
        {
            get { return _templateFilePathContactUsHtml; }
            set { _templateFilePathContactUsHtml = value; }
        }
    }

    public class GetStdTimeStamp
    {
        /// <summary>
        /// Set system time format as: year/month/day " " Hr:Min:Sec.MilSec
        /// </summary>
        
        private static DateTime _stdTimeStampFormat; //Set the TimeStamp format to yyyy//mm//dd        

        public GetStdTimeStamp()
        {
            int dtDay = DateTime.Now.Day;
            int dtMth = DateTime.Now.Month;
            int dtYr = DateTime.Now.Year;
            int dtHr = DateTime.Now.Hour;
            int dtMin = DateTime.Now.Minute;
            int dtSec = DateTime.Now.Second;
            int dtMilSec = DateTime.Now.Millisecond;
            _stdTimeStampFormat = Convert.ToDateTime(dtYr + "/" + dtMth + "/" + dtDay + " " + dtHr + ":" + dtMin + ":" + dtSec + "." + dtMilSec);
        }
        
        public static DateTime StdTimeStampFormat
        {
            get { return _stdTimeStampFormat; }
            set { _stdTimeStampFormat = value; }
        }
    }
}



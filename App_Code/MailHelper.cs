using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net.Mail;
using System.IO;
using WebSite;
using WebSite.App_Code;

namespace WebSite
{
    public class MailHelper
    {
        string assemblyTitle = GlobalVariables.AppName;
        string assemblyCmpny = GlobalVariables.CmpnyName;

        //Pop3 or IMAP settings:
        SmtpClient client;
        MailMessage mm;    
        
        public MailHelper()
        {
            //Pop3 or IMAP settings:
            client = new SmtpClient();
            client.Host = GlobalVariables.StpSmtp.ToString();

            if (!(string.IsNullOrEmpty(GlobalVariables.StpPort)))
            {
                client.Port = Convert.ToInt16(GlobalVariables.StpPort);
            }

            client.UseDefaultCredentials = false;            
            client.EnableSsl = false;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential(GlobalVariables.StpEmailUsername, GlobalVariables.StpEmailPassw);
            mm = new MailMessage();
        }

        public void SendMailMessageContactUs(string fromAddress, string toAddress, string _sRNo,
            string signatureFrom, string formSubject, string formMessage, string formEmail)
        {
            string strPath = GlobalVariables.TemplateFilePathContactUsHtml;
            string strEmailBody = string.Empty;

            try
            {
                strEmailBody = File.ReadAllText(strPath);                
            }
            catch (Exception ex)
            {
                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                ExceptionUtility.LogException(ex, "Method: SendMailMessageContactUs; Class: MailHelper");
            }
            finally
            {
                if (string.IsNullOrEmpty(GlobalVariables.ErrMsg))
                {
                    SendContactUsPop3(fromAddress, toAddress, strEmailBody, _sRNo, formSubject, formMessage, signatureFrom, formEmail);
                }
            }
        }

        private void SendContactUsPop3(string fromAddress, string toAddress,
            string strEmailBody, string _sRNo,
            string formSubject, string formMessage, string signatureFrom, string formEmail)
        {
            strEmailBody = strEmailBody.Replace("#assemblyTitle#", assemblyTitle);
            strEmailBody = strEmailBody.Replace("#sr_no#", _sRNo);
            strEmailBody = strEmailBody.Replace("#subject#", formSubject);
            strEmailBody = strEmailBody.Replace("#message#", formMessage);
            strEmailBody = strEmailBody.Replace("#signature_from#", signatureFrom);
            strEmailBody = strEmailBody.Replace("#formEmail#", formEmail);

            try
            {
                string subject = assemblyTitle + " Contact Us: " + _sRNo;
                
                mm.From = new MailAddress(fromAddress);
                mm.To.Add(toAddress);
                mm.Subject = subject;
                mm.IsBodyHtml = true;
                mm.Body = strEmailBody;
                client.Send(mm);
            }
            catch (Exception ex)
            {
                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                ExceptionUtility.LogException(ex, "Method: SendContactUsPop3; Class: MailHelper");
            }
        }
    }
}

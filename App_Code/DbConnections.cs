using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite;
using EncryptionOnly;
using WebSite.App_Code;
using System.Data.Entity.Infrastructure;

/// <summary>
/// All connections to db SortThosePics
/// </summary>
public class DbConnections
{
	public DbConnections()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void InsertUserWin(string userFName, string userSName, string userEmail)
    {
        try
        {
            Guid g;
            g = Guid.NewGuid();
            GetStdTimeStamp getTs = new GetStdTimeStamp();
            DateTime ts = GetStdTimeStamp.StdTimeStampFormat;
            string encFName = EncryptionOnly.Rijndael.Encrypt(userFName);
            string encSName = EncryptionOnly.Rijndael.Encrypt(userSName);
            string encUserEmail = EncryptionOnly.Rijndael.Encrypt(userEmail);
            
            using (EF.SortThosePicsEntities ctx = new EF.SortThosePicsEntities())
            {
                EF.stp_try01_for_free_win try01 = new EF.stp_try01_for_free_win();
                
                try01.try01_win_guid = g;
                try01.try01_win_timestamp = ts;
                try01.try01_win_language = GlobalVariables.AppLanguage;
                try01.try01_win_app_name = GlobalVariables.AppName;
                try01.try01_win_app_guid = GlobalVariables.AppGuid;
                try01.try01_win_app_version = GlobalVariables.AppVersion;
                try01.try01_win_user_fname = encFName;
                try01.try01_win_user_surname = encSName;
                try01.try01_win_user_email = encUserEmail;

                ctx.stp_try01_for_free_win.Add(try01);
                ctx.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
            ExceptionUtility.LogException(ex, "Method: InsertUserWin; Class: DbConnections");
        }
    }

    public void InsertUserMac(string userFName, string userSName, string userEmail)
    {
        try
        {
            Guid g;
            g = Guid.NewGuid();
            GetStdTimeStamp getTs = new GetStdTimeStamp();
            DateTime ts = GetStdTimeStamp.StdTimeStampFormat;
            string encFName = EncryptionOnly.Rijndael.Encrypt(userFName);
            string encSName = EncryptionOnly.Rijndael.Encrypt(userSName);
            string encUserEmail = EncryptionOnly.Rijndael.Encrypt(userEmail);

            using (EF.SortThosePicsEntities ctx = new EF.SortThosePicsEntities())
            {
                EF.stp_try02_for_free_mac try02 = new EF.stp_try02_for_free_mac();

                try02.try02_mac_guid = g;
                try02.try02_mac_timestamp = ts;
                try02.try02_mac_language = GlobalVariables.AppLanguage;
                try02.try02_mac_app_name = GlobalVariables.AppName;
                try02.try02_mac_app_guid = GlobalVariables.AppGuid;
                try02.try02_mac_app_version = GlobalVariables.AppVersion;
                try02.try02_mac_user_fname = encFName;
                try02.try02_mac_user_surname = encSName;
                try02.try02_mac_user_email = encUserEmail;

                ctx.stp_try02_for_free_mac.Add(try02);
                ctx.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
            ExceptionUtility.LogException(ex, "Method: InsertUserMac; Class: DbConnections");
        }
    }

    public void InsertUserMsg(string userFName, string userSName, string userEmail, string msgSubject, string msgMessage, Guid g)
    {
        try
        {
            GetStdTimeStamp getTs = new GetStdTimeStamp();
            DateTime ts = GetStdTimeStamp.StdTimeStampFormat;
            string encFName = EncryptionOnly.Rijndael.Encrypt(userFName);
            string encSName = EncryptionOnly.Rijndael.Encrypt(userSName);
            string encUserEmail = EncryptionOnly.Rijndael.Encrypt(userEmail);

            using (EF.SortThosePicsEntities ctx = new EF.SortThosePicsEntities())
            {
                EF.stp_contact_us us = new EF.stp_contact_us();

                us.contact_us_guid = g;
                us.contact_us_timestamp = ts;
                us.contact_us_language = GlobalVariables.AppLanguage;
                us.contact_us_app_name = GlobalVariables.AppName;
                us.contact_us_app_guid = GlobalVariables.AppGuid;
                us.contact_us_app_version = GlobalVariables.AppVersion;
                us.contact_us_user_fname = encFName;
                us.contact_us_user_surname = encSName;
                us.contact_us_user_email = encUserEmail;
                us.contact_us_subject = msgSubject;
                us.contact_us_message = msgMessage;

                ctx.stp_contact_us.Add(us);
                ctx.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
            ExceptionUtility.LogException(ex, "Method: InsertUserMsg; Class: DbConnections");
        }
    }

    public void UpdateDownloadTotal()
    {
        bool err = false;
        int recordCountCurrent = new int();
        int recordCountUpdated = new int();
        List<EF.stp_download_counter> qry = new List<EF.stp_download_counter>();

        using (EF.SortThosePicsEntities ctx = new EF.SortThosePicsEntities())
        {
            try
            {
                qry = (from c in ctx.stp_download_counter
                       select c).ToList();
            }
            catch (Exception ex)
            {
                err = true;
                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                ExceptionUtility.LogException(ex, "Method: UpdateDownloadTotal; Class: DbConnections");
            }
            finally
            {
                if (err == false)
                {
                    switch (qry.Count)
                    {
                        case 0:
                            EF.stp_download_counter cntr = new EF.stp_download_counter();

                            recordCountCurrent = 0;
                            recordCountUpdated = recordCountCurrent + 1;
                            cntr.dwnld_cntr_downloads_total = recordCountUpdated;

                            try
                            {
                                ctx.stp_download_counter.Add(cntr);
                                ctx.SaveChanges();
                            }
                            catch (InvalidOperationException ex)
                            {
                                err = true;
                                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                                ExceptionUtility.LogException(ex, "Method: UpdateDownloadTotal; Class: DbConnections");
                            }
                            catch (DbUpdateException ex)
                            {
                                err = true;
                                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                                ExceptionUtility.LogException(ex, "Method: UpdateDownloadTotal; Class: DbConnections");
                            }
                            break;
                        case 1:
                            recordCountCurrent = qry[0].dwnld_cntr_downloads_total;
                            recordCountUpdated = recordCountCurrent + 1;

                            foreach (EF.stp_download_counter result in qry)
                            {
                                result.dwnld_cntr_downloads_total = recordCountUpdated;
                            }

                            try
                            {
                                ctx.SaveChanges();
                            }
                            catch (InvalidOperationException ex)
                            {
                                err = true;
                                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                                ExceptionUtility.LogException(ex, "Method: UpdateDownloadTotal; Class: DbConnections");
                            }
                            catch (DbUpdateException ex)
                            {
                                err = true;
                                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                                ExceptionUtility.LogException(ex, "Method: UpdateDownloadTotal; Class: DbConnections");
                            }
                            break;
                        default:
                            err = true;
                            break;

                    }
                }
            }
        }
    }

    public void GetDownloadDetailsWin()
    {
        bool err = false;
        List<EF.stp_try01_for_free_win> qry = new List<EF.stp_try01_for_free_win>();

        using (EF.SortThosePicsEntities ctx = new EF.SortThosePicsEntities())
        {
            try
            {
                qry = (from c in ctx.stp_try01_for_free_win
                       select c).ToList();
            }
            catch (Exception ex)
            {
                err = true;
                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                ExceptionUtility.LogException(ex, "Method: GetDownloadDetailsWin; Class: DbConnections");
            }
            finally
            {
                if (err == false)
                {
                    switch (qry.Count)
                    {
                        case 0:
                            //Do nothing.
                            break;
                        default:
                            try
                            {
                                string downloadDetails = null;
                                foreach (EF.stp_try01_for_free_win result in qry)
                                {
                                    downloadDetails += result.try01_win_user_fname + " " + result.try01_win_user_surname + " " + result.try01_win_user_email + Environment.NewLine; ; 
                                }

                                DownloadDetailsUtility.LogDownloadDetailsWin(downloadDetails); 
                            }
                            catch (Exception ex)
                            {
                                err = true;
                                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                                ExceptionUtility.LogException(ex, "Method: GetDownloadDetailsWin; Class: DbConnections");
                            }
                            break;

                    }
                }
            }
        }
    }

    public void GetDownloadDetailsMac()
    {
        bool err = false;
        List<EF.stp_try02_for_free_mac> qry = new List<EF.stp_try02_for_free_mac>();

        using (EF.SortThosePicsEntities ctx = new EF.SortThosePicsEntities())
        {
            try
            {
                qry = (from c in ctx.stp_try02_for_free_mac
                       select c).ToList();
            }
            catch (Exception ex)
            {
                err = true;
                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                ExceptionUtility.LogException(ex, "Method: GetDownloadDetailsMac; Class: DbConnections");
            }
            finally
            {
                if (err == false)
                {
                    switch (qry.Count)
                    {
                        case 0:
                            //Do nothing.
                            break;
                        default:
                            try
                            {
                                string downloadDetails = null;
                                foreach (EF.stp_try02_for_free_mac result in qry)
                                {
                                    downloadDetails += result.try02_mac_user_fname + " " + result.try02_mac_user_surname + " " + result.try02_mac_user_email + Environment.NewLine; ;
                                }

                                DownloadDetailsUtility.LogDownloadDetailsMac(downloadDetails);
                            }
                            catch (Exception ex)
                            {
                                err = true;
                                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                                ExceptionUtility.LogException(ex, "Method: GetDownloadDetailsMac; Class: DbConnections");
                            }
                            break;

                    }
                }
            }
        }
    }
}
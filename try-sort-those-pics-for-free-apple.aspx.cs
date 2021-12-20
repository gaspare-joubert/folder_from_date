using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite;
using WebSite.App_Code;
using EF;
using System.IO;
using Ionic.Zip;

public partial class try_apple : System.Web.UI.Page
{
    private static string formFname = string.Empty;
    private static string formSname = string.Empty;
    private static string formEmail = string.Empty;
    private static string formResponse = string.Empty;
    private const string formResponseSuccess = "Thank you for trying Sort-Those-Pics";
    private const string formResponseFail = GlobalVariables._errMsgStd;
    DbConnections db = new DbConnections();                
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            if (formResponse == formResponseSuccess)
            {
                formResponse = string.Empty;
            }
            else
            {
                formResponse = formResponseSuccess;
            }
        }
        else
        {
            ResetForm();            
        }
    }

    protected void ResetForm()
    {
        formResponse = string.Empty;
    }

    //1. Validate fields for server side.
    //2. Save user to db tbl.
    //3. Allow user to download files, when available.
    //4. Increase download count, when available.
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            //1. Validate fields for server side.
            ValidateFields();

            if (!(string.IsNullOrEmpty(GlobalVariables.ErrMsg)))
            {
                //Error.
                ResetFormDone();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", "<script type=\"text/javascript\">$('#ErrorModal').modal('show');</script>", false); //This works once validation succeeds
            }
            else
            {
                //2. Save user to db tbl.
                db.InsertUserMac(formFname, formSname, formEmail);

                if (!(string.IsNullOrEmpty(GlobalVariables.ErrMsg)))
                {
                    //Error.
                    GlobalVariables.ErrMsg = GlobalVariables._errMsgClr;
                    ResetFormDone();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", "<script type=\"text/javascript\">$('#ErrorModal').modal('show');</script>", false); //This works once validation succeeds
                }
                else
                {
                    //3.  Increase download count &  4. Allow user to download files.
                    //DownloadFile();
                }
            }
        }
    }

    //3.  Increase download count &  4. Allow user to download files.
    protected void DownloadFile()
    {
        bool err = false;

        string dirToConfirmMain = "~/FileDownloads/Website/Apple/";
        string dirToConfirmPathMain = Server.MapPath(dirToConfirmMain);

        string dirToConfirmSub = "~/FileDownloads/Website/Apple/stp";
        string dirToConfirmPathSub = Server.MapPath(dirToConfirmSub);

        try
        {
            if (Directory.Exists(dirToConfirmPathSub) && Directory.EnumerateFiles(dirToConfirmPathSub).Any())
            {
                err = false;
            }
            else
            {
                err = true;
            }

        }
        catch (HttpException ex)
        {
            err = true;
            GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
            ExceptionUtility.LogException(ex, "Method: DownloadFile; Class: try_apple");
        }

        if (err == false)
        {
            try
            {
                //3. Increase download count.
                db.UpdateDownloadTotal();
                ResetFormDone();
            }
            catch (HttpException ex)
            {
                err = true;
                GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
                ExceptionUtility.LogException(ex, "Method: DownloadFile; Class: try_apple");
            }

            if (err == false)
            {
                //4. Allow user to download files.
                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    DirectoryInfo f = new DirectoryInfo(dirToConfirmPathMain);
                    DirectoryInfo[] sub = f.GetDirectories();

                    for (int i = 0; i < sub.Length; i++)
                    {
                        zip.AddDirectory(dirToConfirmPathMain + @"\" + sub[i].Name);
                    }

                    Response.Clear();
                    Response.BufferOutput = false;
                    string zipName = String.Format("Sort-Those-Pics.zip");
                    Response.ContentType = "application/zip";
                    Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        else
        {
            //Error.
            GlobalVariables.ErrMsg = GlobalVariables._errMsgClr;
            ResetFormDone();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", "<script type=\"text/javascript\">$('#ErrorModal2').modal('show');</script>", false);
        }
    }

    protected void ResetFormDone()
    {
        formFname = string.Empty;
        formSname = string.Empty;
        formEmail = string.Empty;
    }

    protected void ValidateFields()
    {
        GlobalVariables.ErrMsg = string.Empty;

        if (string.IsNullOrWhiteSpace(Request.Form["FName"]))
        {
            formFname = string.Empty;
        }
        else
        {
            formFname = Request.Form["FName"].Trim();
        }

        if (string.IsNullOrWhiteSpace(Request.Form["LName"]))
        {
            formSname = string.Empty;
        }
        else
        {
            formSname = Request.Form["LName"].Trim();
        }

        if (string.IsNullOrWhiteSpace(Request.Form["userEmail"]))
        {
            formEmail = string.Empty;
        }
        else
        {
            formEmail = Request.Form["userEmail"].Trim();
        }

        //Validate First Name.
        ValidteFieldsFName();

        //Validate Last Name.
        ValidteFieldsLName();

        //Validate User E-mail.
        ValidteFieldsUserEMail();
    }

    //Validate First Name.
    protected void ValidteFieldsFName()
    {
        int errCount = 0;
        
        if (string.IsNullOrEmpty(formFname))
        {
            errCount++;
        }
        else
        {
            errCount = 0;
        }

        if (errCount != 0)
        {
            GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
        }
    }

    //Validate Last Name.
    protected void ValidteFieldsLName()
    {
        int errCount = 0;

        if (string.IsNullOrEmpty(formSname))
        {
            errCount++;
        }
        else
        {
            errCount = 0;
        }

        if (errCount != 0)
        {
            GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
        }
    }

    //Validate User E-mail.
    protected void ValidteFieldsUserEMail()
    {
        int errCount = 0;

        if (string.IsNullOrEmpty(formEmail))
        {
            errCount++;
        }
        else
        {
            errCount = 0;
        }

        if (errCount != 0)
        {
            GlobalVariables.ErrMsg = GlobalVariables._errMsgStd;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSite;

public partial class preview_dotnet_templates_with_out_masterpages_one_page_Scroll_nav_index : System.Web.UI.Page
{
    private static string formResponse = string.Empty;
    private const string formResponseSuccess = "Thank you for contacting us.";
    private static string formFname = string.Empty;
    private static string formSname = string.Empty;
    private static string formEmail = string.Empty;
    private static string formSubject = string.Empty;
    private static string formMessage = string.Empty;
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
    //3. Send e-mail.
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
                //2. Save message to db tbl.
                Guid g;
                g = Guid.NewGuid();

                db.InsertUserMsg(formFname, formSname, formEmail, formSubject, formMessage, g);

                if (!(string.IsNullOrEmpty(GlobalVariables.ErrMsg)))
                {
                    //Error.
                    GlobalVariables.ErrMsg = GlobalVariables._errMsgClr;
                    ResetFormDone();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myKey", "<script type=\"text/javascript\">$('#ErrorModal').modal('show');</script>", false); //This works once validation succeeds
                }
                else
                {
                    //3.  Send e-mail.
                    //string fromAddress = formFname + " " + formSname + "<" + GlobalVariables.StpEmailUsername + ">";
                    string fromAddress = GlobalVariables.StpEmailUsername;
                    string toAddress = GlobalVariables.StpEmailUsername;
                    string _sRNo = g.ToString();
                    string signatureFrom = formFname + " " + formSname;
                    
                    WebSite.MailHelper mH = new WebSite.MailHelper();
                    mH.SendMailMessageContactUs(fromAddress, toAddress, _sRNo, signatureFrom, formSubject, formMessage, formEmail);
                }
            }
        }
    }

    protected void ResetFormDone()
    {
        formFname = string.Empty;
        formSname = string.Empty;
        formEmail = string.Empty;
        formSubject = string.Empty;
        formMessage = string.Empty;
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

        if (string.IsNullOrWhiteSpace(Request.Form["Subject"]))
        {
            formSubject = string.Empty;
        }
        else
        {
            formSubject = Request.Form["Subject"].Trim();
        }

        if (string.IsNullOrWhiteSpace(Request.Form["Message"]))
        {
            formMessage = string.Empty;
        }
        else
        {
            formMessage = Request.Form["Message"].Trim();
        }

        //Validate First Name.
        ValidteFieldsFName();

        //Validate Last Name.
        ValidteFieldsLName();

        //Validate User E-mail.
        ValidteFieldsUserEMail();

        //Validate Subject.
        ValidteFieldsSubject();

        //Validate Message.
        ValidteFieldsMessage();
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

    //Validate Subject.
    protected void ValidteFieldsSubject()
    {
        int errCount = 0;

        if (string.IsNullOrEmpty(formSubject))
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

    //Validate Message.
    protected void ValidteFieldsMessage()
    {
        int errCount = 0;

        if (string.IsNullOrEmpty(formMessage))
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
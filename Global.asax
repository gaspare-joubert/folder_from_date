<%@ Application Language="C#" %>
<%@ Import Namespace="WebSite" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
    }

    // Code that runs when an unhandled error occurs.
    void Application_Error(object sender, EventArgs e)
    {
        // Get last error from the server
        Exception exc = Server.GetLastError();

        if (exc is HttpUnhandledException)
        {
            if (exc.InnerException != null)
            {
                exc = new Exception(exc.InnerException.Message);
                Server.Transfer("ErrorPage.aspx?handler=Application_Error%20-%20Global.asax",
                    true);
            }
        }
    }

</script>

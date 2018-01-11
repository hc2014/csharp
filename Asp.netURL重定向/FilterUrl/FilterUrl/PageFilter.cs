using System;
using System.Web;
using System.Web.SessionState;

namespace FilterUrl
{
    public class PageFilter : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication application)
        {
            application.AcquireRequestState += new EventHandler(Application_AcquireRequestState);
        }

        private void Application_AcquireRequestState(Object source, EventArgs e)
        {

            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;
            HttpSessionState session = context.Session;
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            String contextPath = request.ApplicationPath;
            var url = app.Request.Url;
            if (url.ToString().Contains("test.aspx"))
            {
                response.Redirect("Login.aspx");


                //app.Response.Write("非法路径");
                //app.Response.End();
            }

        }

    }
}
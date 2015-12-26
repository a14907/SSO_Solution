using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Site
{
    public class BasePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CheckLogin())
            {
                return;
            }
            else
            {
                //提示未登录，是否登录
                Response.Write("<script language='javascript'>window.location='/IsLogin.aspx'</script>");
                Response.End();
            }
        }

        private bool CheckLogin()
        {
            if (Session["IsLogin"] != null)
            {
                return (bool)Session["IsLogin"];
            }
            if (Request.QueryString["token"] == null)
            {
                return false;
            }
            string token = Server.UrlEncode(Request.QueryString["token"]);
            //远程验证Token
            WebRequest req = HttpWebRequest.Create(CommonHelper.SsoUrl);
            req.Method = "POST";
            req.Timeout = 300 * 1000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] buf = Encoding.Default.GetBytes("token=" + token);
            req.ContentLength = buf.Length;
            var dataStream = req.GetRequestStream();
            dataStream.Write(buf, 0, buf.Length);
            dataStream.Close();
            var res = req.GetResponse();
            dataStream = res.GetResponseStream();
            string resStr = "";
            if (dataStream != null)
            {
                var reader = new StreamReader(dataStream);
                resStr = reader.ReadToEnd();
                reader.Dispose();
                dataStream.Dispose();
                res.Dispose();
            }
            if (resStr == "0")
            {
                //重新登录
                Response.Redirect(CommonHelper.SsoLogin + "?returnurl=" + Server.UrlEncode(Request.Url.AbsoluteUri));
                Response.End();
                return false;
            }
            else
            {
                //登陆成功，保存session
                Session["IsLogin"] = true;
                return true;
            }
        }
    }
}

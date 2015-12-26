using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Server.Models;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class HomeController : Controller
    {

        public string Index()
        {
            return DateTime.Now.ToString();
        }


        [HttpGet]
        public ActionResult Login(string returnurl)
        {
            if (string.IsNullOrEmpty(returnurl))
            {
                return Content("请求异常！");
            }
            if (Request.Cookies[CommonHelper.LoginCookieName] != null)
            {
                using (MyDbContext db = new MyDbContext())
                {
                    var userName = DEncrypt.Decrypt(Request.Cookies[CommonHelper.LoginCookieName].Value);
                    return LoginMethod(db.User.Where(m => m.UserName == userName).First(), db, returnurl);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model, string returnurl)
        {
            using (MyDbContext db = new MyDbContext())
            {
                User item = db.User.First(m => m.UserName == model.UserName);
                if (item == null || !item.Pwd.Equals(CommonHelper.GetPwd(model.Pwd)))
                {
                    return View("LoginFail");
                }
                //成功登录
                //产生一个Token,用于验证是否为合法用户：用户名存在，没过有效期限
                return LoginMethod(model, db, returnurl);
            }

        }

        private ActionResult LoginMethod(User model, MyDbContext db, string returnurl)
        {
            Token t = new Token
            {
                ExpTime = DateTime.Now.AddMinutes(2),
                IsEnable = true,
                UserName = model.UserName
            };
            db.Token.Add(t);
            db.SaveChanges();

            string token = DEncrypt.Encrypt<Token>(t);
            //写入一个cookie,标识登陆成功
            HttpCookie c = new HttpCookie(CommonHelper.LoginCookieName, DEncrypt.Encrypt(model.UserName));
            c.Expires = DateTime.Now.AddDays(7);
            if (Request.Cookies[CommonHelper.LoginCookieName] == null)
            {
                Response.Cookies.Add(c);
            }
            else
            {
                Response.Cookies.Set(c);
            }

            string url = DEncrypt.Decrypt(returnurl);
            return Redirect(url + "?token=" + Server.UrlEncode(token));
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> YanZhengToken(string token)
        {
            try
            {
                var tokenObj = DEncrypt.Decrypt<Token>(token);
                using (MyDbContext db = new MyDbContext())
                {
                    Token tokenObj2 = null;
                    Task t1 = Task.Factory.StartNew(() =>
                    {
                        tokenObj2 = db.Token.FirstOrDefault(m => m.Id == tokenObj.Id && m.IsEnable == true);
                    });
                    await t1;
                    if (tokenObj2 == null || tokenObj2.IsEnable == false)
                    {
                        return Content("0");
                    }

                    tokenObj2.IsEnable = false;
                    db.SaveChanges();
                    if (tokenObj2.ExpTime > DateTime.Now)
                    {
                        return Content("1");
                    }
                    else
                    {
                        return Content("0");
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                throw;
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User model)
        {
            try
            {
                using (MyDbContext db = new MyDbContext())
                {
                    model.CreateTime = DateTime.Now;
                    model.Pwd = CommonHelper.GetPwd(model.Pwd);
                    db.User.Add(model);
                    db.SaveChanges();
                }
                return View("Success");
            }
            catch (Exception ex)
            {
                ViewBag.Msg = ex.Message;
                return View("Error");
            }
        }
    }
}
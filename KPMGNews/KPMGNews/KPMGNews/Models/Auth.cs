using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace KPMGNews.Models
{
    public class LoginData
    {
        private HttpContextBase httpContext;

        public string Username;
        public string Password;
        public string UserID;
        public string UserTypeID;

        public LoginData(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;

            KPMGNews.Models.Model1 db = new KPMGNews.Models.Model1();
            Username = httpContext.Session["Username"] == null ? "" : httpContext.Session["Username"].ToString();
            Password = httpContext.Session["Password"] == null ? "" : httpContext.Session["Password"].ToString();
            UserID = httpContext.Session["UserID"] == null ? "" : httpContext.Session["UserID"].ToString();
            UserTypeID = httpContext.Session["UserTypeID"] == null ? "" : httpContext.Session["UserTypeID"].ToString();

            if (Username == "")
            {
                Username = httpContext.Request.Cookies["Username"] == null ? "" : httpContext.Request.Cookies["Username"].Value.ToString();
                Password = httpContext.Request.Cookies["Password"] == null ? "" : httpContext.Request.Cookies["Password"].Value.ToString();
                UserID = httpContext.Request.Cookies["UserID"] == null ? "" : httpContext.Request.Cookies["UserID"].Value.ToString();
                UserTypeID = httpContext.Request.Cookies["UserTypeID"] == null ? "" : httpContext.Request.Cookies["UserTypeID"].Value.ToString();

                httpContext.Response.Cookies["Username"].Expires = DateTime.Now.AddDays(7);
                httpContext.Response.Cookies["Password"].Expires = DateTime.Now.AddDays(7);
                httpContext.Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(7);
                httpContext.Response.Cookies["UserTypeID"].Expires = DateTime.Now.AddDays(7);

                httpContext.Session["Username"] = Username;
                httpContext.Session["Password"] = Password;
                httpContext.Session["UserID"] = UserID;
                httpContext.Session["UserTypeID"] = UserTypeID;

            }
        }

        public void LogOut()
        {
            httpContext.Session.Clear();
            httpContext.Request.Cookies.Clear();
        }

        public bool IsLoggedIn()
        {

            KPMGNews.Models.Model1 db = new KPMGNews.Models.Model1();

            int u = (from users in db.Users
                     where users.Login.ToString() == Username.ToString() && (users.Password.ToString() == Password.ToString() || users.Password.ToString() == Password)
                     select users).Count();

            if (u > 0)
            {
                return true;
            }
            else return false;
        }

        public string loginname()
        {
            KPMGNews.Models.Model1 db = new KPMGNews.Models.Model1();
            string uname = (from users in db.Users
                            where users.Login.ToString() == Username.ToString() && (users.Password.ToString() == Password.ToString() || users.Password.ToString() == Password)
                            select users).First().Login.ToString();
            return uname;
        }

        public string usertype()
        {
            KPMGNews.Models.Model1 db = new KPMGNews.Models.Model1();
            string utype = (from users in db.Users
                            where users.Login.ToString() == Username.ToString() && (users.Password.ToString() == Password.ToString() || users.Password.ToString() == Password)
                            select users).First().UserTypeID.ToString();

            if (utype.Equals("0"))
            {
                return "Publisher";
            }
            else return "Employee";
        }

    }

    //Login Check
    public class SessionAuthorizeAttribute : AuthorizeAttribute
    {

        //Login Check
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            KPMGNews.Models.Model1 db = new KPMGNews.Models.Model1();
            string Username = httpContext.Session["Username"] == null ? "" : httpContext.Session["Username"].ToString();
            string Password = httpContext.Session["Password"] == null ? "" : httpContext.Session["Password"].ToString();
            string UserID = httpContext.Session["UserID"] == null ? "" : httpContext.Session["UserID"].ToString();
            string UserTypeID = httpContext.Session["UserTypeID"] == null ? "" : httpContext.Session["UserTypeID"].ToString();

            if (Username == "")
            {
                Username = httpContext.Request.Cookies["Username"] == null ? "" : httpContext.Request.Cookies["Username"].Value.ToString();
                Password = httpContext.Request.Cookies["Password"] == null ? "" : httpContext.Request.Cookies["Password"].Value.ToString();
                UserID = httpContext.Request.Cookies["UserID"] == null ? "" : httpContext.Request.Cookies["UserID"].Value.ToString();
                UserTypeID = httpContext.Request.Cookies["UserTypeID"] == null ? "" : httpContext.Request.Cookies["UserTypeID"].Value.ToString();

                httpContext.Response.Cookies["Username"].Expires = DateTime.Now.AddDays(7);
                httpContext.Response.Cookies["Password"].Expires = DateTime.Now.AddDays(7);
                httpContext.Response.Cookies["UserID"].Expires = DateTime.Now.AddDays(7);
                httpContext.Response.Cookies["UserTypeID"].Expires = DateTime.Now.AddDays(7);

                httpContext.Session["Username"] = Username;
                httpContext.Session["Password"] = Password;
                httpContext.Session["UserID"] = UserID;
                httpContext.Session["UserTypeID"] = UserTypeID;

            }



            

            int u = (from users in db.Users
                     where users.Login.ToString() == Username.ToString() && (users.Password.ToString() == Password.ToString() || users.Password.ToString() == Password)
                     select users).Count();



            if (u == 0 )
            {
                httpContext.Session.Clear();
                httpContext.Request.Cookies.Clear();
                return false;
            }
            else
            {
                User usr = (from users in db.Users
                            where users.Login.ToString() == Username.ToString() && (users.Password.ToString() == Password.ToString() || users.Password.ToString() == Password)
                            select users).First();

                httpContext.Session["UserTypeID"] = usr.UserTypeID.ToString();
                httpContext.Session["UserID"] = usr.UserID.ToString();
                return true;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var url = filterContext.HttpContext.Request.RawUrl;
            filterContext.Result = new RedirectResult("~/Accounts/Login?message=Session expired or Incorrect Login, Please login again.", false);
        }
    }

}
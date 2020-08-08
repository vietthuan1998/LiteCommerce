using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            ViewBag.Title = "Change Password";
            ViewBag.Mesage = "Change Your Password";
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string email = "", string oldPassword = "", string newPassword = "", string confirmPassword = "")
        {
            if (string.IsNullOrEmpty(oldPassword))
                ModelState.AddModelError("oldpassword", "Old Password expected!");
            if (string.IsNullOrEmpty(newPassword))
                ModelState.AddModelError("newpassword", "New Password expected!");
            if (string.IsNullOrEmpty(confirmPassword))
                ModelState.AddModelError("confirmpassword", "Confirm Password expected!");
            if(UserAccountBLL.Authorize(email, Helper.EncodeMD5(oldPassword)
                                        ,UserAccountTypes.Employee) == null)
                ModelState.AddModelError("authorize", "Old Password does not match!");
            if (oldPassword == newPassword)
                ModelState.AddModelError("different", "Old Password must be different New Password!");
            if(newPassword != confirmPassword)
                ModelState.AddModelError("match", "New Password can't be different Confirm Password!");
            if (!ModelState.IsValid)
            {
                return View();
            }
            newPassword = Helper.EncodeMD5(newPassword);
            UserAccountBLL.ChangePassword(email, newPassword, UserAccountTypes.Employee);
            return RedirectToAction("Index", "Dashboard");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {
            Session.Abandon();
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("SignIn");
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Dashboard");
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(string email = "", string password = "")
        {
            //TODO: Kiểm tra tài khoản thông qua cơ sở dữ liệu
            password = Helper.EncodeMD5(password);
           
            UserAccount user = UserAccountBLL.Authorize(email, password, UserAccountTypes.Employee);
            if (user != null)
            {
                WebUserData userData = new WebUserData() 
                {
                    UserID = user.UserID,
                    FullName = user.FullName,
                    GroupName = user.GroupName,
                    LoginTime = DateTime.Now,
                    SessionID = Session.SessionID,
                    ClientIP =Request.UserHostAddress,
                    Photo = user.Photo,
                    Title = user.Title
                    
                };

                ModelState.AddModelError("test", user.FullName);
                System.Web.Security.FormsAuthentication.SetAuthCookie(userData.ToCookieString(), false);
                //return View();
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("LoginError", "Login Fail!");
                ViewBag.Email = email;
                return View();
            }

            //if(email == "vietthuan@gmail.com" && password == "123123")
            //{
            //    //ghi nhận phiên đăng nhập của tài khoản
            //    System.Web.Security.FormsAuthentication.SetAuthCookie(email, false);
            //    //chuyển trang về dashboard
            //    return RedirectToAction("Index", "Dashboard");
            //}
            //else
            //{
            //    ModelState.AddModelError("LoginError", "Login Fail!");
            //    ViewBag.Email = email;
            //    return View();
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgotPassword()
        {

            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPassword(string userName = "") 
        {
            if (string.IsNullOrEmpty(userName) || 
                UserAccountBLL.FindUserName(userName, UserAccountTypes.Employee) != false)
                ModelState.AddModelError("email", "Email expected");
            if (!ModelState.IsValid)
                return View();
            string pass = Helper.EncodeMD5("123456");
            UserAccountBLL.ResetPassword(userName, pass, UserAccountTypes.Employee);

            return Redirect("SignIn");
        }
    }
}
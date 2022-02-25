using DigitalAzariBDC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace DigitalAzariBDC.Controllers
{
    public class AccountController : Controller
    {
       
            DigitalAzariEntities dbEntities = new DigitalAzariEntities();
            public ActionResult Login()
            {
                Response.Cookies["_UserID"].Expires = DateTime.Now;
                Response.Cookies["_UserType"].Expires = DateTime.Now;
                Response.Cookies["_UserEmail"].Expires = DateTime.Now;
                Response.Cookies["_UserName"].Expires = DateTime.Now;
                return View();
            }

            [HttpPost]
            public ActionResult Login(string email, string password)
            {
                try
                {
                    string pass = "";
                    if (password != null)
                    {
                        byte[] EncDataBtye = new byte[password.Length];
                        EncDataBtye = System.Text.Encoding.UTF8.GetBytes(password);
                        pass = Convert.ToBase64String(EncDataBtye);
                    }
                    tblUser User = dbEntities.tblUsers.Where(x => x.UserEmail == email && x.Password == pass).FirstOrDefault();
                    if (User != null)
                    {
                        if (User.IsActive == true)
                        {
                            HttpCookie UserID = Request.Cookies["_UserID"];
                            if (UserID == null)
                            {
                                UserID = new HttpCookie("_UserID");
                                UserID.Value = Convert.ToString(User.UserID);
                                UserID.Expires = DateTime.Now.AddDays(1);
                            }
                            else
                            {
                                UserID.Expires = DateTime.Now.AddDays(1);
                            }
                            Response.Cookies.Add(UserID);
                            HttpCookie UserType = Request.Cookies["_UserType"];
                            if (UserType == null)
                            {
                                UserType = new HttpCookie("_UserType");
                                UserType.Value = Convert.ToString(User.RoleType);
                                UserType.Expires = DateTime.Now.AddDays(1);
                            }
                            else
                            {
                                UserType.Expires = DateTime.Now.AddDays(1);
                            }
                            Response.Cookies.Add(UserType);
                            HttpCookie UserEmail = Request.Cookies["_UserEmail"];
                            if (UserEmail == null)
                            {
                                UserEmail = new HttpCookie("_UserEmail");
                                UserEmail.Value = Convert.ToString(User.UserEmail);
                                UserEmail.Expires = DateTime.Now.AddDays(1);
                            }
                            else
                            {
                                UserEmail.Expires = DateTime.Now.AddDays(1);
                            }
                            Response.Cookies.Add(UserEmail);
                            HttpCookie UserName = Request.Cookies["_UserName"];
                            if (UserName == null)
                            {
                                UserName = new HttpCookie("_UserName");
                                UserName.Value = Convert.ToString(User.FirstName + " " + User.LastName);
                                UserName.Expires = DateTime.Now.AddDays(1);
                            }
                            else
                            {
                                UserName.Expires = DateTime.Now.AddDays(1);
                            }
                            Response.Cookies.Add(UserName);
                            if (User.RoleType == "Admin")
                            {
                                return RedirectToAction("Index", "Home");
                            }

                            if (User.RoleType == "Agent")
                            {
                                return RedirectToAction("Index", "Home", new { id = User.UserID });
                            }
                        }
                        else if (User.IsActive == false)
                        {
                            ViewBag.IsError = "Your account is deactivated please contact to Admin.";

                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.IsError = "Your credentials are incorrect.";
                        return View();
                    }
                }
                catch (Exception)
                {
                    ViewBag.IsError = "Oops there is something wrong.";
                    return View();
                }
                return View();
            }




            public ActionResult ResetPassword(string ActivationCode)
            {
                try
                {
                    string activationCode = !string.IsNullOrEmpty(Request.QueryString["ActivationCode"]) ? Request.QueryString["ActivationCode"] : Guid.Empty.ToString();
                    byte[] b = Convert.FromBase64String(activationCode);
                    string decrypted = System.Text.ASCIIEncoding.ASCII.GetString(b);

                    ViewBag.Email = decrypted;
                }
                catch (Exception ex)
                {

                    ViewBag.Error = ex.Message;
                    Console.WriteLine("Error" + ex.Message);
                }
                return View();
            }
            [HttpPost]
            public ActionResult ResetPassword(string NewPassword, string ConfirmPassword, string Email)
            {
                string pass = null;
                try
                {
                    byte[] EncDataBtye = null;
                    tblUser Data = new tblUser();
                    Data = dbEntities.tblUsers.Select(r => r).Where(x => x.UserEmail == Email).FirstOrDefault();
                    if (Data != null)
                    {

                        if (NewPassword == ConfirmPassword)
                        {
                            EncDataBtye = new byte[NewPassword.Length];
                            EncDataBtye = System.Text.Encoding.UTF8.GetBytes(NewPassword);
                            pass = Convert.ToBase64String(EncDataBtye);
                        }
                        else
                        {
                            ViewBag.PError = "New Password and Confirm Password not Matched!!!";
                            return View();
                        }

                        Data.Password = pass;
                        dbEntities.Entry(Data);
                        dbEntities.SaveChanges();
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        ViewBag.Error = "Old password is not Correct!!!";
                        return View();
                    }


                }
                catch (Exception ex)
                {

                    ViewBag.Error = ex.Message;
                    Console.WriteLine("Error" + ex.Message);
                }

                return View();
            }

            public ActionResult ChangePassword()
            {
                ViewBag.IsSuccess = null;
                ViewBag.IsError = null;
                return View();
            }

            [HttpPost]
            public ActionResult ChangePassword(string oldPassword, string newPassword)
            {
                try
                {
                    string Email = "";
                    HttpCookie cookie = Request.Cookies["_UserEmail"];
                    if (cookie != null)
                    {
                        Email = Convert.ToString(cookie.Value);

                    }

                    tblUser User = dbEntities.tblUsers.Where(x => x.Password == oldPassword && x.UserEmail == Email).FirstOrDefault();
                    if (User == null)
                    {
                        ViewBag.IsError = "Your old Password is incorrect.";
                        return View();
                    }
                    else
                    {
                        User.Password = newPassword;
                        dbEntities.Entry(User).State = EntityState.Modified;
                        dbEntities.SaveChanges();
                        ViewBag.IsSuccess = "Password change successfully.";
                        return View();
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
            public ActionResult ForgetPassword()
            {
                return View();
            }

            [HttpPost]
            public ActionResult ForgetPassword(string email)
            {
                try
                {
                    tblUser User = dbEntities.tblUsers.Where(x => x.UserEmail == email).FirstOrDefault();
                    if (User != null)
                    {
                        if (User.IsActive == true)
                        {
                            SendForgetPasswordEmail(User.UserID, User.UserEmail, User.FirstName + " " + User.LastName);
                            ViewBag.isSuccess = "Password change link is sent to your email.";
                            return View();
                        }
                        else
                        {
                            ViewBag.IsError = "Your sccount is suspended contact to Admin.";
                            return View();
                        }
                    }
                    else
                    {
                        ViewBag.IsError = "Your eneterd email is not registered.";
                        return View();
                    }
                }
                catch (Exception)
                {

                    throw;
                }



            }

            private void SendForgetPasswordEmail(int userId, string email, string name)
            {
                try
                {
                    tblSetting sa = dbEntities.tblSettings.Find(1);
                    byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(email);
                    string encrypted = Convert.ToBase64String(b);

                    using (MailMessage mm = new MailMessage(sa.Email, email))
                    {
                        string link = Request.Url.ToString();
                        link = link.Replace("ForgetPassword", "ResetPassword");
                        mm.Subject = "Password reset";
                        string body1 = "";
                        body1 += "Please click on button to reset your password.";
                        body1 += "<br /> <button style='padding: 10px 28px 11px 28px;color: #fff;background:#5F3A81;'><a style='color:white !important' href = '" + link + "?ActivationCode=" + encrypted + "'>Reset Password</a></button>";
                        body1 += "<br /><br />Yours,<br />The Bdc Power Team";

                        string body = "";
                        body += "<body  style='background-color:white !important'>";
                        body += " <div>";
                        //body += "<h3>Hello " + sa.ReceiveName + ",</h3>";
                        body += " <table style='background-color: #f2f3f8; max-width:670px;' width='100%' border='0'  cellpadding='0' cellspacing='0'>";
                        body += " <tbody> <tr style='background-color:#5F3A81;'><td style='padding: 0 35px; background-color:#5F3A81;'><a><h1 style ='color:white' > BDC POWER </h1>   </a></td> </tr>";
                        body += "<tr style='color:#455056; font-size:15px;line-height:35px;text-align: center;'><td style='padding:6px;text-align: center;'> <b>Hello " + name + ",</b></td></tr><tr style='color:#455056; font-size:15px;line-height:35px;'><td style='padding:6px;text-align: center;'>" + body1 + "</td></tr>";
                        body += "  </tbody></table>";
                        body += "</body>";

                        mm.Body = body;
                        mm.IsBodyHtml = true;
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = sa.Smtp;
                        smtp.EnableSsl = Convert.ToBoolean(sa.IsActive);
                        NetworkCredential NetworkCred = new NetworkCredential(sa.Email, sa.Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = Convert.ToInt32(sa.Port);
                        smtp.Send(mm);
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
    }

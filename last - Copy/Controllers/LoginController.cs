﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using last.Models;
using System.Web.Http.Results;

namespace last.Controllers
{
    //[RoutePrefix("Api/login")]

    public class LoginController : ApiController
    {
        NGOEntities db = new NGOEntities();
       
        //For user login   
        [Route("Api/Login/userLogin")]
        [HttpPost]
        public Response userLogin(last.Models.login login)
        {
            var log = db.User.Where(x => x.UserName.Equals(login.UserName) && x.Password.Equals(login.Password)).FirstOrDefault();
            if (log == null)
            {
         
                return new Response { Status = "Invalid", Message = "Invalid User or passwor." };
               
            }
            else
            {
                var session = HttpContext.Current.Session;
                if (session != null)
                {
                    if (session["UserName"] == null)
                    {
                        session["UserName"] = login.UserName;
                    }
                }
                var x = session["UserName"].ToString();
                return new Response { Status = "Success", Message = "Login Successfully" };
            }


             

        }

        //For new user Registration  
        [Route("Api/Login/createcontact")]
        [HttpPost]
        public object createcontact(Registration lvv)
        {
            try
            {
                
                User user = new User();
                if (user.Id == 0)
                {
                    user.UserName = lvv.UserName;
                    user.Password = lvv.Password;
                    user.Email = lvv.Email;
                    user.PhoneNumber = lvv.PhoneNumber;
                    db.User.Add(user);
                    db.SaveChanges();
                    return new Response
                    { Status = "Success", Message = "SuccessFully Saved." };
                }
            }
            catch (Exception)
            {

                throw;
            }
            return new Response
            { Status = "Error", Message = "Invalid Data." };
        }
       
        [Route("Api/Login/Validuser")]
        [HttpGet]

        public string Validuser(string UserName)
        {
            var User = db.User.Where(w => w.UserName == UserName).FirstOrDefault();

            if (User == null)
                return "user is valid";
            else
                return "user is found please choose another one";
        }
        [Route("Api/Login/GetUser")]

        [HttpGet]

        public IEnumerable<User> GetUser()
        {
            List<User> User = null;
            using (NGOEntities entities = new NGOEntities())
            {
                User = entities.User.AsEnumerable().Select(x => new User
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Password = x.Password,
                    PhoneNumber = Convert.ToString(x.PhoneNumber)
                }).ToList();
                
                
            }
            return User;
        }

        //[Route("Api/Login/GetUserByUserName1")]

        //[HttpGet]
        //public User GetUserByUserName1(string UserName)
        //{
        //    User User = null;
        //    ///using (NGOEntities entities = new NGOEntities())
        //    {
        //        User = db.User.Where(x => x.UserName == UserName).Select(s => new User
        //        {
        //            Id = s.Id,
        //            UserName = s.UserName,
        //            Email = s.Email,
        //            PhoneNumber = Convert.ToString(s.PhoneNumber),
        //            Password =s.Password

        //        }).FirstOrDefault();
        //    }
        //    return User;


        //}

        [Route("Api/Login/GetUserByUserName")]

        [HttpGet]
        public User GetUserByUserName(string UserName)
        {
            User User = null;
            ///using (NGOEntities entities = new NGOEntities())
            {
                User = db.User.Where(x => x.UserName == UserName).FirstOrDefault();
            }
            //return Json(User, JsonRequestBehavior.AllowGet)
            //var a = User.
            //return Json(User);
            return User;
                //OK(new { result = User });


        }



        [HttpPut]
        [Route("Api/Login/UpdateUser")]
        public IHttpActionResult PutUserMaster(User UserName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.User.

            return Ok(UserName);
        }
        [HttpDelete]
        [Route("Api/Login/DeleteUser")]
        public IHttpActionResult DeleteEmaployeeDelete(string UserName)
        {
            User DeleteUser = db.User.Find(UserName);
            if (UserName == null)
            {
                return NotFound();
            }

            db.User.Remove(DeleteUser);
            db.SaveChanges();

            return Ok(UserName);
        }





























        //[HttpDelete]
        //[Route("Api/Login/DeleteUser")]
        //public IHttpActionResult Delete(string UserName)
        //{


        //    using (var ctx = new NGOEntities())
        //    {
        //        var UserDeleted = ctx.User
        //            .Where(s => s.UserName == UserName)
        //            .FirstOrDefault();

        //        ctx.Entry(UserName).State = System.Data.Entity.EntityState.Deleted;
        //        ctx.SaveChanges();
        //    }

        //    return Ok();
        //}


    }
}

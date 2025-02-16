﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using last.Models;
using NGOdata;
using AutoMapper;
using System.Web;
//using last.Areas.Administration.Models;

namespace last.Areas.Administration.Controllers
{
    public class AdminController : ApiController
    {
        private NGOdata.NGODBEntities db = new NGOdata.NGODBEntities();

        [Route("Api/Areas/Administration/Controller/AdminController/AdminLogin")]
        [HttpPost]
        public Response AdminLogin(last.Models.login login)
        {
            var log = db.Roles.Where(x => x.UserName.Equals(login.UserName) && x.Password.Equals(login.Password)).FirstOrDefault();
            if (log == null)
            {

                return new Response { Status = "Invalid", Message = "Invalid User or passwor." };

            }
            else
            {
                return new Response { Status = "Success", Message = "Login Successfully" };

            }


        }
    }

}
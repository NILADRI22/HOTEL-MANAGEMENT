using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HOTELMANAGEMENTWEBAPI.Jwt;
using HOTELMANAGEMENTWEBAPI.Models;

namespace HOTELMANAGEMENTWEBAPI.Controllers
{
    public class RegisterController : ApiController
    {
        HotelEntities3 mb = new HotelEntities3();

        [HttpGet]
        public IEnumerable<UserData> GetUsers()
        {
            return mb.UserDatas.ToList();
        }

        // GET api/<controller>/5

        [HttpGet]
        public object GetUsersById(int id)
        {
            var obj = mb.UserDatas.Where(x => x.Id == id).FirstOrDefault();
            return obj;
        }
        [HttpPost]
        public HttpResponseMessage InsertUser(UserData Reg)
        {
            try
            {
               
                if (Reg.Id == 0)
                {
                    UserData EL = new UserData();
                    EL.FullName = Reg.FullName;
                    EL.Mobile = Reg.Mobile;
                    EL.Email = Reg.Email;
                    EL.Password = Reg.Password;
                    var users = mb.UserDatas.Where(x => x.Email == Reg.Email).ToList();
                    if (users.Count > 0)
                    {
                        return new HttpResponseMessage(HttpStatusCode.Ambiguous);
                    }
                    else
                    {
                        mb.UserDatas.Add(EL);
                        mb.SaveChanges();
                        return new HttpResponseMessage(HttpStatusCode.Created);
                    }
                }

                else
                {
                    var obj = mb.UserDatas.Where(x => x.Id == Reg.Id).ToList().FirstOrDefault();
                    if (obj.Id > 0)
                    {
                        obj.FullName = Reg.FullName;
                        obj.Mobile = Reg.Mobile;
                        obj.Email = Reg.Email;
                        obj.Password = Reg.Password;
                        mb.SaveChanges();
                    }
                    return new HttpResponseMessage(HttpStatusCode.OK);
                }
            
                
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            var obj = mb.UserDatas.Where(x => x.Id == id).ToList().FirstOrDefault();
            mb.UserDatas.Remove(obj);
            mb.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Accepted);
         }

        [HttpPost]
        public HttpResponseMessage Login(string email, string password)
        {
            // System.Diagnostics.Debug.WriteLine($"Username{email}\tPassword{password}");
            try
            {

                if (CheckUser(email, password))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, JwtManager.GenerateToken(email) + ":" + email);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        public bool CheckUser(string email, string password)
        {
            // should check in the database
            var log = mb.UserDatas.Where(x => x.Email.Equals(email) && x.Password.Equals(password)).FirstOrDefault();
            if (log == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
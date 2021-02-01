using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HOTELMANAGEMENTWEBAPI.Models;
namespace HOTELMANAGEMENTWEBAPI.Controllers
{
    public class StaffController : ApiController
    {
        HotelEntities2 mb = new HotelEntities2();

        [HttpGet]
        public IEnumerable<Staff> GetStaffs()
        {
            return mb.Staffs.ToList();
        }

        [HttpGet]
        public object GetStaffsById(int id)
        {
            var obj = mb.Staffs.Where(x => x.Id == id).FirstOrDefault();
            return obj;
        }
        // POST api/<controller>

        [HttpPost]
        public HttpResponseMessage PostOrEditStaffs(Staff bs)
        {
            try
            {
                if (bs.Id == 0)
                {
                    Staff c = new Staff();
                    c.StaffId = bs.StaffId;
                    c.StaffName = bs.StaffName;
                    c.Salary = bs.Salary;
                    mb.Staffs.Add(c);
                    mb.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.Created);
                }
                else
                {
                    var obj = mb.Staffs.Where(x => x.Id == bs.Id).ToList().FirstOrDefault();
                    if (obj.Id > 0)
                    {
                        obj.StaffId = bs.StaffId;
                        obj.StaffName = bs.StaffName;
                        obj.Salary = bs.Salary;
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
        public HttpResponseMessage DeleteStaff(int id)
        {
            var obj = mb.Staffs.Where(x => x.Id == id).ToList().FirstOrDefault();
            mb.Staffs.Remove(obj);
            mb.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Accepted);

        }

    }
}
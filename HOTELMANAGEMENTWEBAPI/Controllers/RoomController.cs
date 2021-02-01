using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HOTELMANAGEMENTWEBAPI.Models;

namespace HOTELMANAGEMENTWEBAPI.Controllers
{
   
    public class RoomController : ApiController
    {
        HotelEntities mb = new HotelEntities();

        [HttpGet]
        public IEnumerable<Room> GetRooms()
        {
            return mb.Rooms.ToList();
        }

        // GET api/<controller>/5
    
        [HttpGet]
        public object GetRoomsById(int id)
        {
            var obj= mb.Rooms.Where(x=>x.RoomId==id).FirstOrDefault();
            return obj;
        }

        // POST api/<controller>

        [HttpPost]
        public HttpResponseMessage PostOrEditRooms(Room bs)
        {
            try
            {
                if (bs.RoomId == 0)
                {
                    Room c = new Room();
                    c.RoomNumber = bs.RoomNumber;
                    c.RoomTitle = bs.RoomTitle;
                    c.RoomPrice = bs.RoomPrice;
                    mb.Rooms.Add(c);
                    mb.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.Created);
                }
                else
                {
                    var obj = mb.Rooms.Where(x => x.RoomId == bs.RoomId).ToList().FirstOrDefault();
                    if (obj.RoomId > 0)
                    {
                        obj.RoomNumber = bs.RoomNumber;
                        obj.RoomTitle = bs.RoomTitle;
                        obj.RoomPrice = bs.RoomPrice;
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
        public HttpResponseMessage DeleteRoom(int id)
        {
            var obj = mb.Rooms.Where(x => x.RoomId == id).ToList().FirstOrDefault();
            mb.Rooms.Remove(obj);
            mb.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Accepted);

        }
        [HttpPost]
        public HttpResponseMessage PostRoomStatus(BookRoomStatu bs)
        {
            try
            {

                    mb.BookRoomStatus.Add(bs);
                    mb.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        public IEnumerable<BookRoomStatu> GetRoomStatus()
        {
            return mb.BookRoomStatus.ToList();
        }

        [HttpDelete]
        public HttpResponseMessage DeleteRoomStatus(int id)
        {
            var obj = mb.BookRoomStatus.Where(x => x.ID == id).ToList().FirstOrDefault();
            mb.BookRoomStatus.Remove(obj);
            mb.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Accepted);

        }
    }
}

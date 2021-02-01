using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HOTELMANAGEMENTWEBAPI.Models;

namespace HOTELMANAGEMENTWEBAPI.Controllers
{
  
    public class FoodController : ApiController
    {
        // GET api/<controller>
        HotelEntities1 mb = new HotelEntities1();
        public string ShoppingCartId { get; set; }
        [HttpGet]
        public IEnumerable<Food> GetFoods()
        {
            mb.Configuration.ProxyCreationEnabled = false;
            return mb.Foods.ToList();
        }

        [HttpGet]
        public object GetFoodsById(int id)
        {
            mb.Configuration.ProxyCreationEnabled = false;
            var obj = mb.Foods.Where(x => x.FoodId == id).FirstOrDefault();
            return obj;
        }
        // POST api/<controller>

        [HttpPost]
        public HttpResponseMessage PostOrEditFoods(Food bs)
        {
            try
            {
                if (bs.FoodId == 0)
                {
                    Food c = new Food();
                    c.Dishes = bs.Dishes;
                    c.Quantity = bs.Quantity;
                    c.Price = bs.Price;
                    mb.Foods.Add(c);
                    mb.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.Created);
                }
                else
                {
                    var obj = mb.Foods.Where(x => x.FoodId == bs.FoodId).ToList().FirstOrDefault();
                    if (obj.FoodId > 0)
                    {
                        obj.Dishes = bs.Dishes;
                        obj.Quantity = bs.Quantity;
                        obj.Price = bs.Price;
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
        public HttpResponseMessage DeleteFood(int id)
        {
            var obj = mb.Foods.Where(x => x.FoodId == id).ToList().FirstOrDefault();
            mb.Foods.Remove(obj);
            mb.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Accepted);

        }
       
        [HttpGet]
        public IEnumerable<Object> GetFoodStatus()
        {
            mb.Configuration.ProxyCreationEnabled = false;
            return (from p in mb.Foods join o in mb.FoodStatus on p.FoodId equals o.FoodID where 
                    o.Status=="Accepted"
                    select
                    new
                    {
                        o.OrderID,
                        o.FoodID,
                        p.Dishes,
                        o.Status,
                        p.Price,
                        p.Quantity
                    }).ToList();
        }
        [HttpPost]
        public HttpResponseMessage PostFoods(FoodStatu bs)
        {
            try
            {
                if (bs.OrderID == 0)
                {
                    FoodStatu c = new FoodStatu();
                    c.FoodID = bs.FoodID;
                    c.Status = "Accepted";
                    mb.FoodStatus.Add(c);
                    mb.SaveChanges();
                    return new HttpResponseMessage(HttpStatusCode.Created);
                }
                else
                {
                    var obj = mb.FoodStatus.Where(x => x.OrderID == bs.OrderID).ToList().FirstOrDefault();
                    if (obj.OrderID > 0)
                    {
                        obj.Food.Quantity = bs.Food.Quantity;
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
        public HttpResponseMessage DeleteFoodStatus(int id)
        {
            var obj = mb.FoodStatus.Where(x => x.OrderID == id).ToList().FirstOrDefault();
            mb.FoodStatus.Remove(obj);
            mb.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.Accepted);

        }
    }
}
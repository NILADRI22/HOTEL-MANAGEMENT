using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using HOTELMANAGEMENTWEBAPI.Models;

namespace HOTELMANAGEMENTWEBAPI.Controllers
{
    public class ProductController : ApiController
    {
        ImageEntities db = new ImageEntities();
        // POST api/<controller>
        [HttpPost]
        public HttpResponseMessage Post(Product p)
        {
            if (HttpContext.Current.Request.Files.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            //Read the File data from Request.Form collection.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            //Convert the File data to Byte Array.
            byte[] bytes;
            using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            {
                bytes = br.ReadBytes(postedFile.ContentLength);
            }

            //Insert the File to Database Table.
            //Product  employee = new Product();
            //Product img = new Product();
            //img.ImageID = p.ImageID;
            //   img.ImageName = Path.GetFileName(postedFile.FileName);
            //   img.ImageSrc = bytes;
            //   db.Products.Add(img);
            //   db.SaveChanges();



            Product img = new Product()
            {
                ImageName = Path.GetFileName(postedFile.FileName),
                ImageSrc = bytes,
                ImageID = p.ImageID

            };
            db.Products.Add(img);
            db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

       
    }
}
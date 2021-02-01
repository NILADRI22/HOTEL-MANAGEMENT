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
    public class ImageController : ApiController
    {
        EmployeeEntities db = new EmployeeEntities();
        [HttpPost]
        public HttpResponseMessage Post()
        {
            //Create HTTP Response.
            //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            //Check if Request contains File.
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
         
            Image image = new Image()
            {
                ImageName = Path.GetFileName(postedFile.FileName),
                Image1 = bytes,
                ImageId=100
                
            };
            db.Images.Add(image);
            db.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}

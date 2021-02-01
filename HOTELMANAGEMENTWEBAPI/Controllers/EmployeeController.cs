using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;
using HOTELMANAGEMENTWEBAPI.Models;
using System.Net.Http.Headers;
//using Image = HOTELMANAGEMENTWEBAPI.Models.Image;


namespace HOTELMANAGEMENTWEBAPI.Controllers
{
   
    public class EmployeeController : ApiController
    {
        EmployeeEntities entities = new EmployeeEntities();
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Post(Employee e)
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
            //byte[] bytes;
            //using (BinaryReader br = new BinaryReader(postedFile.InputStream))
            //{
            //    bytes = br.ReadBytes(postedFile.ContentLength);
            //}

            //Insert the File to Database Table.

            Employee emp = new Employee()
            {
                ImageName = Path.GetExtension(postedFile.FileName),
                //Image = byte,
                EmployeeName = Guid.NewGuid().ToString(),
                Occupation = postedFile.ContentType
            };
            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            var fileSavePath = Path.Combine(rootPath, emp.EmployeeName + emp.ImageName);
            postedFile.SaveAs(fileSavePath);
            entities.Employees.Add(emp);
            entities.SaveChanges();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }


        //     [System.Web.Http.HttpGet]
        //     public HttpResponseMessage GetFiles(int id)
        //     {
        //         //Create HTTP Response.
        //         HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
        //         var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
        //         //Fetch the File data from Database.
        //         //FilesEntities entities = new FilesEntities();
        //         Employee fileDTO = entities.Employees.ToList().Find(p => p.EmployeeID == id);
        //         var filepath = Path.Combine(rootPath, fileDTO.EmployeeName + fileDTO.ImageName);
        //         //Set the Response Content.
        //         byte[] file = File.ReadAllBytes(filepath);

        //MemoryStream ms = new MemoryStream(file);
        //         response.Content = new ByteArrayContent(file.Image);

        //         //Set the Response Content Length.
        //         response.Content.Headers.ContentLength = file.Image.LongLength;

        //         //Set the Content Disposition Header Value and FileName.
        //         response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //         response.Content.Headers.ContentDisposition.FileName = file.ImageName;
        //         return response;
        //     }

        [System.Web.Http.HttpGet]
        public Object Get(int id)
        {
            // Employee unique = new Employee();
            var rootPath = HttpContext.Current.Server.MapPath("~/UploadedFiles");
            //var rootPath = Path.GetFullPath("~/UploadedFiles");
            var fileDTO = entities.Employees.Find(id);
            if (fileDTO != null)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var filepath = Path.Combine(rootPath, fileDTO.EmployeeName + fileDTO.ImageName);
                byte[] file = File.ReadAllBytes(filepath);

                MemoryStream ms = new MemoryStream(file);
                //fileDTO.EmployeeName = new ByteArrayContent(file).ToString();
                response.Content = new ByteArrayContent(file);
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                // response.Content.Headers.ContentType = new MediaTypeHeaderValue(uniqueName.Occupation);
                //response.Content.Headers.ContentDisposition.FileName=
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }

        }
    }
}
    


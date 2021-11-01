using MIDS207_Project.BusinessLogic;
using MIDS207_Project.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace MIDS207_Project.WebApi.Controllers.api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StudentController : ApiController
    {
        private StudentBusinessLogic studentBusinessLogic;

        public StudentBusinessLogic StudentBusinessLogic
        {
            get
            {
                if (studentBusinessLogic == null)
                    studentBusinessLogic = new StudentBusinessLogic();

                return studentBusinessLogic;
            }
            set { studentBusinessLogic = value; }
        }

        public StudentController()
        {

        }

        [HttpGet]
        [Route("api/Student/GetSearch")]
        public JsonResult<ResponseService> GetSearch(string searchStudent)
        {
            ResponseService response = StudentBusinessLogic.GetSearch(searchStudent);

            return Json(response);
        }

        [HttpGet]
        [Route("api/Student/GetStudentById")]
        public JsonResult<ResponseService> GetStudentById(int studentId)
        {
            ResponseService response = StudentBusinessLogic.GetStudentById(studentId);

            return Json(response);
        }


        [HttpGet]
        [Route("api/Student/InactiveStudent")]
        public JsonResult<ResponseService> InactiveStudent(int studentId)
        {
            ResponseService response = StudentBusinessLogic.InactiveStudent(studentId);

            return Json(response);
        }

        [HttpPost]
        [Route("api/Student/CreateUpdateStudent")]
        public JsonResult<ResponseService> CreateUpdateStudent(StudentDto student)
        {
            student.PathServer = HttpContext.Current.Server.MapPath("/Files");
            ResponseService response = StudentBusinessLogic.CreateUpdateStudent(student);

            return Json(response);
        }

        [HttpPost]
        [Route("api/Student/UploadFile")]
        public JsonResult<ResponseService> UploadFile()
        {
            var filename = HttpContext.Current.Request.Form["fileName"];
            //var f = Request.Files
            var httpRequest = HttpContext.Current.Request;

            // Check if files are available
            if (httpRequest.Files.Count > 0)
            {
                var files = new List<string>();

                // interate the files and save on the server
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    files.Add(filePath);
                }

                // return result
                //result = Request.CreateResponse(HttpStatusCode.Created, files);
            }
            else
            {
                // return BadRequest (no file(s) available)
                //result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            //ResponseService response = StudentBusinessLogic.CreateUpdateStudent(student);

            return Json(new ResponseService());
        }

    }
}
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

        /// <summary>
        /// Regresa el listado de alumnos
        /// </summary>
        /// <param name="searchStudent"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Student/GetSearch")]
        public JsonResult<ResponseService> GetSearch(string searchStudent)
        {
            ResponseService response = StudentBusinessLogic.GetSearch(searchStudent);

            return Json(response);
        }

        /// <summary>
        /// Regresa un alumno
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Student/GetStudentById")]
        public JsonResult<ResponseService> GetStudentById(int studentId)
        {
            ResponseService response = StudentBusinessLogic.GetStudentById(studentId);

            return Json(response);
        }

        /// <summary>
        /// Desactiva un alumno
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Student/InactiveStudent")]
        public JsonResult<ResponseService> InactiveStudent(int studentId)
        {
            ResponseService response = StudentBusinessLogic.InactiveStudent(studentId);

            return Json(response);
        }

        /// <summary>
        /// Regresa un booleano e indica si se puede ingresar el RFC
        /// </summary>
        /// <param name="studenId"></param>
        /// <param name="rfc"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Student/CanUseRFC")]
        public JsonResult<ResponseService> CanUseRFC(int studenId, string rfc)
        {
            ResponseService response = StudentBusinessLogic.CanUseRFC(studenId, rfc);

            return Json(response);
        }

        /// <summary>
        /// Regresa un booleano e indica si se puede ingresar el CURP
        /// </summary>
        /// <param name="studenId"></param>
        /// <param name="curp"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Student/CanUseCURP")]
        public JsonResult<ResponseService> CanUseCURP(int studenId, string curp)
        {
            ResponseService response = StudentBusinessLogic.CanUseCURP(studenId, curp);

            return Json(response);
        }

        /// <summary>
        /// Regresa un booleano e indica si se puede ingresar el email
        /// </summary>
        /// <param name="studenId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/Student/CanUseEmail")]
        public JsonResult<ResponseService> CanUseEmail(int studenId, string email)
        {
            ResponseService response = StudentBusinessLogic.CanUseEmail(studenId, email);

            return Json(response);
        }

        /// <summary>
        /// Crea o Actualzia un alumno
        /// </summary>
        /// <param name="student">Entidad con los valores del alumnos</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Student/CreateUpdateStudent")]
        public JsonResult<ResponseService> CreateUpdateStudent(StudentDto student)
        {
            student.PathServer = HttpContext.Current.Server.MapPath("/Files");
            ResponseService response = StudentBusinessLogic.CreateUpdateStudent(student);

            return Json(response);
        }

    }

}
using MIDS207_Project.DataAccess;
using MIDS207_Project.Entities;
using MIDS207_Project.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDS207_Project.BusinessLogic
{
    public class StudentBusinessLogic
    {
        private readonly IRepositoryStudent repositoryStudent;
        public StudentBusinessLogic()
        {
            repositoryStudent = new RepositoryStudent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchStudent"></param>
        /// <returns></returns>
        public ResponseService GetSearch(string searchStudent)
        {
            ResponseService response = new ResponseService();
            try
            {
                response.Data = string.IsNullOrWhiteSpace(searchStudent) ? repositoryStudent.GetStudents() :
                    repositoryStudent.GetStudents(x => x.FirstName.ToLower().Contains(searchStudent.ToLower()) || x.MiddleName.ToLower().Contains(searchStudent.ToLower()));
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsException = true;
                response.Message = string.Format(ExceptionMessage.ErrorShowUser, DateTime.Now.ToString(), new StackTrace().GetFrame(1).GetMethod().Name, ex.Message);
                response.InnerException = string.Format(ExceptionMessage.ErrorException, DateTime.Now.ToString(), ex.StackTrace, ex.GetOriginalException().Message);
            }
            return response;
        }

        public ResponseService GetStudentById(int studentId)
        {
            ResponseService response = new ResponseService();
            try
            {
                response.Data = repositoryStudent.GetStudent(x => x.StudentID == studentId);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsException = true;
                response.Message = string.Format(ExceptionMessage.ErrorShowUser, DateTime.Now.ToString(), new StackTrace().GetFrame(1).GetMethod().Name, ex.Message);
                response.InnerException = string.Format(ExceptionMessage.ErrorException, DateTime.Now.ToString(), ex.StackTrace, ex.GetOriginalException().Message);
            }
            return response;
        }

        public ResponseService InactiveStudent(int studentId)
        {
            ResponseService response = new ResponseService();
            try
            {
                response.Data = repositoryStudent.InactiveStudent(studentId);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsException = true;
                response.Message = string.Format(ExceptionMessage.ErrorShowUser, DateTime.Now.ToString(), new StackTrace().GetFrame(1).GetMethod().Name, ex.Message);
                response.InnerException = string.Format(ExceptionMessage.ErrorException, DateTime.Now.ToString(), ex.StackTrace, ex.GetOriginalException().Message);
            }
            return response;
        }

        public ResponseService CreateUpdateStudent(StudentDto studentDto)
        {
            ResponseService response = new ResponseService();
            try
            {
                bool isCreateUpdate = repositoryStudent.CreateUpdateStudent(studentDto);


                string pathFolderFiles = Path.Combine(studentDto.PathServer, studentDto.StudentID.ToString());


                if (Directory.Exists(pathFolderFiles))
                {
                    repositoryStudent.DeleteFilesByStudentID(studentDto.StudentID);
                    Array.ForEach(Directory.GetFiles(pathFolderFiles), File.Delete);
                }
                else
                {
                    Directory.CreateDirectory(pathFolderFiles);
                }

                foreach (var item in studentDto.Files)
                {
                    byte[] objFileBytes = DataURI.GetFile(item.FileDataURI);
                    string path = System.IO.Path.Combine(pathFolderFiles, item.FileName);

                    StudentFile studentFile = new StudentFile()
                    {
                        StudentID = studentDto.StudentID,
                        Name = item.FileName,
                        PathUrl = path.Trim(),
                        FileExtension = DataURI.GetMimeType(item.FileDataURI),
                        CreatedDate = DateTime.Now
                    };
                    repositoryStudent.CreateStudentFiles(studentFile);

                    File.WriteAllBytes(path, objFileBytes);

                }

                response.Data = isCreateUpdate;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsException = true;
                response.Message = string.Format(ExceptionMessage.ErrorShowUser, DateTime.Now.ToString(), new StackTrace().GetFrame(1).GetMethod().Name, ex.Message);
                response.InnerException = string.Format(ExceptionMessage.ErrorException, DateTime.Now.ToString(), ex.StackTrace, ex.GetOriginalException().Message);
            }
            return response;
        }

        public ResponseService CanUseRFC(int studentId, string rfc)
        {
            ResponseService response = new ResponseService();
            try
            {
                if (string.IsNullOrEmpty(rfc))
                    response.Data = false;
                else
                {
                    rfc = rfc?.ToUpper().Trim();
                    response.Data = repositoryStudent.CanUseRFC(studentId, rfc);
                }
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsException = true;
                response.Message = string.Format(ExceptionMessage.ErrorShowUser, DateTime.Now.ToString(), new StackTrace().GetFrame(1).GetMethod().Name, ex.Message);
                response.InnerException = string.Format(ExceptionMessage.ErrorException, DateTime.Now.ToString(), ex.StackTrace, ex.GetOriginalException().Message);
            }
            return response;
        }

        public ResponseService CanUseCURP(int studentId, string curp)
        {
            ResponseService response = new ResponseService();
            try
            {
                if (string.IsNullOrEmpty(curp))
                    response.Data = false;
                else
                {
                    curp = curp?.ToUpper().Trim();
                    response.Data = repositoryStudent.CanUseCURP(studentId, curp);
                }
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsException = true;
                response.Message = string.Format(ExceptionMessage.ErrorShowUser, DateTime.Now.ToString(), new StackTrace().GetFrame(1).GetMethod().Name, ex.Message);
                response.InnerException = string.Format(ExceptionMessage.ErrorException, DateTime.Now.ToString(), ex.StackTrace, ex.GetOriginalException().Message);
            }
            return response;
        }

        public ResponseService CanUseEmail(int studentId, string email)
        {
            ResponseService response = new ResponseService();
            try
            {
                if (string.IsNullOrEmpty(email))
                    response.Data = false;
                else
                {
                    email = email?.ToUpper().Trim();
                    response.Data = repositoryStudent.CanUseEmail(studentId, email);
                }
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsException = true;
                response.Message = string.Format(ExceptionMessage.ErrorShowUser, DateTime.Now.ToString(), new StackTrace().GetFrame(1).GetMethod().Name, ex.Message);
                response.InnerException = string.Format(ExceptionMessage.ErrorException, DateTime.Now.ToString(), ex.StackTrace, ex.GetOriginalException().Message);
            }
            return response;
        }
    }
}

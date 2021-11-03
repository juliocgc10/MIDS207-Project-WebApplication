using MIDS207_Project.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MIDS207_Project.DataAccess
{
    public class RepositoryStudent : IRepositoryStudent
    {
        public StudentDto GetStudent(Expression<Func<Student, bool>> predicate = null)
        {
            StudentDto studentResult = new StudentDto();
            Student student = null;
            using (cpdsEntities context = new cpdsEntities())
            {
                if (predicate != null)
                {
                    student = context.Students.FirstOrDefault(predicate);
                }

                studentResult = ConvertToEntitiesStudent(student);
            }

            return studentResult;
        }

        public List<StudentDto> GetStudents(Expression<Func<Student, bool>> predicate = null)
        {

            List<StudentDto> studentsResult = new List<StudentDto>();
            IEnumerable<Student> students;
            using (cpdsEntities contex = new cpdsEntities())
            {
                if (predicate != null)
                {
                    students = contex.Students.Where(predicate);
                }
                else
                    students = contex.Students;

                students.ToList().ForEach(student =>
                {
                    studentsResult.Add(ConvertToEntitiesStudent(student));
                }
                );
            }

            return studentsResult.OrderBy(x => x.StudentID).ToList();

        }

        public bool CreateUpdateStudent(StudentDto studentDto)
        {
            bool isCreateUpdate = false;
            Student student = null;
            using (cpdsEntities context = new cpdsEntities())
            {
                if (studentDto == null)
                    throw new Exception("No se ingresaron loa avlores del alumno, verifique");

                if (studentDto.StudentID != 0)
                {

                    student = context.Students.FirstOrDefault(x => x.StudentID == studentDto.StudentID);

                    if (student != null)
                    {
                        StudenToParseStudent(studentDto, student);

                        context.Students.Attach(student);
                        context.Entry(student).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        //studentDto = ConvertToEntitiesStudent(student);

                        studentDto.StudentID = student.StudentID;
                        isCreateUpdate = true; ;
                    }
                    else
                        throw new Exception("No existe el alumno, verifique que sea un alumno valido");
                }
                else
                {
                    student = new Student();
                    StudenToParseStudent(studentDto, student);
                    context.Students.Add(student);
                    context.SaveChanges();

                    studentDto.StudentID = student.StudentID;
                }
            }

            return isCreateUpdate;
        }

        public bool CreateStudentFiles(StudentFile studentFile)
        {
            if (studentFile == null)
                throw new Exception("No se ingresaron loS valores del archivo del alumno, verifique");

            bool isCreateStudenFiles = false;
            using (cpdsEntities context = new cpdsEntities())
            {
                context.StudentFiles.Add(studentFile);
                context.SaveChanges();
                isCreateStudenFiles = true;
            }

            return isCreateStudenFiles;
        }

        public bool DeleteFilesByStudentID(int studentId)
        {

            bool isdeleteFiles = false;
            using (cpdsEntities context = new cpdsEntities())
            {
                var studentFiles = context.StudentFiles.Where(x => x.StudentID == studentId);

                if (studentFiles.Count() > 0)
                {
                    foreach (var item in studentFiles)
                    {
                        context.StudentFiles.Remove(item);
                    }
                    context.SaveChanges();
                }

                isdeleteFiles = true;
            }

            return isdeleteFiles;
        }

        private static void StudenToParseStudent(StudentDto studentDto, Student student)
        {
            student.FirstName = studentDto.FirstName;
            student.MiddleName = studentDto.MiddleName;
            student.LastName = studentDto.LastName;
            student.BirthDate = studentDto.BirthDate;
            student.RFC = studentDto.RFC;
            student.CURP = studentDto.CURP;
            student.Password = studentDto.Password;
            student.Gender = studentDto.Gender;
            student.IsActive = studentDto.IsActive;
            student.PhoneNumber = studentDto.PhoneNumber;
            student.PostalCode = studentDto.PostalCode;
            student.State = studentDto.State;
            student.Email = studentDto.Email;

            if (studentDto.StudentID == 0)
            {
                student.CreatedDate = DateTime.Now;
                student.IsActive = true;
            }

        }

        public StudentDto InactiveStudent(int studentId)
        {
            StudentDto studentDto = null;

            using (cpdsEntities context = new cpdsEntities())
            {
                var studentUpdate = context.Students.FirstOrDefault(x => x.StudentID == studentId);

                if (studentUpdate != null)
                {

                    studentUpdate.IsActive = false;

                    context.Students.Attach(studentUpdate);
                    context.Entry(studentUpdate).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                    studentDto = ConvertToEntitiesStudent(studentUpdate);
                }
                else
                    throw new Exception("No existe el alumno, verifique que sea un alumno valido");
            }

            return studentDto;
        }

        private StudentDto ConvertToEntitiesStudent(Student student)
        {
            var studentoDto = new StudentDto()
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                BirthDate = student.BirthDate,
                RFC = student.RFC,
                CURP = student.CURP,
                Password = student.Password,
                Gender = student.Gender,
                IsActive = student.IsActive,
                PhoneNumber = student.PhoneNumber,
                PostalCode = student.PostalCode,
                State = student.State,
                Email = student.Email,
                Files = new List<FileData>()
            };

            foreach (var item in student.StudentFiles)
            {
                studentoDto.Files.Add(new FileData()
                {
                    FileName = item.Name,
                    Path = item.PathUrl
                });
            }

            return studentoDto;

        }

        public bool CanUseRFC(int studentId, string rfc)
        {
            using (cpdsEntities context = new cpdsEntities())
            {
                Student student = context.Students.FirstOrDefault(x => x.StudentID == studentId);

                if (student == null) return !context.Students.Any(x => x.RFC.ToUpper().Trim() == rfc);

                return student.RFC.ToUpper().Trim().Equals(rfc) || !context.Students.Any(x => x.RFC.ToUpper().Trim() == rfc);
            }
        }

        public bool CanUseCURP(int studentId, string curp)
        {
            using (cpdsEntities context = new cpdsEntities())
            {
                Student student = context.Students.FirstOrDefault(x => x.StudentID == studentId);

                if (student == null) return !context.Students.Any(x => x.CURP.ToUpper().Trim() == curp);

                return student.CURP.ToUpper().Trim().Equals(curp) || !context.Students.Any(x => x.CURP.ToUpper().Trim() == curp);
            }
        }

        public bool CanUseEmail(int studentId, string email)
        {
            using (cpdsEntities context = new cpdsEntities())
            {
                Student student = context.Students.FirstOrDefault(x => x.StudentID == studentId);

                if (student == null) return !context.Students.Any(x => x.Email.ToUpper().Trim() == email);

                return student.Email.ToUpper().Trim().Equals(email) || !context.Students.Any(x => x.Email.ToUpper().Trim() == email);
            }
        }
    }
}

using MIDS207_Project.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MIDS207_Project.DataAccess
{
    public interface IRepositoryStudent
    {
        List<StudentDto> GetStudents(Expression<Func<Student, bool>> predicate = null);

        StudentDto GetStudent(Expression<Func<Student, bool>> predicate = null);

        bool CreateUpdateStudent(StudentDto studentDto);

        bool CreateStudentFiles(StudentFile studentFile);

        bool DeleteFilesByStudentID(int studentId);

        StudentDto InactiveStudent(int studentId);

        bool CanUseRFC(int studentId, string rfc);

        bool CanUseCURP(int studentId, string curp);

        bool CanUseEmail(int studentId, string email);


    }
}

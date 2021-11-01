using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MIDS207_Project.Entities
{
    public class StudentDto
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public List<FileData> Files { get; set; }

        //public string File1DataURI { get; set; }
        //public string File1Name { get; set; }

        //public string File2DataURI { get; set; }
        //public string File2Name { get; set; }

        //public string File3DataURI { get; set; }
        //public string File3Name { get; set; }

        public string PathServer { get; set; }

        //public HttpPostedFileBase File1 { get; set; }
        //public HttpPostedFileBase File2 { get; set; }
    }

    public class FileData
    {
        public string FileDataURI { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}

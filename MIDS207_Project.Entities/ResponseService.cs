using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIDS207_Project.Entities
{
    public class ResponseService
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public bool IsException { get; set; }
        public string InnerException { get; set; }
    }
}

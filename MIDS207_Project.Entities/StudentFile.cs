//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MIDS207_Project.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentFile
    {
        public int FileID { get; set; }
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public string PathUrl { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual Student Student { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClassLibrary1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int StudentID { get; set; }
        public int ScheduledClassID { get; set; }
        public System.DateTime EnrollmentDate { get; set; }
    
        public virtual ScheduledClass ScheduledClass { get; set; }
        public virtual Student Student { get; set; }
    }
}

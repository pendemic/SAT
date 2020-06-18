using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1
{
    
        #region course
        public class CourseMetadata
        {
           
            [Required(ErrorMessage = "This field is required.")]
            public string CourseName { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            public string CourseDescription { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            [Range(0, double.MaxValue, ErrorMessage = "**Must be greater than Zero-0")]
            public byte CreditHours { get; set; }

            [StringLength(250,ErrorMessage ="250 Characters Max")]
            public string Curriculum { get; set; }

            [UIHint("MultilineText")]
            public string Notes { get; set; }

            [Required(ErrorMessage = "This field is required.")]
            [Display(Name = "Active / Inactive")]
            public bool IsActive { get; set; }

        }

        [MetadataType(typeof(CourseMetadata))]
        public partial class Course
        {

        }




        #endregion

        #region Enrollment
        public class EnrollmentMetadata
        {
            [Required(ErrorMessage = "This field is required")]
            public int StudentID { get; set; }

            [Required(ErrorMessage = "Valid Date required")]
            public int ScheduledClassID { get; set; }

            [Required(ErrorMessage = "Valid Date required")]
            public System.DateTime EnrollmentDate { get; set; }
            

        }

        [MetadataType(typeof(EnrollmentMetadata))]
        public partial class Enrollment

        {

        }
        #endregion

        #region ScheduledClass Metadata
        public class ScheduledClassMetadata
        {
            [Required(ErrorMessage = "This field is Required")]
            public int CourseID { get; set; }

            [Required(ErrorMessage = "This field is Required")]
            public System.DateTime StartDate { get; set; }

            [Required(ErrorMessage = "This field is Required")]
            public System.DateTime EndDate { get; set; }

            [Required(ErrorMessage = "This field is Required")]
            [StringLength(40,ErrorMessage ="Max 40 Characters")]
            public string InstructorName { get; set; }

            [StringLength(20, ErrorMessage = "Max 20 Characters")]
            [Required(ErrorMessage = "This field is Required")]
            public string Location { get; set; }

            [Display(Name = "Student Course Status ID")]
            [Required(ErrorMessage = "This field is Required")]
            public int SCSID { get; set; }

        }

        [MetadataType(typeof(ScheduledClassMetadata))]
        public partial class ScheduledClass

        {


        }
        #endregion

        #region ScheduledClassStatus

        public class ScheduledClassStatusMetadata
        {
           [Display(Name = "Student Class Status")]
           [StringLength(50, ErrorMessage = "Max 50 Characters")]
           [Required(ErrorMessage = "Student Class Status Required")]
            public string SCSName { get; set; }

        }

        [MetadataType(typeof(ScheduledClassStatusMetadata))]
        public partial class ScheduledClassStatus
        {


        }
        #endregion

        #region Student


        public class StudentMetadata
        {
            
            [Required(ErrorMessage = "Please enter a first name")]
            [StringLength(50, ErrorMessage = "Max 50 Characters")]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [StringLength(50, ErrorMessage = "Max 50 Characters")]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [StringLength(75, ErrorMessage = "Max 75 Characters")]
            [Display(Name = "Street Address")]
            public string StreetAddress { get; set; }

            [StringLength(50, ErrorMessage = "Max 50 Characters")]
            public string City { get; set; }

            [StringLength(2, ErrorMessage = "Max 2 Characters")]
            public string State { get; set; }

            [StringLength(10, ErrorMessage = "Max 10 Characters")]
            public string ZipCode { get; set; }

            [StringLength(25, ErrorMessage = "Max 25 Characters")]
            public string Major { get; set; }

            [StringLength(13, ErrorMessage = "Max 13 Characters")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Please enter an e-mail")]
            [DataType(DataType.EmailAddress,ErrorMessage = "Enter a valid e-mail")]
            [StringLength(50, ErrorMessage = "Max 50 Characters")]
            public string Email { get; set; }

            [StringLength(100, ErrorMessage = "Max 100 Characters")]
            public string PhotoUrl { get; set; }

            public int SSID { get; set; }

            
        }

        [MetadataType(typeof(StudentMetadata))]
        public partial class Student
        {

            [Display(Name = "Full Name")]
            public string FullName { get { return FirstName + " " + LastName; } }


        }


        #endregion

        #region StudentStatus

        public class StudentStatusMetadata
        {
            [Display(Name = "Student Status")]
            [Required(ErrorMessage = "Student Status Name Required")]
            [StringLength(30, ErrorMessage = "Max 30 Characters")]
            public string SSName { get; set; }

            [Display(Name = "Student Status Description")]
            [StringLength(250, ErrorMessage = "Max 250 Characters")]
            public string SSDescription { get; set; }

        }

        [MetadataType(typeof(StudentStatusMetadata))]
        public partial class StudentStatus
        {


        }

        #endregion


    
}

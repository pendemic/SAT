using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAT.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "***")]
        public string Name { get; set; }

        [Required(ErrorMessage = "***")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Subject { get; set; }

        [Required(ErrorMessage = "**Message Required**")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }//end class
}//end namespace
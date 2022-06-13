using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_Identity.Models
{
    public class RecruiterModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is Required!")]
        public string Firstname { get; set; }


        public string Lastname { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        //[EmailAddress]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",
                   ErrorMessage = "Entered Email format is not valid.")]
        public string Email { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Identity.Models
{
    public class CandModels
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required, Display(Name = "First Name")]
        public string Firstname { get; set; }
        [Required, Display(Name = "Last Name")]
        public string Lastname { get; set; }
        [Required, Display(Name = "Phone No")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Write valid Phone No.")]
        public String Phone_no { get; set; }
        [Required, Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Write valid Email")]
        public string Email { get; set; }
        [Required, Display(Name = "Experience (In Year)")]
        public string Experience { get; set; }
        [Required, Display(Name = "Skill (First)")]
        public string Skill { get; set; }
        [Display(Name = "Current CTC")]
        public float Current_CTC { get; set; }
        [Required, Display(Name = "Expected CTC")]
        public float Expected_CTC { get; set; }
        [Required, Display(Name = "Notice period")]
        public string Notice_period { get; set; }
        [Display(Name = "Created date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public DateTime? Date_Time { get; set; }
        [Required, Display(Name = "Current Address")]
        public string Current_address { get; set; }
        [Required, Display(Name = "Permanent Address")]
        public string Permanent_address { get; set; }

        [NotMapped]
        public IEnumerable<Skills> SkillList { get; set; }

        [NotMapped]
        public string[] SelectedArray
        {
            get; set;
        }
    }
}

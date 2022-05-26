using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Identity.Models
{
    public class InterviewerModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Designation { get; set; }
        [Required, Display(Name = "Available timings")]
        public DateTime? Timing { get; set; }
        [Display(Name = "Skills")]
        public string SelectedSkillID { get; set; }
        //public string SelectedSkillName { get; set; }

        [NotMapped]
        public IEnumerable<Skills> SkillCollection { get; set; }

        [NotMapped]
        public string[] SelectedIDArray { get; set; }
    }
}
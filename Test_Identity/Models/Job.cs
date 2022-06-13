using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Identity.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string JobName { get; set; }
        public int Experience { get; set; }

        [Required(ErrorMessage = "Field can't be empty")]
        public string JobDescription { get; set; }
        //public int skillId { get; set; }

        //[ForeignKey("skillId")]
        //public Skills Skills { get; set; }
        public string SelectedSkillID { get; set; }

        [NotMapped]
        public IEnumerable<Skills> SkillCollection { get; set; }

        [NotMapped]
        public string[] SelectedIDArray { get; set; }

    }
}
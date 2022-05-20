using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test_Identity.Models
{

    public class Skills
    {
        [Key]
        public int SkillId { get; set; }
       
        [Required(ErrorMessage = "Field can't be empty")]
        public string SkillName { get; set; }

       public DateTime? LastUpdatedOn { get; set; }

        public string CommentBox { get; set; }
    }
}
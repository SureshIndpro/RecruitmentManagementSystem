using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Identity.Models
{
    public class InterviewModels
    {
        public int Id { get; set; }
        [Display(Name = "Round of Interview")]
        public string Round { get; set; }
        [Display(Name = "Candidate Name ")]
        public string CandidateId { get; set; }
        [Display(Name = "Interviewer Name ")]
        public string InterviewerId { get; set; }
        [Display(Name = "Mode of Interview")]
        public string ModeOfInterview { get; set; }
      
        [Display(Name = "Date and time of Interview")]
        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Date_Time { get; set; }

        [Required]
        [Display(Name = "Result Comment")]
        public string Comments { get; set; }

        public bool? isUpdateEnabled { get; set; }

        [NotMapped]
        public IEnumerable<CandModels> Candidate { get; set; }

        [NotMapped]
        public IEnumerable<InterviewerModel> Interview { get; set; }
        public string SelectedSkillID { get; set; }

        [NotMapped]
        public IEnumerable<Skills> SkillCollection { get; set; }
        [NotMapped]
        public string[] SelectedIDArray { get; set; }

        [Display(Name = "Result")]
        public StatusId Results { get; set; }

        public enum StatusId
        {
            Selected, 
            Selected_for_next_round, 
            On_hold, 
            Rejected, 
            Interview_not_taken
        }

    }
}
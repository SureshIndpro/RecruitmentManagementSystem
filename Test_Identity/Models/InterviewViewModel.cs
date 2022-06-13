using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test_Identity.Models
{
    public class InterviewViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Round of Interview")]
        public int Round { get; set; }
        [Display(Name = "Candidate Name ")]
        public int CandidateId { get; set; }
        [Display(Name = "Interviewer Name ")]
        public int InterviewerId { get; set; }
        public string ModeOfInterview { get; set; }

        [Display(Name = "Date and time of Interview")]

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? Date_Time { get; set; }

        [Required]
        [Display(Name = "Result Comment")]
        public string Comments { get; set; }

        [ForeignKey("CandidateId")]
        public CandModels Candidate { get; set; }

        [ForeignKey("InterviewerId")]
        public InterviewerModel Interview { get; set; }
    }
}
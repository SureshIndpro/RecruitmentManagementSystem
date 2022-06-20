using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Identity.ViewModels
{
    public class AutoMapperVM
    {
        public string InterviewerName { get; set; }
        public List<string> CandidateName { get; set; }
        public List<string> SelectedSkillID { get; set; }
    }
}
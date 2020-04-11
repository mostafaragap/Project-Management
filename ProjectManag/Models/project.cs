using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManag.Models
{
    public class project
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Project Title")]
        public string title { get; set; }
        [Required]
        [Display(Name = "Project decsription")]
        public string decsription { get; set; }
        [Required]
        [Display(Name = "Number Of Hours")]
        public double NoOfHours { get; set; }
        [Required]
        [Display(Name = "Skills need")]
        public string skill_need { get; set; }
        [Required]
        [Display(Name = "Due Deta")]
        public DateTime due_deta { get; set; }
        public DateTime creation_date { get; set; }
        public int isopen { get; set; }
        public string customerId { get; set; }

        public virtual ApplicationUser customer { get; set; }




    }
}
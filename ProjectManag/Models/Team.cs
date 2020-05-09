using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManag.Models
{
    public class Team
    {
        public int id { get; set; }

        [ForeignKey("Developer")]
        public string DeveloperId { get; set; }
        public string Feedback { get; set; }
        public string TeamLeaderID { get; set; }
        public int AssignId { get; set; }
        public string JDSkills { get; set; }
        public virtual ICollection<Developers> Developer { get; set; }
        public virtual AssignJob Assign { get; set; }
    }
}
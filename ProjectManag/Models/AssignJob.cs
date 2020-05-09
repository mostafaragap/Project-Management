using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManag.Models
{
    public class AssignJob
    {
        public int id { get; set; }

        public string Message { get; set; }
        public string State { get; set; }
        public string Customer_Notes { get; set; }
        public int projectId { get; set; }
        public string UserId { get; set; }
        public string TeamleaderId { get; set; }
        // public int TeamId { get; set; }
        public double Price { get; set; }
        public int TLState { get; set; }
        public virtual project project { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual TeamLeader Teamleader { get; set; }
    }
}
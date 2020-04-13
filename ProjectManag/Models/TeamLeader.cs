using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManag.Models
{
    public class TeamLeader
    {
        [Key, ForeignKey("ApplicationUser")]
        public string TeamLeaderId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
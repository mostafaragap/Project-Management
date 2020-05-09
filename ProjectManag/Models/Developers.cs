using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectManag.Models
{
    public class Developers
    {
        [Key, ForeignKey("ApplicationUser")]
        public string developerID { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Team team { get; set; }
    }
}
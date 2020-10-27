using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
    [Table("tEnrollment")]
    public class Enrollment
    {
        [Key]
        public int ID { get; set; }
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
    [Table("tResult")]
    public class Result
    {
        [Key]
        public int ID { get; set; }
        public string TotalMarks { get; set; }
        public short ObtainedMarks { get; set; }
        public short InternalMarks { get; set; }
        public string ExternalMarks { get; set; }
        public string Grade { get; set; }

    [ForeignKey("Enrollment")]
    public int EnrollmentId { get; set; }
    public virtual Enrollment Enrollment { get; set; }


    }
}

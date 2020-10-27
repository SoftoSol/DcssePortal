using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{[Table("tCoursesScheme")]
    public class CoursesScheme
    {
        [Key]
        public int ID { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int credithour { get; set; }
        public string SemesterOffer { get; set; }
        public string Department { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{   [Table("tCourseContent")]
   public class CourseContent
    {
        [Key]
        public int ID { get; set; }
        public string CourseCode { get; set; }
        public int CourseTitle { get; set; }
        public int CourseData { get; set; }
    }
}

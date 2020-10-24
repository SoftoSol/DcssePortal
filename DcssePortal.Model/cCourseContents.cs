using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
    [Table("tCourseContents")]
    public class cCourseContents
    {
        [Key]
        public int ID { get; set; }
        public string CourseCode { get; set; }
        public string CourseData { get; set; }
        public string CourseTitle { get; set; }
        

    }
}

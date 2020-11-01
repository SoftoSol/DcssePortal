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
        public Char Grade { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }


        public Result()
        {
            Student = new Student();
            Course = new Course();
        }



    }
}

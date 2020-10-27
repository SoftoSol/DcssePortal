using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
    [Table("tFeedback")]
    public class Feedback
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string FileURL { get; set; }
        public virtual Course Course { get; set; }

    }
}

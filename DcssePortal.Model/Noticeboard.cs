using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
    [Table("tNoticeboard")]
    public class Noticeboard
    {
        [Key]
        public int ID { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
  [Table("tEnrollment")]
  public class Enrollment
  {
    [Key]
    public int ID { get; set; }
    [Required]
    [Index("IX_Course_And_Student", 1, IsUnique = true)]
    public virtual Course Course { get; set; }
    [Required]
    [Index("IX_Course_And_Student", 2, IsUnique = true)]
    public virtual Student Student { get; set; }
    //public virtual Feedback Feedback { get; set; }
  }
}

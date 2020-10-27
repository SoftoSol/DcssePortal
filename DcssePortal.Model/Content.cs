using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DcssePortal.Model
{
   public class Content
    {
        public int ID { get; set; }
        public string Data { get; set; }
        public string ContentTitle { get; set; }
        public virtual Course Course { get; set; }
    }
}

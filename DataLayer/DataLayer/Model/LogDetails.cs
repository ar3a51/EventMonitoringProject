using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Model
{
   public class LogDetails
    {
        public long eventId { set; get; }
        public String source {set; get;}
        public DateTime dtGenerated { set; get; }
        public String chUsername { set; get; }
        public String type { set; get; }
       

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Model
{
    public class UserDetails
    {
        public String sUsername { set; get; }
        public String sFirstname { set; get; }
        public String sLastname { set; get; }
        public String sPrimaryEmail { set; get; }
        public String sSecondaryEmail { set; get; }
        public String[] machines { set; get; }
        
    }
}

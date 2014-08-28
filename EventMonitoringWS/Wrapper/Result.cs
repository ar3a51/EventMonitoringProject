using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventMonitoringWS.Wrapper
{
    public class ResultSet
    {
        public static String SUCCESS = "success";
        public static String FAIL = "fail";

        public String stringResultType {set; get;}
        public String stringResultMessage {set; get;}
        public String stringResultData {set; get;}
    }
}
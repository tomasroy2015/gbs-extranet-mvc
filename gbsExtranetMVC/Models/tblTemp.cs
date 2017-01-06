using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models
{
    public class tblTemp
    {

        public long TempID { get; set; }
        public string Temp { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
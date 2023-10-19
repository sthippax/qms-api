using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDAL.Models
{
    public class ReqGetSilicon
    {
        public List<SiliconReq> SiliconReq { get; set; }
    }
    public class SiliconReq
    {
        public string PlatformName { get; set; }        
        public string SKUName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDAL.Models
{
    internal class AppSettings
    {
        public string ConnectionString { get; set; }
        public string ImageUrl { get; set; }
        public string DefaultImage { get; set; }
    }
}

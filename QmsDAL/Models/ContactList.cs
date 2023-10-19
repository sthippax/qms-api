using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsDAL.Models
{
    public class ContactList
    {
        public string EmailId { get; set; }
        public string Name { get; set; }
        public string IDSID { get; set; }
        public int WWID { get; set; }
        public string EmployeeBadgeType { get; set; }
        public string AvatarURL { get; set; }
        public string Role { get; set; }
        public string Domain { get; set; }
        public string Comments { get; set; }
    }
}

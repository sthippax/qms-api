using QmsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsRepository.Interface
{
    public interface IAuthorizeInterface
    {
        Task<ContactList> WhoAmI(string indentifier);
    }
}

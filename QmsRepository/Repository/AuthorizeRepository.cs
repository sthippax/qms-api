using QmsDAL.DataAccess;
using QmsDAL.Models;
using QmsRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsRepository.Repository
{
    public class AuthorizeRepository : IAuthorizeInterface
    {
        #region [Code Owner : Chenthilkumaran (11-04-2023)]
        public Task<ContactList> WhoAmI(string indentifier)
        {
            try
            {
                ContactList response = new AuthorizeDAL().GetUserAccount(indentifier);
                return Task.FromResult(response);
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}

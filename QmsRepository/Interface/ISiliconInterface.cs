using QmsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsRepository.Interface
{
    public interface ISiliconInterface
    {        
        Task<object> GetPreSilicon(ReqGetSilicon objData);
        Task<object> GetPostSiliconDefects(ReqGetSilicon objData);
        Task<object> GetPostSiliconDeviation(ReqGetSilicon objData);
        Task<object> GetQMS(ReqGetQMS objData);
        Task<object> UpdateQMS(ReqUpdateQMS objData);
        Task<object> GetSecondQMS();
        Task<object> UpdateSecondQMS(ReqUpdateSecondQMS objData);

    }
}

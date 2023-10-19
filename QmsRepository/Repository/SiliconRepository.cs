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
    public class SiliconRepository : ISiliconInterface
    {
        #region GetPreSilicon [Code Owner : Chenthilkumaran (24-04-2023)]
        public Task<object> GetPreSilicon(ReqGetSilicon objData)
        {
            try
            {
                object response = new SiliconDAL().GetPreSilicon(objData);
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region GetPostSiliconDefects [Code Owner : Chenthilkumaran (24-04-2023)]
        public Task<object> GetPostSiliconDefects(ReqGetSilicon objData)
        {
            try
            {
                object response = new SiliconDAL().GetPostSiliconDefects(objData);
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region GetPostSiliconDeviation [Code Owner : Chenthilkumaran (24-04-2023)]
        public Task<object> GetPostSiliconDeviation(ReqGetSilicon objData)
        {
            try
            {
                object response = new SiliconDAL().GetPostSiliconDeviation(objData);
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        
        #region GetQMS [Code Owner : Chenthilkumaran (10-07-2023)]
        public Task<object> GetQMS(ReqGetQMS objData)
        {
            try
            {
                object response = new SiliconDAL().GetQMS(objData);
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region UpdateQMS [Code Owner : Chenthilkumaran (10-07-2023)]
        public Task<object> UpdateQMS(ReqUpdateQMS objData)
        {
            try
            {
                object response = new SiliconDAL().UpdateQMS(objData);
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
       
        #region GetSecondQMS [Code Owner : Chenthilkumaran (23-07-2023)]
        public Task<object> GetSecondQMS()
        {
            try
            {
                object response = new SiliconDAL().GetSecondQMS();
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region UpdateSecondQMS [Code Owner : Chenthilkumaran (23-07-2023)]
        public Task<object> UpdateSecondQMS(ReqUpdateSecondQMS objData)
        {
            try
            {
                object response = new SiliconDAL().UpdateSecondQMS(objData);
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

    }
}

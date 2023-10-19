using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QmsDAL.Models;
using QmsRepository.Interface;

namespace QmsApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SiliconController : ControllerBase
    {
        private ISiliconInterface _Isilicon;
        public SiliconController(ISiliconInterface _ISilicon)
        {
            _Isilicon = _ISilicon;
        }
        #region GetPreSilicon [Code Owner : Chenthilkumaran (24-04-2023)]
        [HttpPost("GetPreSilicon")]
        public async Task<IActionResult> GetPreSilicon(ReqGetSilicon objData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                object result = await _Isilicon.GetPreSilicon(objData);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpMessage().GetOops());
            }

        }
        #endregion
        #region GetPostSiliconDefects [Code Owner : Chenthilkumaran (24-04-2023)]
        [HttpPost("GetPostSiliconDefects")]
        public async Task<IActionResult> GetPostSiliconDefects(ReqGetSilicon objData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                object result = await _Isilicon.GetPostSiliconDefects(objData);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpMessage().GetOops());
            }

        }
        #endregion
        #region GetPostSiliconDeviation [Code Owner : Chenthilkumaran (24-04-2023)]
        [HttpPost("GetPostSiliconDeviation")]
        public async Task<IActionResult> GetPostSiliconDeviation(ReqGetSilicon objData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                object result = await _Isilicon.GetPostSiliconDeviation(objData);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpMessage().GetOops());
            }

        }
        #endregion

        #region GetQMS [Code Owner : Chenthilkumaran 10-07-2023)]
        [HttpPost("GetQMS")]
        public async Task<IActionResult> GetQMS(ReqGetQMS objData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                object result = await _Isilicon.GetQMS(objData);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpMessage().GetOops());
            }

        }
        #endregion
        #region UpdateQMS [Code Owner : Chenthilkumaran 10-07-2023)]
        [HttpPost("UpdateQMS")]
        public async Task<IActionResult> UpdateQMS(ReqUpdateQMS objData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                object result = await _Isilicon.UpdateQMS(objData);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpMessage().GetOops());
            }

        }
        #endregion

        #region GetSecondQMS [Code Owner : Chenthilkumaran 23-07-2023)]
        [HttpPost("GetSecondQMS")]
        public async Task<IActionResult> GetSecondQMS()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                object result = await _Isilicon.GetSecondQMS();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpMessage().GetOops());
            }

        }
        #endregion
        #region UpdateSecondQMS [Code Owner : Chenthilkumaran 10-07-2023)]
        [HttpPost("UpdateSecondQMS")]
        public async Task<IActionResult> UpdateSecondQMS(ReqUpdateSecondQMS objData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                object result = await _Isilicon.UpdateSecondQMS(objData);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new HttpMessage().GetOops());
            }

        }
        #endregion

    }
}

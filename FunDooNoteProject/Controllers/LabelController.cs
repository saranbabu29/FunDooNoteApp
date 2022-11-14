using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using RepositoryLayer.Entity;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FunDooNoteProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL ilabelBL;
        private readonly ILogger<LabelController> logger;
        private readonly IDistributedCache distributedCache;
       private readonly FundooContext fundooContext;
        public LabelController(ILabelBL ilabelBL, ILogger<LabelController> logger , IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.ilabelBL = ilabelBL;
            this.logger = logger;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult AddLabel(LabelModel labelModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = ilabelBL.AddLabel(labelModel, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Created Successful", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Somwthing went Wrong"
                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpGet]
        [Route("Read")]
        public IActionResult ReadLabel()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = ilabelBL.ReadLabel(UserId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "label Open Successful", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Something went wrong"
                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateLabel(LabelModel labelModel, long LabelId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = ilabelBL.UpdateLabel(labelModel,LabelId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Updated Successfully", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Something Went Wrong"
                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteLabel( long LabelId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = ilabelBL.DeleteLabel(UserId, LabelId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Label Deleted Successfully", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Something Went Wrong"
                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCustomersUsingRedisCache()
        {
            var cacheKey = "labelList";
            string serializedLabelList;
            var labelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                labelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                labelList = fundooContext.labelTable.ToList();
                serializedLabelList = JsonConvert.SerializeObject(labelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(labelList);
        }

    }
}

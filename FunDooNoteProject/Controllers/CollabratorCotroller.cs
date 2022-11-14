using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;

namespace FunDooNoteProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollabratorController : ControllerBase
    {
        private readonly ICollabratorBL icollabratorBL;
        private readonly ILogger<CollabratorController> logger;
        private readonly IDistributedCache distributedCache;
        private readonly FundooContext fundooContext;
        public CollabratorController(ICollabratorBL icollabratorBL, ILogger<CollabratorController> logger, IDistributedCache distributedCache, FundooContext fundooContext)
        {
            this.icollabratorBL = icollabratorBL;
            this.logger = logger;
            this.distributedCache = distributedCache;
            this.fundooContext = fundooContext;
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult AddCollabrator(CollabratorModel collabratorModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = icollabratorBL.AddCollabrator(collabratorModel, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collabrator Created Successful", data = result });
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
        public IActionResult ReadCollabrator()
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = icollabratorBL.ReadCollabrator(UserId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collabrator Open Successful", data = result });
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
        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteCollabrator(long CollabratorId)
        {
            try
            {
                long UserId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = icollabratorBL.DeleteCollabrator(UserId, CollabratorId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Collabrator Deleted Successfully", data = result });
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
            var cacheKey = "collabratorList";
            string serializedCollabratorList;
            var collabratorList = new List<CollabratorEntity>();
            var redisCollabratorList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabratorList != null)
            {
                serializedCollabratorList = Encoding.UTF8.GetString(redisCollabratorList);
                collabratorList = JsonConvert.DeserializeObject<List<CollabratorEntity>>(serializedCollabratorList);
            }
            else
            {
                collabratorList = fundooContext.CollabratorTable.ToList();
                serializedCollabratorList = JsonConvert.SerializeObject(collabratorList);
                redisCollabratorList = Encoding.UTF8.GetBytes(serializedCollabratorList);
                var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabratorList, options);
            }
            return Ok(collabratorList);
        }
    }
}

using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using RepositoryLayer.Entity;

namespace FunDooNoteProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL ilabelBL;
        private readonly ILogger<LabelController> logger;
        public LabelController(ILabelBL ilabelBL, ILogger<LabelController> logger)
        {
            this.ilabelBL = ilabelBL;
            this.logger = logger;
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

    }
}

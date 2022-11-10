using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.Eventing.Reader;

namespace FunDooNoteProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly INoteBL inoteBL;
        private readonly ILogger<NoteController> logger;
        public NoteController(INoteBL inoteBL, ILogger<NoteController> logger)
        {
            this.inoteBL = inoteBL;
            this.logger = logger;
        }
        [HttpPost]
        [Route("Create")]
        public IActionResult CreateNote(AddNoteModel addNoteModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = inoteBL.AddNote(addNoteModel, userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Created Successful", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Note Did Not Save"
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
        public IActionResult ReadNote()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = inoteBL.ReadNote(userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Open Successful", data = result });
                }
                else
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Notes are Not Available"
                    });
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        [HttpDelete]
        [Route("Remove")]
        public IActionResult DeleteNote(long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = inoteBL.DeleteNote(userId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Deleted Successfully", data = result });
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
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNote(AddNoteModel addNoteModel,long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = inoteBL.UpdateNote(addNoteModel,userId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Updated Successfully", data = result });
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
        [HttpPut]
        [Route("Pin")]
        public IActionResult PinNote(long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = inoteBL.PinNote(userId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Pinned Successfully", data = result });
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
        [HttpPut]
        [Route("Archieve")]
        public IActionResult ArchieveNote(long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = inoteBL.ArchieveNote(userId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Archieved Successfully", data = result });
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
        [HttpPut]
        [Route("Trash")]
        public IActionResult TrashNote(long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = inoteBL.TrashNote(userId, NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note Moved to Trash Successfully", data = result });
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
        [HttpPut]
        [Route("Colour")]
        public IActionResult NoteColour(string colour,long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = inoteBL.NoteColour(colour,NoteId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Note colour changed Successfully", data = result });
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
        [HttpPut]
        [Route("Image")]
        public IActionResult AddImage(IFormFile Image, long NoteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "userID").Value);
                var result = inoteBL.AddImage(Image, NoteId,userId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Image Added Successfully", data = result });
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

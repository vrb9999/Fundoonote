using BusinessLayer.Interface;
using DatabaseLayer.NoteModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoonote_Ado.Net.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        INoteBL noteBL;
        public NoteController(INoteBL noteBL)
        {
            this.noteBL = noteBL;
        }


        [Authorize]
        [HttpPost("AddNote")]
        public async Task<IActionResult> AddNote(int UserId, NoteModel noteModel)
        {
            if (noteModel == null)
            {
                return BadRequest("Note is null.");
            }
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                UserId = Int32.Parse(userId.Value);
                await this.noteBL.AddNote(UserId, noteModel);
                return Ok(new { success = true, Message = "Note Created Successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpGet("GetAllNotes")]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                var result = await this.noteBL.GetAllNotes(UserId);
                return Ok(new { success = true, Message = "All Notes Fetch Successfully", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("UpdateNote")]
        public async Task<IActionResult> UpdateNote(int NoteId, UpdateNoteModel updateNoteModel)
        {
            if (updateNoteModel == null)
            {
                return BadRequest("Note is null.");
            }
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                if (updateNoteModel.Title == "" || updateNoteModel.Title == "string" && updateNoteModel.Description == "string" && updateNoteModel.Bgcolor == "string")
                {
                    return this.BadRequest(new { sucess = false, Message = "Please Provide Valid Fields for Note!!" });
                }
                await this.noteBL.UpdateNote(UserId, NoteId, updateNoteModel);
                return Ok(new { sucess = true, Message = "Note Updated Successfully..." });
            }
            catch (Exception ex)
            {
                if (ex.Message == "Note Does Not Exist!!")
                {
                    return this.BadRequest(new { sucess = false, Message = "Note Does not Exists!!" });
                }
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteNote/{NoteId}")]
        public async Task<IActionResult> DeleteNote(int NoteId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                await this.noteBL.DeleteNote(UserId, NoteId);
                return Ok(new { success = true, Message = "Deleted SuccessFully" });
            }
            catch (Exception ex)
            {
                if (ex.Message == "Note Does not Exists")
                {
                    return this.BadRequest(new { success = false, Message = "Note Does not Exists" });

                }
                throw ex;
            }
        }
    }
}

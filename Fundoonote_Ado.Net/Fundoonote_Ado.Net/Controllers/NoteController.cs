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
        public async Task<IActionResult> Index(NoteModel noteModel)
        {
            if (noteModel == null)
            {
                return BadRequest("Note is null.");
            }
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userId.Value);
                await this.noteBL.AddNote(UserId, noteModel);
                return Ok(new { success = true, Message = "Note Created Successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

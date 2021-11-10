using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace ElevenNote.WebAPI.Controllers
{
    [RoutePrefix("api/Notes")]
    [Authorize]
    public class NoteController : ApiController
    {
        [Route]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(CreateNoteService().GetNotes());
        }

        [Route("GetById/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(CreateNoteService().GetNoteById(id));
        }

        [Route("GetByTitle/{title}")]
        [HttpGet]
        public IHttpActionResult Get(string title)
        {
            return Ok(CreateNoteService().GetNoteByTitle(title));
        }

        [Route("Create")]
        [HttpPost]
        public IHttpActionResult Post(NoteCreate note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!CreateNoteService().CreateNote(note))
                return InternalServerError();

            return Ok();
        }

        [Route("Update")]
        [HttpPut]
        public IHttpActionResult Put(NoteEdit note)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!CreateNoteService().UpdateNote(note))
                return InternalServerError();

            return Ok();
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (!CreateNoteService().DeleteNote(id))
                return InternalServerError();

            return Ok();
        }

        //Helper Methods
        private NoteService CreateNoteService()
        {
            //var userId = Guid.Parse(User.Identity.GetUserId());
            //var noteService = new NoteService(userId);
            //return noteService;
            return new NoteService(GetUser());
        }

        private Guid GetUser()
        {
            return Guid.Parse(User.Identity.GetUserId());
        }

    }
}

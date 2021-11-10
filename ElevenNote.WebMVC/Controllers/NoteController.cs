using ElevenNote.Data;
using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElevenNote.WebMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        // GET: Notes
        public ActionResult Index()
        {
            return View(new NoteListItem[0]);
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            var svc = CreateNoteService();
            var model = svc.GetNotes();
            return View(model);
        }

        // POST: Notes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            CreateNoteService().CreateNote(model);

            return RedirectToAction("Index");
        }

        // Helper Methods

        // Creates a user-authenticated instance of NoteService
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new NoteService(userId);
        }
    }
}
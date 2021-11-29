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
            var svc = CreateNoteService();
            var model = svc.GetNotes();
            TempData["Categories"] = svc.GetCategoryOptions();
            return View(model);
        }

        // GET: Notes (by category)

        // GET: Note/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var svc = CreateNoteService();

            if (!svc.CreateNote(model))
            {
                ModelState.AddModelError("", "Note could not be created.");
                return View(model);
            }

            return RedirectToAction("Index");
        }

        // GET: Note/Delete
        // POST: Note/Delete

        // GET: Note/Update
        // POST: Note/Update

        // GET: Note/Details (by id)
        public ActionResult Details(int? id)
        {
            if (id is null)
                return RedirectToAction("Index");
            var model = CreateNoteService().GetNoteById((int)id);
            return View(model);
        }
        // GET: Note/Details (by title)
        public ActionResult Details(string title)
        {
            if (title is null)
                return RedirectToAction("Index");
            var model = CreateNoteService().GetNoteByTitle(title);
            return View(model);
        }

        // <--- HELPER METHODS --->

        // Creates a user-authenticated instance of NoteService
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            return new NoteService(userId);
        }
    }
}
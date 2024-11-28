using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;

// Generated from Custom Template.

namespace MVC.Controllers
{
    public class MovieController : MvcController
    {
        // Service injections:
        private readonly ImovieService _movieService;
        private readonly IDirectorService _directorService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public MovieController(
			ImovieService movieService
            , IDirectorService directorService

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _movieService = movieService;
            _directorService = directorService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Movie
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _movieService.Query().ToList();
            return View(list);
        }

        // GET: Movie/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _movieService.Query().SingleOrDefault(q => q.Record.id == id);
            return View(item);
        }
        public class DirectorSelectModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }


        protected void SetViewData()
        {
            // Fetch directors
            var directors = _directorService.Query()
                .Select(d => new DirectorSelectModel
                {
                    Id = d.Record.id,
                    Name = d.Record.name
                })
                .ToList();

            // Ensure the list is valid
            if (!directors.Any())
            {
                directors = new List<DirectorSelectModel>();
            }

            // Assign to ViewData
            ViewData["directorid"] = new SelectList(directors, "Id", "Name");
        }



        // GET: Movie/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieModel movie)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _movieService.Create(movie.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movie.Record.id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movie);
        }

        // GET: Movie/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _movieService.Query().SingleOrDefault(q => q.Record.id == id);
            SetViewData();
            return View(item);
        }

        // POST: Movie/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MovieModel movie)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _movieService.Update(movie.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = movie.Record.id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(movie);
        }

        // GET: Movie/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _movieService.Query().SingleOrDefault(q => q.Record.id == id);
            return View(item);
        }

        // POST: Movie/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _movieService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}

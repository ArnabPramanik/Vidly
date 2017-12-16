using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
          _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult New()
        {
            MovieFormViewModel movieFormViewModel = new MovieFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                
            };

            return View("MovieForm", movieFormViewModel);
        }

        public ActionResult Edit(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if(movieInDb == null)
            {
                return HttpNotFound();
            }
            MovieFormViewModel movieFormViewModel = new MovieFormViewModel()
            {
                Genres = _context.Genres.ToList(),
                

            };
    

            return View("MovieForm", movieFormViewModel);

        }

        public ActionResult Delete(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m=>m.Id == id);
            if(movieInDb == null)
            {
                return HttpNotFound();
            }
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return RedirectToAction("Index","Movies");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    
                    Genres = _context.Genres.ToList()


                };
                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m=>movie.Id == m.Id);
                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.GenreId = movie.GenreId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        
        // GET: Movies
        public ViewResult Index()
        {
            var movies = _context.Movies.Include(c=>c.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c=>c.Genre).SingleOrDefault(c => c.Id == id);
            if(movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);

        }
    }
}
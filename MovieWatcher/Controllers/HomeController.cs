using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieWatcher.Data;
using MovieWatcher.Domain;
using MovieWatcher.Models;

namespace MovieWatcher.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _appContext;
        public HomeController(ApplicationDbContext applicationDbContext)
        {
            _appContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            List<HomeListViewModel> films = new List<HomeListViewModel>();
            IEnumerable<Film> filmsFromDb = _appContext.Films;
            GezienStatus[] gezienStatusesFromDb = _appContext.GezienStatuses.ToArray();
            List<SelectListItem> gezienStatuses = new List<SelectListItem>();
            foreach (var item in gezienStatusesFromDb)
            {
                gezienStatuses.Add(new SelectListItem()
                {
                    Text = item.Naam,
                    Value = item.Id.ToString()
                });
            }

            foreach (var item in filmsFromDb)
            {
                films.Add(new HomeListViewModel()
                {
                    Id = item.Id,
                    Titel = item.Titel,
                    Beschrijving = item.Beschrijving,
                    GezienStatussen = gezienStatuses
                });
            }

            return View(films);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AdminPage()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HomeCreateViewModel model)
        {
            if (!TryValidateModel(model))
            {
                return View(model);
            }
            var film = new Film()
            {
                Titel = model.Titel,
                Beschrijving = model.Beschrijving,

            };
            using (var memoryStream = new MemoryStream())
            {
                await model.Foto.CopyToAsync(memoryStream);
                film.Foto = memoryStream.ToArray();

            }
            _appContext.Films.Add(film);
            _appContext.SaveChanges();
            return RedirectToAction("AdminPage");
        }
        public IActionResult Details(int id)
        {
            Film filmFromDb = _appContext.Films.FirstOrDefault(x => x.Id == id);
            Rating[] ratingsFromDb = _appContext.Ratings.ToArray();
            List<Rating> ratings = new List<Rating>();
            IdentityUser[] users = _appContext.Users.ToArray();
            UserFilmGezienStatus[] userFilmGezienStatusFromDb = _appContext.UserFilmGeziens.Where(x => x.FilmId == id).ToArray();
            

            HomeDetailViewModel model = new HomeDetailViewModel()
            {
                Titel = filmFromDb.Titel,
                Beschrijving = filmFromDb.Beschrijving,
                Foto = filmFromDb.Foto,
                UserFilmGezienStatuses = userFilmGezienStatusFromDb
                

            };
            foreach (var item in ratingsFromDb)
            {

                var user = users.FirstOrDefault(x => x.Id == item.UserId);
                item.User.UserName = user.UserName;

                ratings.Add(item);
            }
            model.Ratings = ratings;

            return View(model);
        }
        public IActionResult Rating(int id)
        {
            Rating[] ratingsFromDb = _appContext.Ratings.ToArray();
            List<SelectListItem> ratings = new List<SelectListItem>();
            Film filmFromDb = _appContext.Films.FirstOrDefault(x => x.Id == id);


            for (int i = 1; i < 11; ++i)
            {
                ratings.Add(new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString(),
                });
            }
            HomeRatingViewModel model = new HomeRatingViewModel()
            {
                Titel = filmFromDb.Titel,
                Beschrijving = filmFromDb.Beschrijving,
                Foto = filmFromDb.Foto,
                Ratings = ratings,
               
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Rating(int id, HomeRatingViewModel model)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!TryValidateModel(model))
            {
                return View(model);
            }

            var rating = new Rating()
            {
                RatingPoints = model.Rating.RatingPoints,
                FilmId = id,
                UserId = userId,
                Review = model.Rating.Review
            };
            try
            {
            _appContext.Ratings.Add(rating);
            _appContext.SaveChanges();

            }
            catch (Exception)
            {
                _appContext.Ratings.Update(rating);
                _appContext.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Gezien(int id, HomeListViewModel item)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            var gezien = new UserFilmGezienStatus()
            {
                FilmId = id,
                UserId = userId,
                GezienStatusId = int.Parse(item.Gezien)
            };
            try
            {

            _appContext.UserFilmGeziens.Add(gezien);

            _appContext.SaveChanges();
            }
            catch (Exception)
            {
                _appContext.UserFilmGeziens.Update(gezien);

                _appContext.SaveChanges();
              
            }
            return RedirectToAction("Index");
        }
    }
}

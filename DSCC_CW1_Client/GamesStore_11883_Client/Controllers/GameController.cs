using GamesStore_11883_Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace GamesStore_11883_Client.Controllers
{
    public class GameController : Controller
    {
        private readonly string baseUrl = new ConfigController().baseurl;

        // GET: Game
        public async Task<ActionResult> Index()
        {
            List<Game> Games = new List<Game>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                http.DefaultRequestHeaders.Clear();

                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await http.GetAsync("/api/Game");

                if (res.IsSuccessStatusCode)
                {
                    var data = res.Content.ReadAsStringAsync().Result;
                    Games = JsonConvert.DeserializeObject<List<Game>>(data);
                }
                return View(Games);
            }
        }
        public async Task<Game> GetById(int id) {
            Game Game = new Game();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                http.DefaultRequestHeaders.Clear();

                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await http.GetAsync($"/api/Game/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var data = res.Content.ReadAsStringAsync().Result;
                    Game = JsonConvert.DeserializeObject<Game>(data);
                }
                return Game;
            }
        }
        // GET: Game/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Game game = await GetById(id);
            return View(game);
        }

        // GET: Game/Create
        public async Task<ActionResult> Create()
        {
            AuthorController ctrl = new AuthorController();
            List<Author> authors = await ctrl.GetAuthors();
            ViewBag.Authors = authors;
            return View();
        }

        // POST: Game/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Game game)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(baseUrl);
                    http.DefaultRequestHeaders.Clear();
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(game), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await http.PostAsync("/api/Game/", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Handle the error case
                        ModelState.AddModelError(string.Empty, "Failed to create the game.");
                        return View(game);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the game.");
                return View(game);
            }
        }

        // GET: Game/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Game game = await GetById(id);
            AuthorController ctrl = new AuthorController();
            List<Author> authors = await ctrl.GetAuthors();
            ViewBag.Authors = authors;
            return View(game);
        }

        // POST: Game/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Game game)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(baseUrl);
                    http.DefaultRequestHeaders.Clear();
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var content = new StringContent(JsonConvert.SerializeObject(game), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await http.PutAsync($"/api/Game/{id}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Handle the error case
                        ModelState.AddModelError(string.Empty, "Failed to update the game.");
                        return View(game);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the game.");
                return View(game);
            }
        }

        // GET: Game/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Game game = await GetById(id);
            return View(game);
        }

        // POST: Game/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(baseUrl);
                    http.DefaultRequestHeaders.Clear();
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await http.DeleteAsync($"/api/Game/{id}");

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorMessage = "Failed to delete the game. Status code: " + response.StatusCode;
                        ModelState.AddModelError(string.Empty, errorMessage);
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the game.");
                return View();
            }
        }
    }
}

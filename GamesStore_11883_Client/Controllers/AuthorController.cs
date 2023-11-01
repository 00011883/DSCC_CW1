using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GamesStore_11883_Client.Models;
using System.Text;

namespace GamesStore_11883_Client.Controllers
{
    public class AuthorController : Controller
    {
        private readonly string baseUrl = new ConfigController().baseurl;
        // GET: Author
        public async Task<ActionResult> Index()
        {
            List<Author> Authors = await GetAuthors();
            return View(Authors);
        }

        public async Task<List<Author>> GetAuthors()
        {
            List<Author> Authors = new List<Author>();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                http.DefaultRequestHeaders.Clear();
                // Setup http headers
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await http.GetAsync("/api/Author");

                if (res.IsSuccessStatusCode)
                {
                    var data = res.Content.ReadAsStringAsync().Result;
                    // Parse string into JSON format
                    Authors = JsonConvert.DeserializeObject<List<Author>>(data);
                }
                return Authors;
            }
        }

        // Separate function to get author by ID for multiple usage DRY Principle
        public async Task<Author> GetById(int id)
        {
            Author Author = new Author();
            using (var http = new HttpClient())
            {
                http.BaseAddress = new Uri(baseUrl);
                http.DefaultRequestHeaders.Clear();
                // Setup http headers
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await http.GetAsync($"/api/Author/{id}");

                if (res.IsSuccessStatusCode)
                {
                    var data = res.Content.ReadAsStringAsync().Result;
                    // Parse string into JSON format
                    Author = JsonConvert.DeserializeObject<Author>(data);
                }
                return Author;
            }
        }

        // GET: Author/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Author author = await GetById(id);
            return View(author);
        }

        // GET: Author/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        public async Task<ActionResult> Create(Author author)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(baseUrl);
                    http.DefaultRequestHeaders.Clear();
                    // Setup http headers
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // Parse string into JSON format
                    var content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await http.PostAsync("/api/Author/", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Handle the error case
                        ModelState.AddModelError("AddAuthorFailure", "Failed to add the author.");
                        return View(author);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UnexpectedAuthorError", "An error occurred while adding the author.");
                return View(author);
            }
        }

        // GET: Author/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Author author = await GetById(id);
            return View(author);
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Author author)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(baseUrl);
                    http.DefaultRequestHeaders.Clear();
                    // Setup http headers
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // Parse string into JSON format
                    var content = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await http.PutAsync($"/api/Author/{id}", content);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Handle the error case
                        ModelState.AddModelError("UpdateAuthorFailure", "Failed to update the author.");
                        return View(author);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UnexpectedUpdateAuthorError", "An error occurred while updating the author.");
                return View(author);
            }
        }

        // GET: Author/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Author author = await GetById(id);
            return View(author);
        }

        // POST: Author/Delete/5
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
                    // Setup http headers
                    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await http.DeleteAsync($"/api/Author/{id}");

                    // Check if the response is successful
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        var errorMessage = "Failed to delete the author. Status code: " + response.StatusCode;
                        ModelState.AddModelError("DeleteAuthorFailure", errorMessage);
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("UnexpectedDeleteAuthorError", "An error occurred while deleting the author.");
                return View();
            }
        }
    }
}

using Newtonsoft.Json.Linq;
using Web_MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net.Http.Headers;

namespace Web_MVC.Controllers
{
    public class AuthorMVCController : Controller
    {
        public async Task<ActionResult> GetAuthors()
        {
            IEnumerable<AuthorViewModel> authors = null;
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var response = await client.GetAsync("/api/Author");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsAsync<IList<AuthorViewModel>>();
                       authors = responseContent;
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            if (authors != null)
            {
                return View(authors);
            }
            else
            {
                return View();
            }
        }


        //GET /AuthorMVC/CreateAuthor
        public ActionResult CreateAuthor()
        {
            return View();
        }

        //Post /AuthorMVC/CreateAuthor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAuthor(AuthorViewModel model)
        {
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var response = await client.PostAsJsonAsync("/api/Author", model);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("GetAuthors", "AuthorMVC");
                    }
                }
            }
            return View("Error");
        }

        //GET /AuthorMVC/EditAuthor
        public ActionResult EditAuthor(int id)
        {
            AuthorViewModel author = null;
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var responseTask = client.GetAsync("/api/Author?id=" + id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<AuthorViewModel>();
                        readTask.Wait();

                        author = readTask.Result;
                    }
                }
            }
            return View(author);
        }

        //GET /BookMVC/EditBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAuthor(AuthorViewModel model)
        {
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var response = await client.PutAsJsonAsync("/api/Author", model);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("GetAuthors", "AuthorMVC");
                    }
                }
            }
            return View("Error");
        }

        public async Task<ActionResult> DeleteAuthor(int id)
        {
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var response = await client.DeleteAsync("/api/Author/" + id.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetAuthors", "AuthorMVC");
                    }
                }
            }
            return View("Error");
        }

        public async Task<ActionResult> DetailsAuthor(int id)
        {
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var response = await client.GetAsync("/api/Author?id=" + id.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = await response.Content.ReadAsAsync<AuthorViewModel>();
                        return View(readTask);
                    }
                }
            }
            return View("Error");
        }
    }
}
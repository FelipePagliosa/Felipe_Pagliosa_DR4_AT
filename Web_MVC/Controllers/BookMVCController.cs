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
    public class BookMVCController : Controller
    {
        public async Task<ActionResult> GetBooks()
        {
            IEnumerable<BookViewModel> books = null;
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var response = await client.GetAsync("/api/Book");
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsAsync<IList<BookViewModel>>();
                        books = responseContent;
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            }
            if (books != null)
            {
                return View(books);
            }
            else
            {
                return View();
            }
        }
        //GET /BookMVC/CreateBook
        public ActionResult CreateBook()
        {
            BookViewModel model = new BookViewModel();
            model.Authors = GetAuthorsBIMBIM();
            return View(model);
        }

        //GET /BookMVC/CreateBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBook(BookViewModel model)
        {
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var response = await client.PostAsJsonAsync("/api/Book",model);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("GetBooks","BookMVC");
                    }
                }
            }
            return View("Error");
        }

        public ICollection<AuthorViewModel> GetAuthorsBIMBIM()
        {
            ICollection<AuthorViewModel> authors = null;
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var response = client.GetAsync("/api/Author").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsAsync<IList<AuthorViewModel>>().Result;
                        authors = responseContent;
                        return authors;
                    }
                }
            }
            return authors;
        }

        //GET /BookMVC/EditBook
        public ActionResult EditBook(int id)
        {
            BookViewModel book = null;
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var responseTask =  client.GetAsync("/api/Book?id="+id.ToString());
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<BookViewModel>();
                        readTask.Wait();

                        book = readTask.Result;
                    }
                }
            }
            return View(book);
        }

        //GET /BookMVC/EditBook
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditBook(BookViewModel model)
        {
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var response = await client.PutAsJsonAsync("/api/Book", model);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return RedirectToAction("GetBooks","BookMVC");
                    }
                }
            }
            return View("Error");
        }

        public async Task<ActionResult> DeleteBook(int id)
        {
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var response = await client.DeleteAsync("/api/Book/" + id.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("GetBooks", "BookMVC");
                    }
                }
            }
            return View("Error");
        }

        public async Task<ActionResult> DetailsBook(int id)
        {
            string access_token = Session["access_token"]?.ToString();
            if (!string.IsNullOrEmpty(access_token))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44367");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                    var response = await client.GetAsync("/api/Book?id=" + id.ToString());
                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = await response.Content.ReadAsAsync<BookViewModel>();
                        return View(readTask);
                    }
                }
            }
            return View("Error");
        }
    }
}
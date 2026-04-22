using Microsoft.AspNetCore.Mvc;
using KMCEventWeb.Models;
using Newtonsoft.Json;
using System.Text;

namespace KMCEventWeb.Controllers
{
    public class EventController : Controller
    {
        private readonly string baseUrl = "http://localhost:5000/api/";

        public async Task<IActionResult> Index(string search)
        {
            List<EventVM> events = new List<EventVM>();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(baseUrl + "Event");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    events = JsonConvert.DeserializeObject<List<EventVM>>(json);
                }
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                events = events
                    .Where(e => !string.IsNullOrEmpty(e.Title) &&
                                e.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            ViewBag.Search = search;

            return View(events);
        }

        [HttpGet]
        public IActionResult Register(int id)
        {
            RegistrationVM reg = new RegistrationVM();
            reg.EventId = id;
            return View(reg);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationVM reg)
        {
            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(reg);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(baseUrl + "Registration", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["msg"] = "Registration Successful";
                    return RedirectToAction("Index");
                }
            }

            TempData["msg"] = "Registration Failed";
            return View(reg);
        }
    }
}
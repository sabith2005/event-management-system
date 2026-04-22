using Microsoft.AspNetCore.Mvc;
using KMCEventAdmin.Models;
using Newtonsoft.Json;
using System.Text;

namespace KMCEventAdmin.Controllers
{
    public class EventController : Controller
    {
        private readonly string baseUrl = "http://localhost:5000/api/Event/";
        private readonly string registrationUrl = "http://localhost:5000/api/Registration/";

        [HttpGet]
        public IActionResult Create()
        {
            EventVM ev = new EventVM();
            ev.EventDate = DateTime.Now;
            return View(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EventVM ev)
        {
            if (string.IsNullOrWhiteSpace(ev.Title) || string.IsNullOrWhiteSpace(ev.Venue))
            {
                TempData["Message"] = "Title and Venue are required.";
                return View(ev);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(ev);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(baseUrl, content);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Event added successfully";
                        return RedirectToAction("Create");
                    }

                    TempData["Message"] = "Failed to add event: " + result;
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "API connection error: " + ex.Message;
            }

            return View(ev);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EventVM ev)
        {
            if (string.IsNullOrWhiteSpace(ev.Title) || string.IsNullOrWhiteSpace(ev.Venue))
            {
                TempData["Message"] = "Title and Venue are required.";
                return View("Create", ev);
            }

            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(ev);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(baseUrl + ev.eventId + "?organizerId=" + ev.OrganizerId, content);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Event updated successfully";
                        return RedirectToAction("Create");
                    }

                    TempData["Message"] = "Failed to update event: " + result;
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "API connection error: " + ex.Message;
            }

            return View("Create", ev);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EventVM ev)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync(baseUrl + ev.eventId + "?organizerId=" + ev.OrganizerId);
                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "Event deleted successfully";
                        return RedirectToAction("Create");
                    }

                    TempData["Message"] = "Failed to delete event: " + result;
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "API connection error: " + ex.Message;
            }

            return View("Create", ev);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            EventVM ev = new EventVM();

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(baseUrl + id);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<EventVM>(json);
                        ev = data ?? new EventVM();
                    }
                    else
                    {
                        TempData["Message"] = "Event not found";
                        ev.EventDate = DateTime.Now;
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "API connection error: " + ex.Message;
                ev.EventDate = DateTime.Now;
            }

            return View("Create", ev);
        }

        [HttpGet]
        public async Task<IActionResult> Registrations()
        {
            List<RegistrationVM> registrations = new List<RegistrationVM>();

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(registrationUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<List<RegistrationVM>>(json);
                        registrations = data ?? new List<RegistrationVM>();
                    }
                    else
                    {
                        TempData["Message"] = "Failed to load registrations";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "API connection error: " + ex.Message;
            }

            return View("Registrations", registrations);
        }
    }
}
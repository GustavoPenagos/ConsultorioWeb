using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OdontologiaWeb.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace ConsultorioWeb.Controllers
{
    public class HomeController : Controller
    {
        public readonly string  api = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"];
        public async Task<dynamic> Index()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage httpResponse = await client.GetAsync(api + "/api/lista/pacientes");
                if (httpResponse.IsSuccessStatusCode)
                {
                    string response = await httpResponse.Content.ReadAsStringAsync();
                    List<Usuario>usuarios = JsonConvert.DeserializeObject<List<Usuario>>(response);
                    return View(usuarios);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
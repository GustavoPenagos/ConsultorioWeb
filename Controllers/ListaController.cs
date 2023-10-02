using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsultorioWeb.Controllers
{
    public class ListaController : Controller
    {
        public readonly string api = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"];

        public async Task<dynamic> Citas(long id=0)
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiCitas = api + "/buscar/citasxid" + "?id=" + id;
                HttpResponseMessage message = await client.GetAsync(apiCitas);
                if (message.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject(await message.Content.ReadAsStringAsync());
                    ViewBag.Citas = response;
                    return View();
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
            
            return View();
        }

        public async Task<dynamic> Historial(long id = 0)
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiCitas = api + "/buscar/usuario" + "?id=" + id + "&fuente=htl";
                HttpResponseMessage message = await client.GetAsync(apiCitas);
                if (message.IsSuccessStatusCode)
                {
                    var response = JsonConvert.DeserializeObject(await message.Content.ReadAsStringAsync());
                    ViewBag.Usuario = response;
                    return View();
                }

                return View();

            }catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<dynamic> Fotos(long id = 0)
        {
            try
            {
                HttpClient client = new HttpClient();

                string apiFotos = api + "/lista/fotos?id=" + id;
                HttpResponseMessage message = await client.GetAsync(apiFotos);
                if (message.IsSuccessStatusCode)
                {
                    ViewBag.Images = JsonConvert.DeserializeObject(await message.Content.ReadAsStringAsync());
                }

                return View();
            }catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        
    }
}

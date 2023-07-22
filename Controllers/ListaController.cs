using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            HttpClient client = new HttpClient();
            string apiCitas = api + "/api/buscar/citasxid" + "?id=" + id;
            HttpResponseMessage message = await client.GetAsync(apiCitas);
            if(message.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject(await message.Content.ReadAsStringAsync());
                ViewBag.Citas = response;
                return View();
            }
            return View();
        }
    }
}

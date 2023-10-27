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
                string apiHUsuario= api + "/buscar/usuario" + "?id=" + id + "&fuente=htl";
                HttpResponseMessage message1 = await client.GetAsync(apiHUsuario);
                if (message1.IsSuccessStatusCode)
                {
                    var response1 = JsonConvert.DeserializeObject(await message1.Content.ReadAsStringAsync());
                    ViewBag.Usuario = response1;

                    string apiHAnamnesis = api + "/buscar/anamnesis" + "?id=" + id;
                    HttpResponseMessage message2 = await client.GetAsync(apiHAnamnesis);
                    if (message2.IsSuccessStatusCode)
                    {
                        var response2 = JsonConvert.DeserializeObject(await message2.Content.ReadAsStringAsync());
                        ViewBag.Anamnesis = response2;

                        string apiHFamiliar = api + "/buscar/familiar" + "?id=" + id;
                        HttpResponseMessage message3 = await client.GetAsync(apiHFamiliar);
                        if (message3.IsSuccessStatusCode)
                        {
                            var response3 = JsonConvert.DeserializeObject(await message3.Content.ReadAsStringAsync());
                            ViewBag.Familiar = response3;

                            string apiHEstoma = api + "/buscar/estomatologico" + "?id=" + id;
                            HttpResponseMessage message4 = await client.GetAsync(apiHEstoma);
                            if (message4.IsSuccessStatusCode)
                            {
                                var response4 = JsonConvert.DeserializeObject(await message4.Content.ReadAsStringAsync());
                                ViewBag.Estomatologico = response4;


                                string apiHDental = api + "/buscar/dental" + "?id=" + id;
                                HttpResponseMessage message5 = await client.GetAsync(apiHDental);
                                if (message5.IsSuccessStatusCode)
                                {
                                    var response5 = JsonConvert.DeserializeObject(await message5.Content.ReadAsStringAsync());
                                    ViewBag.Edad = Convert.ToInt32(await new HomeController().BuscarUsuario(Convert.ToInt32(id), "registro"));
                                    ViewBag.Dental = response5;

                                }
                            }
                        }
                    }
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
                List<int> list = new List<int>();
                for (int i = 0; i < ViewBag.Images.Count; i++)
                {
                    list.Add(Convert.ToInt32(ViewBag.Images[i].id.Value));
                }

                IEnumerable<int> duplicates = list.GroupBy(x => x)
                                                .Where(g => g.Count() > 1)
                                                .Select(x => x.Key);
                var v = duplicates.Count();

                var c = String.Join(",", duplicates);

                return View();
            }catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        
    }
}

using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OdontologiaWeb.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;

namespace ConsultorioWeb.Controllers
{
    public class HomeController : Controller
    {
        public readonly string  api = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"];
        public async Task<dynamic> Index(string usuario=null, bool caso = false)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage httpResponse = await client.GetAsync(api + "/lista/pacientes");
                if (httpResponse.IsSuccessStatusCode)
                {
                    if (usuario != null)
                    {
                        ViewBag.Usuario = JsonConvert.DeserializeObject(usuario);
                        return View();
                    }
                    else
                    {
                        string response = await httpResponse.Content.ReadAsStringAsync();
                        ViewBag.Usuario = JsonConvert.DeserializeObject(response);
                        if(caso == true)
                        {
                            return response;
                        }
                        return View();
                    }

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

        public async Task<dynamic> BuscarUsuario(long id, string fuente=null)
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiBuscar = api + "/buscar/usuario" + "?id= " + id + "&fuente=" + fuente;
                HttpResponseMessage message = await client.GetAsync(apiBuscar);
                if (message.IsSuccessStatusCode)
                {
                    string usuario = await message.Content.ReadAsStringAsync();
                    switch (fuente)
                    {
                        case "editarUsuario":
                            return usuario;
                        case "registro":
                            return usuario;
                        default:
                            return RedirectToAction("Index", "Home", new { usuario = usuario });
                    }                    
                }
                else
                {
                    return RedirectToAction("Index","Home");
                }

            }catch (Exception ex)
            {
                return ViewBag.ErrorMessage = ex.Message;
            }
        }

        

        public async Task<dynamic> BuscarCita(long id = 0)
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiCita = api + "/buscar/citaxid" + "?id=" + id;
                HttpResponseMessage message = await client.GetAsync(apiCita);
                if(message.IsSuccessStatusCode)
                {
                    string response = await message.Content.ReadAsStringAsync();
                    return response;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }catch(Exception ex)
            {
                return ViewBag.ErrorMessage = ex.Message;
            }
        }
    }
}
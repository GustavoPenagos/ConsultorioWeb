﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsultorioWeb.Controllers
{
    public class CrudController : Controller
    {
        public readonly string api = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"];
        public async Task<dynamic> EliminarUsuario(long id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiDelete = api + "/eliminar/usuario" + "?id=" + id;
                HttpResponseMessage message = await client.DeleteAsync(apiDelete);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return ViewBag.ErrorMessage = ex.Message;
            }
        }

        public async Task<dynamic> EliminarCita(long id)
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiDelete = api + "/eliminar/cita" + "?id=" + id;
                HttpResponseMessage message = await client.DeleteAsync(apiDelete);

                return RedirectToAction("Citas", "Lista");
            }
            catch (Exception ex)
            {
                return ViewBag.ErrorMessage = ex.Message;
            }
        }

        public async Task<dynamic> EditarUsuario()
        {
            try
            {
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                return ViewBag.ErrorMessage = ex.Message;
            }
        }

        public async Task<dynamic> EliminarFoto(long id, string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiDeleteFoto = api + "/eliminar/foto?id=" + id + "&name=" + name;
                HttpResponseMessage httpResponse = await client.DeleteAsync(apiDeleteFoto);
                if (httpResponse.IsSuccessStatusCode)
                {
                    string response = await httpResponse.Content.ReadAsStringAsync();
                    return RedirectToAction("Fotos", "Lista");
                }
                return RedirectToAction("Index", "Home"); //ALERTA DE ERROR
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
using ConsultorioWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ConsultorioWeb.Controllers
{
    public class TratamientoController : Controller
    {
        public readonly string api = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"];
        StringContent json;

        public ActionResult PlanTratamiento(long id)
        {
            ViewBag.idUsuario = id;
            return View();
        }

        public async Task<dynamic> InsertarTratamiento(List<DateTime> fecha, List<string> diente, List<string> tratamiento
            , List<string> doctor, List<string> firma, string diagnostico, string pronostico, string tratamientos, long idUsuario)
        {
            try
            {
                PlanTratamiento planTratamiento = new PlanTratamiento
                {
                    IdUsuario = idUsuario,
                    Diagnostico = diagnostico,
                    Pronostico = pronostico,
                    Tratamiento = tratamientos,
                    Atencion = DateTime.Now
                };
                HttpClient client = new HttpClient();

                json = new StringContent(JsonConvert.SerializeObject(planTratamiento), Encoding.UTF8, "application/json");
                string apiTrata = api + "/registro/plantratamiento";
                HttpResponseMessage responseUser = await client.PostAsync(apiTrata, json);

                if (responseUser.IsSuccessStatusCode)
                {
                    for (int i = 0; i < fecha.Count; i++)
                    {
                        EstadoTratamiento estado = new EstadoTratamiento
                        {
                            IdUsuario = idUsuario,
                            Fecha = fecha[i],
                            Diente = diente[i],
                            TrataEfectuado = tratamiento[i],
                            Doctor = doctor[i],
                            Firma = firma[i]
                        };
                        json = new StringContent(JsonConvert.SerializeObject(estado), Encoding.UTF8, "application/json");
                        string apiEstado = api + "/registro/estadotratamiento";
                        HttpResponseMessage responseEstado = await client.PostAsync(apiEstado, json);

                        if (responseEstado.IsSuccessStatusCode)
                        {
                        }
                        else
                        {
                            return RedirectToAction("PlanTratamiento", "Registro");
                        }

                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("PlanTratamiento", "Registro");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("PlanTratamiento", "Registro");
            }
        }

    }
}
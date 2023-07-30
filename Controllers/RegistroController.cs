using ConsultorioWeb.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OdontologiaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace ConsultorioWeb.Controllers
{
    public class RegistroController : Controller
    {
        public readonly string api = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"];
        StringContent json;
        
        //Start View

        public async Task<dynamic> RegistroUsuario(long id=0)
        {
            if(id != 0)
            {
                var usuario = await new HomeController().BuscarUsuario(id, "editarUsuario");
                ViewBag.Usuario = JsonConvert.DeserializeObject(usuario);
            }
            var estadoCivil = await EstadoCivil();
            var documentos = await TiposDocumentos();
            var ciudades = await Ciudades();
            var generos = await Generos();

            ViewBag.EstadoCivil = estadoCivil;
            ViewBag.Documentos = documentos;
            ViewBag.Ciudades = ciudades;
            ViewBag.Genero = generos;
            return View();
        }
        public ActionResult RegistroAnamnesis()
        {
            ViewBag.Usuario = TempData["Usuario"];
            ViewBag.idUsuario = TempData["idUsuario"];
            return View();
        }
        public ActionResult RegistroFamiliar()
        {
            return View();
        }
        public ActionResult RegistroEstomatologico()
        {
            return View();
        }
        public async Task<dynamic> RegistroCartaDental()
        {
            var conv = await Convecciones();
            ViewBag.Carta = TempData["carta"];
            ViewBag.Convecciones = conv; 
            ViewBag.idUsuario = TempData["idUsuario"];
            return View();           
        }
        public ActionResult PlanTratamiento()
        {
            //ViewBag.Tratamiento = await Tratamiento();
            ViewBag.idUsuario = TempData["idUsuario"];
            return View();
        }
        public async Task<dynamic> RegistrarCita(long id, string nombre="")
        {
            var cita = await new HomeController().BuscarCita(id);
            if (cita == "[]")
            {
                ViewBag.Cita = null;
                ViewBag.Nombre = nombre;
                ViewBag.Id = id;
                return View();
            }
            else
            {
                ViewBag.Cita = JsonConvert.DeserializeObject(cita);
                return View();
            }
        }
        
        //End Views

        public async Task<dynamic> InsertarCliente(Usuario usuario)
        {
            
            try
            {
                HttpClient client = new HttpClient();
                json = new StringContent(JsonConvert.SerializeObject(usuario));
                string apiUsuario = api + "/api/registro/usuario";
                HttpResponseMessage message = await client.PostAsync(apiUsuario, json);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("RegistroAnamnesis", "Registro");
                }
                else
                {
                    TempData["Usuario"] = usuario.Nombre;
                    TempData["idUsuario"] = usuario.Id_Usuario;
                    ViewBag.Menssage = "Error al guardar datos del paciente";
                    return RedirectToAction("RegistroUsuario", "Registro");
                }

            }
            catch (Exception ex)
            {
                return ViewBag.Error = "Error al guardar datos del paciente" + "\n" + ex.Message;
            }
        }

        public async Task<dynamic> InsertarAnamnesis(Anamnesis anamnesis)
        {
            try
            {
                HttpClient client = new HttpClient();
                json = new StringContent(JsonConvert.SerializeObject(anamnesis), Encoding.UTF8, "application/json");
                string apiAnamnesis = api + "/api/registro/anamnesis";
                HttpResponseMessage message = await client.PostAsync(apiAnamnesis, json);
                if(message.IsSuccessStatusCode)
                {
                    return RedirectToAction("", "Registro");
                }
                else
                {
                    ViewBag.Menssage = "Error al guardar Anamnesis";
                    return RedirectToAction("RegistroAnamnesis", "Registro");
                }
            }
            catch (Exception ex)
            {
                return ViewBag.Errors = "Error al guardar Anamnesis" + "\n" + ex.Message;
            }
        }

        public async Task<dynamic> InsertarFamiliar(Ant_Familiar familiar)
        {
            try
            {
                HttpClient client = new HttpClient();
                json = new StringContent(JsonConvert.SerializeObject(familiar), Encoding.UTF8, "application/json");
                string apiFamiliar= api + "/api/registro/familiar";
                HttpResponseMessage message = await client.PostAsync(apiFamiliar, json);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("", "Registro");
                }
                else
                {
                    ViewBag.Menssage = "Error al guardar el Antecedentes familiares";
                    return RedirectToAction("RegistroAnamnesis", "Registro");
                }
            }
            catch (Exception ex)
            {
                return ViewBag.Errors = "Error al guardar el Antecedentes familiares" + "\n" + ex.Message;
            }
        }

        public async Task<dynamic> InsertarEstomatologico(Estomatologico estomatologico)
        {
            try
            {
                HttpClient client = new HttpClient();
                json = new StringContent(JsonConvert.SerializeObject(estomatologico), Encoding.UTF8, "application/json");
                string apiEstomatologico = api + "/api/registro/estomatologico";
                HttpResponseMessage message = await client.PostAsync(apiEstomatologico, json);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("RegistroCartaDental", "Registro");
                }
                else
                {
                    ViewBag.Menssage = "Error al guardar el examen estomatologico";
                    return RedirectToAction("RegristroEstomatologico", "Registro");
                }
            }
            catch (Exception ex)
            {
                return ViewBag.Errors = "Error al guardar el examen estomatologico" + "\n" + ex.Message;
            }
        }

        public async Task<dynamic> InsertarTratamiento(List<DateTime> fecha, List<string> diente, List<string> tratamiento, List<string> doctor, List<string> firma
            , string diagnostico, string pronostico, string tratamientos, long idUsuario)
        {
            StringContent json;
            try
            {
                PlanTratamiento planTratamiento = new PlanTratamiento
                {
                    Id_Usuario = idUsuario,
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
                            Id_Usuario = idUsuario,
                            Fecha = fecha[i],
                            Diente = diente[i],
                            Trata_Efectuado = tratamiento[i],
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
            catch(Exception ex)
            {
                return RedirectToAction("PlanTratamiento", "Registro");
            }
        }

        public async Task<dynamic> InsertarCartaDentalAdulto(CartaDentalAdulto dentalAdulto)
        {
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(dentalAdulto), Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();

                string apiDental = api + "/registro/dentaladulto";
                HttpResponseMessage message = await client.PostAsync(apiDental, json);
                if (message.IsSuccessStatusCode)
                {
                    TempData["idUsuario"] = dentalAdulto.Id_Usuario;
                    return RedirectToAction("PlanTratamiento", "Registro");
                }
                else
                {
                    return RedirectToAction("RegistroCartaDental", "Registro");
                }
            }
            catch(Exception ex)
            {
                return RedirectToAction("RegistroCartaDental", "Registro");
            }
        }

        public async Task<dynamic> InsertarCartaDentalNino(CartaDentalNino dentalNino)
        {
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(dentalNino), Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();

                string apiDental = api + "/registro/dentalnino";
                HttpResponseMessage message = await client.PostAsync(apiDental, json);
                if (message.IsSuccessStatusCode)
                {
                    TempData["idUsuario"] = dentalNino.Id_Usuario;
                    return RedirectToAction("PlanTratamiento", "Registro");
                }
                else
                {
                    return RedirectToAction("RegistroCartaDental", "Registro");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("RegistroCartaDental", "Registro");
            }
        }

        public async Task<dynamic> InsertarCita(Citas citas)
        {
            try
            {
                var hora = citas.FechaCita.ToShortTimeString();
                if (hora.Equals("12:00 a. m."))
                {
                    return RedirectToAction("Index", "Home");
                }
                var cita = new Citas
                {
                    Id_Usuario = citas.Id_Usuario,
                    FechaCita = Convert.ToDateTime(citas.FechaCita.ToShortDateString()),
                    HoraCita = citas.FechaCita.ToShortTimeString()
                };
                HttpClient client = new HttpClient();
                var json = new StringContent(JsonConvert.SerializeObject(cita), Encoding.UTF8, "application/json");
                string apiCitas = api + "/registro/citas";
                HttpResponseMessage message = await client.PostAsync(apiCitas, json);
                if (message.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("RegistrarCita", "Registro");
            }
            catch (Exception ex)
            {
                return RedirectToAction("RegistrarCita", "Registro");
            }
        }

        public async Task<dynamic> Convecciones()
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiConv = api + "/api/lista/conveccion";
                HttpResponseMessage message = await client.GetAsync(apiConv);
                if(message.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<Convecciones>>(await message.Content.ReadAsStringAsync());
                }
                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public async Task<dynamic> EstadoCivil()
        {
            try
            {
                HttpClient client = new HttpClient();
                string EC = api + "/api/lista/estadocivil";
                HttpResponseMessage message = await client.GetAsync(EC);
                if(message.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<EstadoCivil>>(await message.Content.ReadAsStringAsync());
                }
                return "";
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public async Task<dynamic> TiposDocumentos()
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiTD = api + "/api/lista/tiposdecumentos";
                HttpResponseMessage message = await client.GetAsync(apiTD); 
                if(message.IsSuccessStatusCode) 
                {
                    return JsonConvert.DeserializeObject<List<TipoDocumento>>(await message.Content.ReadAsStringAsync());
                }
                return "";
            }
            catch(Exception ex)
            {
                return "";
            }
        }
        
        public async Task<dynamic> Ciudades()
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiCiud = api + "/api/lista/ciudad";
                HttpResponseMessage message = await client.GetAsync(apiCiud);
                if( message.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<Ciudad>>(await message.Content.ReadAsStringAsync());
                }
                return "";
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public async Task<dynamic> Generos()
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiGenero = api + "/api/lista/genero";
                HttpResponseMessage message = await client.GetAsync(apiGenero);
                if(message.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<Genero>>(await message.Content.ReadAsStringAsync());
                }
                return "";
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        public async Task<dynamic> Tratamiento()
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiTrata = api + "/api/lista/estadotratamiento";
                HttpResponseMessage message = await client.GetAsync(apiTrata);
                if( message.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<List<EstadoTratamiento>>(await message.Content.ReadAsStringAsync());
                }

            }
            catch(Exception ex)
            {

            }
            return "";
        }

    }
}

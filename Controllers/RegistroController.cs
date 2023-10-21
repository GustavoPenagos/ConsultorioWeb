using ConsultorioWeb.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using OdontologiaWeb.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        private string path;

        #region Controller - Views

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
            ViewBag.idUsuario = TempData["id"];
            return View();
        }

        public ActionResult RegistroFamiliar()
        {
            ViewBag.idUsuario = TempData["id"];
            return View();
        }

        public ActionResult RegistroEstomatologico()
        {
            ViewBag.idUsuario = TempData["id"];
            return View();
        }

        public async Task<dynamic> CargarImagen()
        {
            var usuarios = await new HomeController().Index(null, true);
            if(usuarios == null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Usuario = JsonConvert.DeserializeObject(usuarios);
            return View();
        }

        public async Task<dynamic> RegistroCartaDental()
        {
            
            var conv = await Convecciones();
            ViewBag.Carta = Convert.ToInt32(await new HomeController().BuscarUsuario(Convert.ToInt32(TempData["id"]), "registro"));
            ViewBag.Convecciones = conv;
            ViewBag.idUsuario = TempData["id"];
            return View();
        }

        #endregion

        #region Views - Registers 

        public async Task<dynamic> RegistrarCita(long id, string nombre = "")
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

        public async Task<dynamic> InsertarCliente(Usuario usuario)
        {
            HttpClient client = new HttpClient();
            try
            {
                string apiDept = api + "/buscar/departamento";
                HttpResponseMessage message1 = await client.GetAsync(apiDept + "?id=" + usuario.IdCiudad);
                string departamento = await message1.Content.ReadAsStringAsync();
                
                usuario.IdDepartamento = Convert.ToInt32(departamento);

                json = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                string apiUsuario = api + "/registro/usuario";
                HttpResponseMessage message = await client.PostAsync(apiUsuario, json);
                if (message.IsSuccessStatusCode)
                {
                    TempData["nombre"]= usuario.Nombre;
                    TempData["id"]= usuario.IdUsuario;
                    ViewBag.idUsuario = usuario.IdUsuario;
                    return RedirectToAction("RegistroAnamnesis", "Registro");
                }
                else
                {
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
                string apiAnamnesis = api + "/registro/anamnesis";
                HttpResponseMessage message = await client.PostAsync(apiAnamnesis, json);
                if(message.IsSuccessStatusCode)
                {
                    TempData["id"] = anamnesis.IdUsuario;
                    return RedirectToAction("RegistroFamiliar", "Registro");
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
                string apiFamiliar= api + "/registro/familiar";
                HttpResponseMessage message = await client.PostAsync(apiFamiliar, json);
                if (message.IsSuccessStatusCode)
                {
                    TempData["id"] = familiar.IdUsuario;
                    return RedirectToAction("RegistroEstomatologico", "Registro");
                }
                else
                {
                    ViewBag.Menssage = "Error al guardar el Antecedentes familiares";
                    return RedirectToAction("RegistroFamiliar", "Registro");
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
                string apiEstomatologico = api + "/registro/estomatologico";
                HttpResponseMessage message = await client.PostAsync(apiEstomatologico, json);
                if (message.IsSuccessStatusCode)
                {
                    TempData["id"] = estomatologico.IdUsuario;
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["id"] = dentalAdulto.IdUsuario;
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
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["id"] = dentalNino.IdUsuario;
                    return RedirectToAction("RegistroCartaDental", "Registro");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("RegistroCartaDental", "Registro");
            }
        }

        public async Task<dynamic> AgregarFotos(Imagenes imagenes, HttpPostedFileBase imgFile)
        {
            try
            {
                var fileName = Path.GetFileName(imgFile.FileName);
                path = Path.Combine(Server.MapPath("/Content/Imagen/"), fileName);
                string path2 = ("/Content/Imagen/") + fileName;
                imgFile.SaveAs(path);
                imagenes.Imagen = path2.Replace(@"\", "/");

                HttpClient client = new HttpClient();
                json = new StringContent(JsonConvert.SerializeObject(imagenes), Encoding.UTF8, "application/json");
                string jsonImg = api + "/registro/imagen";
                HttpResponseMessage responseMessage = await client.PostAsync(jsonImg, json);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        #endregion

        #region Registers dates

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
                    IdUsuario = citas.IdUsuario,
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

        #endregion

        #region Type - Documents

        public async Task<dynamic> Convecciones()
        {
            try
            {
                HttpClient client = new HttpClient();
                string apiConv = api + "/lista/conveccion";
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
                string EC = api + "/lista/estadocivil";
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
                string apiTD = api + "/lista/tiposdecumentos";
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
                string apiCiud = api + "/lista/ciudad";
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
                string apiGenero = api + "/lista/genero";
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
                string apiTrata = api + "/lista/estadotratamiento";
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

        #endregion

    }
}

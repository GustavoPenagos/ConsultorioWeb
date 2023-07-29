using ConsultorioWeb.Models;
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

namespace ConsultorioWeb.Controllers
{
    public class RegistroController : Controller
    {
        public readonly string api = System.Configuration.ConfigurationManager.AppSettings["UrlAPI"];
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
        public async Task<dynamic> RegistroCartaDental()
        {
            var conv = await Convecciones();
            ViewBag.Carta = TempData["carta"];
            ViewBag.Convecciones = conv; 
            ViewBag.idUsuario = TempData["idUsuario"];
            return View();           
        }
        public async Task<dynamic> PlanTratamiento()
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> InsertarCliente(string nombre, string apellido, int tp_doc, long id_usuario, int civil, DateTime f_nacido,int edad, int sexo, string ocupacion,
            int ciudad, string asrgura, string direccion, string telefono, string oficina, string acudiente, string referido
            , string MTV_consulta, string HA_actual,int sinusitis, int org_snt, int infecciones, int hepatitis, int trans_gastico, int cardiopatias, int fieb_reumatica
            , int trat_medico, int enf_respira, int hipertension, int alt_coagula, int trans_neumolo, int cancer, int diabetes, int ten_arterial, string otros, int emabrazo, int meses, int lactancia, int frec_cepilla, int ceda, string observacion
            , int labios, int lengua, int frenillos, int encias, int musculo, int maxilares, int paladar, int piso_boca, int glan_saliva, int carrillos, int orofaringe
            , int carta)
        {
            StringContent json;
            try
            {
                HttpClient httpClient = new HttpClient();
                string apiDept = api + "/api/buscar/departamento";
                HttpResponseMessage message = await httpClient.GetAsync(apiDept + "?id=" + ciudad);
                string departamento = await message.Content.ReadAsStringAsync();
                
                //
                Usuario usuario = new Usuario
                {
                    Id_Usuario=id_usuario,
                    Id_Documento = tp_doc,
                    Nombre = nombre,
                    Apellido=apellido,
                    Edad=edad,
                    Fecha_Nacido=f_nacido,
                    Estado_Civil=civil,
                    Ocupacion=ocupacion,
                    Aseguradora=asrgura,
                    Direccion=direccion,
                    Telefono = telefono,
                    Id_Genero=sexo,
                    Id_Ciudad=ciudad,
                    Id_Departamento = Convert.ToInt32(departamento),
                    Oficina = oficina,
                    Nombre_Acudiente=acudiente,
                    Referido=referido,
                    Observaciones=observacion,  
                    Atencion = DateTime.Now
                };
                json = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                string apiUser = api + "/registro/usuario";

                
                HttpResponseMessage responseUser = await httpClient.PostAsync(apiUser, json);
                if (responseUser.IsSuccessStatusCode)
                {
                    Anamnesis anamnesis = new Anamnesis
                    {
                        Id_Usuario = id_usuario,
                        Motivo_Consulta = MTV_consulta,
                        Emf_Actual = HA_actual,
                    };

                    json = new StringContent(JsonConvert.SerializeObject(anamnesis), Encoding.UTF8, "application/json");
                    string apiAnam = api + "/registro/anamnesis";
                    HttpResponseMessage responseAnam = await httpClient.PostAsync(apiAnam, json);
                    if(responseAnam.IsSuccessStatusCode) 
                    {
                        Ant_Familiar familiar = new Ant_Familiar
                        {
                            Id_Usuario = id_usuario,
                            Cancer = cancer,
                            Sinusitis = sinusitis,
                            Organos_Sentidos = org_snt,
                            Diabetes = diabetes,
                            Infecciones = infecciones,
                            Hepatitis = hepatitis,
                            Trans_Gastricos = trans_gastico,
                            Cardiopatias = cardiopatias,
                            Fieb_Reumatica = fieb_reumatica,
                            Trata_Medico = trat_medico,
                            Enf_Respiratoria = enf_respira,
                            Hipertension = hipertension,
                            Alt_Coagulatorias = alt_coagula,
                            Trans_Neumologico = trans_neumolo,
                            Ten_Arterial = ten_arterial,
                            Otros = otros,
                            Embarazo = emabrazo,
                            Meses = meses,
                            Lactancia = lactancia,
                            Fre_Cepillado = frec_cepilla,
                            Ceda_Dental = ceda,
                            Observaciones = observacion,
                            Atencion = DateTime.Now
                        };

                        json = new StringContent(JsonConvert.SerializeObject(familiar), Encoding.UTF8, "application/json");
                        string apiFam = api + "/registro/familiar";
                        HttpResponseMessage responseFam = await httpClient.PostAsync(apiFam, json);
                        if (responseFam.IsSuccessStatusCode)
                        {
                            Estomatologico estomatologico = new Estomatologico
                            {
                                Id_Usuario = id_usuario,
                                Labios = labios,
                                Encias = encias,
                                Paladar = paladar,
                                Carrillos = carrillos,
                                Lengua = lengua,
                                Musculos = musculo,
                                Piso_Boca = piso_boca,
                                Orofarige = orofaringe,
                                Frenillos = frenillos,
                                Maxilares = maxilares,
                                Glan_Salivales = glan_saliva,
                                Atencion = DateTime.Now
                            };

                            json = new StringContent(JsonConvert.SerializeObject(familiar), Encoding.UTF8, "application/json");
                            string apiEst = api + "/registro/estomatologico";
                            HttpResponseMessage responseEst = await httpClient.PostAsync(apiEst, json);
                            if (responseEst.IsSuccessStatusCode)
                            {
                                TempData["carta"] = carta;
                                TempData["idUsuario"] = id_usuario;
                                return RedirectToAction("RegistroCartaDental", "Registro");
                            }
                            
                        }
                        
                    }
                    
                }
                return RedirectToAction("RegistroUsuario", "Registro", usuario);

            }
            catch (Exception ex)
            {
                return RedirectToAction("RegistroUsuario","Registro");
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

using ConsultorioWeb.Models;
using Newtonsoft.Json;
using OdontologiaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public async Task<dynamic> RegistroUsuario()
        {
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
        public ActionResult RegistroCartaDental(int carta = 0)
        {
            var conv = Convecciones();
            ViewBag.Carta = carta;
            ViewBag.Conveccion = conv; 
            return View();           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<dynamic> InsertarCliente(string nombre, string apellido, int tp_doc, string id_usuario, int civil, DateTime f_nacido,int edad, int sexo, string ocupacion,
            int ciudad, string asrgura, string direccion, string telefono, string oficina, string acudiente, string referido
        /**/, string MTV_consulta, string HA_actual,int sinusitis, int org_snt, int infecciones, int hepatitis, int trans_gastico, int cardiopatias, int fieb_reumatica
            , int trat_medico, int enf_respira, int hipertension, int alt_coagula, int trans_neumolo
        ,/**/ int cancer, int diabetes, int ten_arterial, string otros, int emabrazo, int meses, int lactancia, int frec_cepilla, int ceda, string observacion
        ,/**/ int labios, int lengua, int frenillos, int encias, int musculo, int maxilares, int paladar, int piso_boca, int glan_saliva, int carrillos, int orofaringe
        ,/**/ int carta=1)
        {
            StringContent json;
            try
            {
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
                    Oficina =oficina,
                    Nombre_Acudiente=acudiente,
                    Referido=referido,
                    Observaciones=observacion,  
                    Atencion = DateTime.Now
                };
                json = new StringContent(JsonConvert.SerializeObject(usuario), Encoding.UTF8, "application/json");
                string apiUser = api + "/registro/usuario";

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage responseUser = await httpClient.PostAsync(apiUser, json);
                if (responseUser.IsSuccessStatusCode)
                {
                    Anamnesis anamnesis = new Anamnesis
                    {
                        Id_Usuario = id_usuario,
                        Motivo_Consulta = MTV_consulta,
                        Emf_Actual = HA_actual
                        
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
                            Observaciones = observacion
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
                                Glan_Salivales = glan_saliva
                            };

                            json = new StringContent(JsonConvert.SerializeObject(familiar), Encoding.UTF8, "application/json");
                            string apiEst = api + "/registro/estomatologico";
                            HttpResponseMessage responseEst = await httpClient.PostAsync(apiEst, json);
                            if (responseEst.IsSuccessStatusCode)
                            {

                            }
                            else
                            {
                                return RedirectToAction("RegistroUsuario", "Registro", usuario);
                            }
                        }
                        else
                        {
                            return RedirectToAction("RegistroUsuario", "Registro", usuario);
                        }
                    }
                    else
                    {
                        return RedirectToAction("RegistroUsuario", "Registro", usuario);
                    }
                }
                else
                {
                    return RedirectToAction("RegistroUsuario", "Registro", usuario);
                }

                return RedirectToAction("RegistroCartaDental", "Registro", carta);
            }
            catch (Exception ex)
            {
                return RedirectToAction("RegistroUsuario","Registro");
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
                    string response = await message.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Convecciones>>(response);
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
                    string response = await message.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<EstadoCivil>>(response);
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
                    string response = await message.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<TipoDocumento>>(response);
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
                    string response = await message.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Ciudad>>(response);
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
                    string response = await message.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Genero>>(response);
                }
                return "";
            }
            catch(Exception ex)
            {
                return "";
            }
        }
    }
}

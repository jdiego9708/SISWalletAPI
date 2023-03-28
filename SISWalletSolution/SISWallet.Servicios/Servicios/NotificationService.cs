using Microsoft.AspNetCore.SignalR.Client;
using SISWallet.Servicios.Interfaces;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System.Data;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Modelos;
using Google.Apis.Util;
using SISWallet.Entidades.ModelosBindeo;
using System.Reflection;
using SISWallet.Entidades.Helpers;
using SISWallet.Entidades.Helpers.Interfaces;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionChatGPT;

namespace SISWallet.Servicios.Servicios
{
    public class NotificationService : INotificationService
    {
        public IUsuariosDac IUsuariosDac { get; set; }
        public IChatGPTHelper ChatGPTHelper { get; set; }
        public NotificationService(IUsuariosDac IUsuariosDac,
            IChatGPTHelper ChatGPTHelper)
        {
            this.ChatGPTHelper = ChatGPTHelper;
            this.IUsuariosDac = IUsuariosDac;
        }
        public async Task<RespuestaServicioModel> BuscarRespuestaChatGPT(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = await this.ChatGPTHelper.GenerateResponseAsync(busqueda.Texto_busqueda1);

                if (string.IsNullOrEmpty(result))
                    throw new Exception("Error generando la respuesta en el servicio de chatgpt");

                ChatGPTResponseModel response = new()
                {
                    Fecha = DateTime.Now,
                    Hora = DateTime.Now.TimeOfDay,
                    Mensaje = result,
                };

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(response);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel InsertarUsuariosFirebase(Usuarios_firebase usuario_firebase)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                string rpta = this.IUsuariosDac.InsertarUsuarioFirebase(usuario_firebase).Result;

                if (!rpta.Equals("OK"))
                    throw new Exception(rpta);

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(usuario_firebase);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarUsuariosFirebase(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.IUsuariosDac.BuscarUsuariosFirebase(busqueda.Tipo_busqueda, busqueda.Texto_busqueda1).Result;

                if (result.dt == null)
                {
                    if (result.rpta.Equals("OK"))
                    {
                        throw new Exception("Sin resultados");
                    }
                    else
                        throw new Exception("Error buscando los usuarios resultados");

                }

                if (result.dt.Rows.Count < 1)
                    throw new Exception("No se encontraron usuarios");

                List<Usuarios_firebase> usuarios = (from DataRow row in result.dt.Rows
                                                    select new Usuarios_firebase(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(usuarios);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public async Task SendNotification(string token)
        {
            try
            {
                Assembly GetAssemblyByName(string name)
                {
                    Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().
                           SingleOrDefault(assembly => assembly.GetName().Name == name);

                    if (assembly == null)
                        return null;

                    return assembly;
                }

                var a = GetAssemblyByName("SISWallet.API");

                using var stream = a.GetManifestResourceStream("SISWallet.API.siswalletfirebasecredentials.json");

                GoogleCredential credential = GoogleCredential.FromStream(stream);
                //Inicializa FirebaseApp con la credencial de administrador
                FirebaseApp app = FirebaseApp.Create(new AppOptions
                {
                    Credential = credential
                });

                // Crea el cliente de mensajería de FCM
                var messaging = FirebaseMessaging.DefaultInstance;

                // Construye la notificación push
                var message = new Message
                {
                    Token = token, // Agrega aquí el token de registro del dispositivo de destino
                    Notification = new Notification
                    {
                        Title = "SISWallet - SOLICITUD",
                        Body = "Tiene una solicitud de un trabajador"
                    },
                };

                // Envía la notificación push al dispositivo de destino
                var response = await messaging.SendAsync(message);

                if (response == null)
                    throw new Exception("Error enviando la notificación");
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}

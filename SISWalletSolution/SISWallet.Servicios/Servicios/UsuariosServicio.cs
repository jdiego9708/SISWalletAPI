using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SISWallet.AccesoDatos;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.Helpers;
using SISWallet.Entidades.Helpers.Interfaces;
using SISWallet.Entidades.Modelos;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;
using SISWallet.Servicios.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SISWallet.Servicios.Servicios
{
    public class UsuariosServicio : IUsuariosServicio
    {
        #region CONSTRUCTOR
        public JwtModel JwtConfiguration { get; set; }
        public IUsuariosDac UsuariosDac { get; set; }
        public IDireccion_clientesDac Direccion_clientesDac { get; set; }
        public IVentasDac VentasDac { get; set; }
        public IAgendamiento_cobrosDac Agendamiento_cobrosDac { get; set; }
        public IBlobStorageService IBlobStorageService { get; set; }
        public IRutas_archivosDac IRutas_archivosDac { get; set; }
        public ITurnosDac ITurnosDac { get; set; }
        public ISolicitudesDac ISolicitudesDac { get; set; }
        public INotificationService INotificationService { get; set; }  
        public UsuariosServicio(IConfiguration IConfiguration,
            IUsuariosDac UsuariosDac,
            IDireccion_clientesDac Direccion_clientesDac,
            IVentasDac VentasDac,
            IAgendamiento_cobrosDac Agendamiento_cobrosDac,
            IBlobStorageService IBlobStorageService,
            IRutas_archivosDac IRutas_archivosDac,
            ITurnosDac ITurnosDac,
            ISolicitudesDac ISolicitudesDac,
            INotificationService INotificationService)
        {
            this.UsuariosDac = UsuariosDac;
            this.Direccion_clientesDac = Direccion_clientesDac;
            this.VentasDac = VentasDac;
            this.Agendamiento_cobrosDac = Agendamiento_cobrosDac;
            this.IBlobStorageService = IBlobStorageService;
            this.IRutas_archivosDac = IRutas_archivosDac;
            this.ITurnosDac = ITurnosDac;
            this.ISolicitudesDac = ISolicitudesDac;
            this.INotificationService = INotificationService;

            var settings = IConfiguration.GetSection("Jwt");
            this.JwtConfiguration = settings.Get<JwtModel>();
        }
        #endregion

        #region MÉTODOS
        //private string GetTokenLogin(Usuarios user, DateTime dateExpired)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var llave = Encoding.UTF8.GetBytes(this.JwtConfiguration.Secreto);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(
        //                new Claim[]
        //                {
        //                    new Claim(ClaimTypes.NameIdentifier, user.Id_usuario.ToString()),
        //                    new Claim(ClaimTypes.Name, user.NombreCompleto),
        //                }
        //            ),
        //        Expires = dateExpired,
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
        public string GenerateJwtToken(Usuarios user, int expireMinutes)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.NombreCompleto),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.JwtConfiguration.Secreto));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: this.JwtConfiguration.Issuer,
                audience: this.JwtConfiguration.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RespuestaServicioModel ProcesarLogin(LoginModel login)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var (dtUsuarios, rpta) = this.UsuariosDac.BuscarUsuarios("PIN", login.Clave).Result;

                if (dtUsuarios == null)
                    throw new Exception($"No se encontró el usuario | {rpta}");

                if (dtUsuarios.Rows.Count < 1)
                    throw new Exception($"No se encontró el usuario | {rpta}");

                List<Cobros> cobros = new();
                List<Usuarios> usuarios = new();
                List<Credenciales> credenciales = new();
                int id_cobro_default = 0;

                foreach (DataRow rowUsuario in dtUsuarios.Rows)
                {
                    id_cobro_default = ConvertValueHelper.ConvertirNumero(rowUsuario["Id_cobro_default"]);

                    Cobros cobro = new(rowUsuario);
                    Usuarios usuario = new(rowUsuario);
                    Credenciales credencial = new(rowUsuario);

                    cobros.Add(cobro);
                    usuarios.Add(usuario);
                    credenciales.Add(credencial);
                }

                Credenciales credencialesDefault = credenciales.Where(x => x.Password == login.Clave).FirstOrDefault();

                if (credencialesDefault == null)
                    throw new Exception("Error obteniendo las credenciales del usuario");

                Usuarios usuarioDefault = usuarios.Where(x => x.Identificacion == credencialesDefault.Usuario.Identificacion).FirstOrDefault();

                if (usuarioDefault == null)
                    throw new Exception("Error obteniendo el usuario");

                Cobros cobroDefault = cobros.Where(x => x.Id_cobro == id_cobro_default).FirstOrDefault();

                if (cobroDefault == null)
                    throw new Exception("Error obteniendo el cobro predeterminado del usuario");

                var resultReglas =
                    this.UsuariosDac.BuscarUsuarios("REGLAS ID COBRO", cobroDefault.Id_cobro.ToString()).Result;

                if (resultReglas.dtUsuarios == null)
                    throw new Exception("No se encontraron las reglas del cobro");

                List<Reglas> reglas = (from DataRow row in resultReglas.dtUsuarios.Rows
                                       select new Reglas(row)).ToList();

                if (reglas == null)
                    throw new Exception("No se encontraron las reglas del cobro");

                if (reglas.Count < 1)
                    throw new Exception("No se encontraron las reglas del cobro");

                var resultReglasUsuario =
                    this.UsuariosDac.BuscarUsuarios("REGLAS ID USUARIO", usuarioDefault.Id_usuario.ToString()).Result;

                if (resultReglasUsuario.dtUsuarios == null)
                    throw new Exception("No se encontraron las reglas del usuario");

                List<Usuarios_reglas> reglasUsuarios = (from DataRow row in resultReglasUsuario.dtUsuarios.Rows
                                                        select new Usuarios_reglas(row)).ToList();

                if (reglasUsuarios == null)
                    throw new Exception("No se encontraron las reglas del usuario");

                if (reglasUsuarios.Count < 1)
                    throw new Exception("No se encontraron las reglas del usuario");

                var resultTipoSolicitudes = this.ISolicitudesDac.BuscarTipoSolicitudes(new()
                {
                    Tipo_busqueda = "COMPLETO",
                    Texto_busqueda1 = string.Empty,
                }).Result;

                if (resultTipoSolicitudes.dt == null)
                    throw new Exception("No se encontraron los tipos de solicitudes");

                List<Tipo_solicitudes> tipossolicitudes = (from DataRow row in resultTipoSolicitudes.dt.Rows
                                                           select new Tipo_solicitudes(row)).ToList();

                if (tipossolicitudes == null)
                    throw new Exception("No se encontraron los tipos de solicitudes");

                DateTime fechaLogin = ConvertValueHelper.ConvertirFecha(login.Fecha);
                TimeSpan horaLogin = ConvertValueHelper.ConvertirHora(login.Hora);

                DateTime fechaCompleta = fechaLogin.Add(horaLogin);
                DateTime fechaExpiredToken = fechaCompleta.AddDays(+1);

                string token = this.GenerateJwtToken(usuarioDefault, 120);

                if (!string.IsNullOrEmpty(login.TokenFirebase))
                    Task.Run(async () => await this.INotificationService.SendNotification(login.TokenFirebase));

                Turnos turno = null;

                if (usuarioDefault.Tipo_usuario.Equals("COBRADOR"))
                {
                    //Buscar turno
                    var resultTurno = this.ITurnosDac.BuscarTurnos(new BusquedaBindingModel()
                    {
                        Tipo_busqueda = "ULTIMO TURNO ID COBRO",
                        Texto_busqueda1 = cobroDefault.Id_cobro.ToString()
                    }).Result;

                    if (resultTurno.dt == null || resultTurno.dt.Rows.Count < 1)
                    {
                        //Si entra por acá significa que es el primer turno del usuario
                        turno = new()
                        {
                            Id_cobrador = usuarioDefault.Id_usuario,
                            Id_cobro = cobroDefault.Id_cobro,
                            Fecha_inicio_turno = fechaLogin,
                            Fecha_fin_turno = fechaLogin,
                            Hora_inicio_turno = horaLogin,
                            Hora_fin_turno = horaLogin,
                            Estado_turno = "ABIERTO",
                        };

                        string rptaturno = this.ITurnosDac.InsertarTurno(turno).Result;
                        if (!rptaturno.Equals("OK"))
                            throw new Exception("No se pudo insertar el turno inicial");
                    }
                    else
                    {
                        //Comprobar la fecha
                        turno = new(resultTurno.dt.Rows[0]);

                        //urno.Cobrador 

                        if (turno.Fecha_inicio_turno.ToString("yyyy-MM-dd") == login.Fecha)
                        {
                            if (!usuarioDefault.Tipo_usuario.Equals("ADMINISTRADOR"))
                                if (turno.Estado_turno.Equals("CERRADO"))
                                    throw new Exception("Turno cerrado no puede acceder a el");
                        }
                        else
                        {



                            if (!usuarioDefault.Tipo_usuario.Equals("ADMINISTRADOR"))
                            {
                                if (turno.Fecha_inicio_turno < fechaLogin)
                                {
                                    var resultSyncClientes = this.ITurnosDac.SincronizarClientes(cobroDefault.Id_cobro,
                                    usuarioDefault.Id_usuario, fechaLogin).Result;

                                    if (resultSyncClientes.dt == null)
                                        throw new Exception("No se pudieron sincronizar los clientes");

                                    if (resultSyncClientes.dt.Rows.Count < 1)
                                        throw new Exception("No se pudieron sincronizar los clientes");

                                    decimal valor_inicial = turno.Recaudo_real;
                                    //Si entra por acá significa que se debe crear el turno actual
                                    turno = new()
                                    {
                                        Id_cobrador = usuarioDefault.Id_usuario,
                                        Id_cobro = cobroDefault.Id_cobro,
                                        Fecha_inicio_turno = fechaLogin,
                                        Fecha_fin_turno = fechaLogin,
                                        Hora_inicio_turno = login.Hora,
                                        Hora_fin_turno = login.Hora,
                                        Valor_inicial = valor_inicial,
                                        Estado_turno = "ABIERTO",
                                    };

                                    string rptaturno = this.ITurnosDac.InsertarTurno(turno).Result;
                                    if (!rptaturno.Equals("OK"))
                                        throw new Exception("No se pudo insertar el turno actual");

                                    var resultEstadisticas = this.ITurnosDac.EstadisticasDiarias(turno.Id_turno,
                                        turno.Fecha_inicio_turno).Result;

                                    if (resultEstadisticas.dt == null)
                                        throw new Exception("Error obteniendo las estadísticas del turno actual");

                                    turno = new Turnos(resultEstadisticas.dt.Rows[0]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //Buscar turno
                    var resultTurno = this.ITurnosDac.BuscarTurnos(new BusquedaBindingModel()
                    {
                        Tipo_busqueda = "ULTIMO TURNO ID COBRO",
                        Texto_busqueda1 = cobroDefault.Id_cobro.ToString()
                    }).Result;

                    if (resultTurno.dt == null || resultTurno.dt.Rows.Count < 1)
                    {
                        //Si entra por acá significa que es el primer turno del usuario
                        turno = new()
                        {
                            Id_cobrador = usuarioDefault.Id_usuario,
                            Id_cobro = cobroDefault.Id_cobro,
                            Fecha_inicio_turno = fechaLogin,
                            Fecha_fin_turno = fechaLogin,
                            Hora_inicio_turno = horaLogin,
                            Hora_fin_turno = horaLogin,
                            Estado_turno = "ABIERTO",
                        };

                        string rptaturno = this.ITurnosDac.InsertarTurno(turno).Result;
                        if (!rptaturno.Equals("OK"))
                            throw new Exception("No se pudo insertar el turno inicial");
                    }
                    else
                        turno = new(resultTurno.dt.Rows[0]);
                }

                LoginDataModel loginData = new()
                {
                    Credenciales = credencialesDefault,
                    Turno = turno,
                    CobroDefault = cobroDefault,
                    Cobros = cobros,
                    TipoProductoDefault = cobroDefault.Tipo_producto,
                    ZonaDefault = cobroDefault.Zona,
                    CiudadDefault = cobroDefault.Zona.Ciudad,
                    PaisDefault = cobroDefault.Zona.Ciudad.Pais,
                    Reglas = reglas,
                    Usuarios_reglas = reglasUsuarios,
                    TokenAccess = token,
                    Tipo_solicitudes = tipossolicitudes,
                };

                if (loginData == null)
                    throw new Exception("Error obteniendo los datos de inicio de sesión");

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(loginData);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel NuevoCliente(ClienteBindingModel cliente)
        {

            RespuestaServicioModel respuesta = new();
            try
            {
                //Insertar Usuario
                string rpta = this.UsuariosDac.InsertarUsuario(cliente.Usuario).Result;
                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando el usuario, detalles: {rpta}");

                if (cliente.Imagenes != null)
                {
                    if (cliente.Imagenes.Count > 0)
                    {
                        int contador = 1;
                        foreach (string imagenbase64 in cliente.Imagenes)
                        {
                            byte[] imagenByte = Convert.FromBase64String(imagenbase64);

                            Stream stream = new MemoryStream(imagenByte);

                            BlobResponse response = this.IBlobStorageService.SubirArchivoContainerBlobStorage(stream,
                                $"{cliente.Usuario.Id_usuario}Imagen{contador}.png", "imagenesusuario");

                            if (!response.IsSuccess)
                                throw new Exception("Error guardando las imagenes en el blob");

                            string uri = Convert.ToString(response.Message);

                            if (string.IsNullOrEmpty(uri))
                                throw new Exception("Error con la URL devuelta");

                            DirectoryInfo info = new(uri);

                            Rutas_archivos ruta = new()
                            {
                                Id_usuario = cliente.Usuario.Id_usuario,
                                Tipo_archivo = "IMAGEN CLIENTE",
                                Fecha_archivo = cliente.Usuario.Fecha_ingreso,
                                Hora_archivo = DateTime.Now.TimeOfDay,
                                Nombre_archivo = Path.GetFileName(info.FullName),
                                Ruta_archivo = uri,
                                Extension_archivo = info.Extension,
                            };

                            rpta = this.IRutas_archivosDac.InsertarRuta(ruta).Result;
                            if (!rpta.Equals("OK"))
                                throw new Exception("Error guardando las imágenes en la BD");

                            contador++;
                        }
                    }
                }

                int id_usuario = cliente.Usuario.Id_usuario;
                cliente.Direccion_cliente.Id_usuario = id_usuario;

                //Insertar Dirección del cliente
                rpta = this.Direccion_clientesDac.InsertarDireccion(cliente.Direccion_cliente).Result;
                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando la dirección, detalles: {rpta}");

                int id_direccion = cliente.Direccion_cliente.Id_direccion;

                cliente.Venta.Id_cliente = id_usuario;
                cliente.Venta.Id_direccion = id_direccion;

                if (cliente.Venta.Interes_venta == 0)
                    cliente.Venta.Interes_venta = 0.20m;

                decimal total_venta = (cliente.Venta.Interes_venta * cliente.Venta.Valor_venta) + cliente.Venta.Valor_venta;
                cliente.Venta.Total_venta = total_venta;

                decimal valor_cuota = total_venta / cliente.Venta.Numero_cuotas;
                cliente.Venta.Valor_cuota = valor_cuota;

                cliente.Agendamiento.Valor_cobro = valor_cuota;

                cliente.Agendamiento.Valor_pagado = 0;

                cliente.Agendamiento.Saldo_restante = total_venta;



                //Insertar venta
                rpta = this.VentasDac.InsertarVentas(cliente.Venta).Result;
                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando la venta, detalles: {rpta}");

                int id_venta = cliente.Venta.Id_venta;
                cliente.Agendamiento.Id_venta = id_venta;

                //Insertar usuario venta
                rpta =
                    this.UsuariosDac.InsertarUsuarioVenta(new Usuarios_ventas
                    {
                        Id_usuario = id_usuario,
                        Id_venta = id_venta,
                    }).Result;
                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando el usuario venta, detalles: {rpta}");

                cliente.Agendamiento.Saldo_restante = total_venta;
                cliente.Agendamiento.Valor_pagado = 0;
                cliente.Agendamiento.Valor_cobro = valor_cuota;
                cliente.Agendamiento.Observaciones_cobro = cliente.Agendamiento.Observaciones_cobro ?? "";
                cliente.Agendamiento.Estado_cobro = cliente.Agendamiento.Estado_cobro ?? "PENDIENTE";
                //Insertar Agendamiento
                rpta = this.Agendamiento_cobrosDac.InsertarAgendamiento(cliente.Agendamiento).Result;
                if (!rpta.Equals("OK"))
                    throw new Exception($"Hubo un error insertando el agendamiento, detalles: {rpta}");

                int id_agendamiento = cliente.Agendamiento.Id_agendamiento;

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(cliente.Agendamiento);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel BuscarArchivos(BusquedaBindingModel busqueda)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.IRutas_archivosDac.BuscarRutas(busqueda.Tipo_busqueda, busqueda.Texto_busqueda1).Result;

                if (result.dtRutas == null)
                    throw new Exception("Error al buscar las rutas");

                if (result.dtRutas.Rows.Count < 1)
                    throw new Exception("No se encontraron rutas");

                List<Rutas_archivos> rutas = (from DataRow row in result.dtRutas.Rows
                                              select new Rutas_archivos(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(rutas);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel InsertarArchivos(List<Rutas_archivos> rutas)
        {

            RespuestaServicioModel respuesta = new();
            try
            {
                if (rutas == null)
                    throw new Exception("Archivos vacíos");

                if (rutas.Count < 1)
                    throw new Exception("Archivos vacíos");

                int contador = 1;

                foreach (Rutas_archivos r in rutas)
                {
                    string stringBase64 = r.Ruta_archivo;

                    byte[] imagenByte = Convert.FromBase64String(stringBase64);

                    Stream stream = new MemoryStream(imagenByte);

                    BlobResponse response = this.IBlobStorageService.SubirArchivoContainerBlobStorage(stream,
                        $"{r.Id_usuario}Imagen{contador}.png", "imagenesusuario");

                    if (!response.IsSuccess)
                        throw new Exception("Error guardando las imagenes en el blob");

                    string uri = Convert.ToString(response.Message);

                    if (string.IsNullOrEmpty(uri))
                        throw new Exception("Error con la URL devuelta");

                    DirectoryInfo info = new(uri);

                    r.Nombre_archivo = Path.GetFileName(info.FullName);
                    r.Ruta_archivo = uri;
                    r.Extension_archivo = info.Extension;

                    string rpta = this.IRutas_archivosDac.InsertarRuta(r).Result;
                    if (!rpta.Equals("OK"))
                        throw new Exception("Error guardando las imágenes en la BD");

                    contador++;
                }

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(rutas);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        public RespuestaServicioModel SincronizarClientes(BusquedaBindingModel busqueda)
        {

            RespuestaServicioModel respuesta = new();
            try
            {
                if (!int.TryParse(busqueda.Texto_busqueda1, out int id_cobro))
                    throw new Exception("El Texto_busqueda1 debe ser el Id_cobro");

                if (!DateTime.TryParse(busqueda.Texto_busqueda2, out DateTime fecha_login))
                    throw new Exception("El Texto_busqueda2 debe ser el fecha_login");

                var resultSyncClientes = this.ITurnosDac.SincronizarClientes(id_cobro, 0, fecha_login).Result;

                if (resultSyncClientes.dt == null)
                    throw new Exception("No se pudieron sincronizar los clientes");

                if (resultSyncClientes.dt.Rows.Count < 1)
                    throw new Exception("No se pudieron sincronizar los clientes");

                List<Agendamiento_cobros> agendamiento = (from DataRow row in resultSyncClientes.dt.Rows
                                                          select new Agendamiento_cobros(row)).ToList();

                respuesta.Correcto = true;
                respuesta.Respuesta = JsonConvert.SerializeObject(agendamiento);
                return respuesta;
            }
            catch (Exception ex)
            {
                respuesta.Correcto = false;
                respuesta.Respuesta = ex.Message;
                return respuesta;
            }
        }
        #endregion
    }
}

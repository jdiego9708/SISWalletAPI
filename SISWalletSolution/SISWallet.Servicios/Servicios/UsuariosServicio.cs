using Newtonsoft.Json;
using SISWallet.AccesoDatos.Interfaces;
using SISWallet.Entidades.ModelosBindeo;
using SISWallet.Entidades.ModelosBindeo.ModelosConfiguracion.ConfiguracionSISWallet;
using SISWallet.Entidades.Models;
using SISWallet.Servicios.Interfaces;
using System.Data;

namespace SISWallet.Servicios.Servicios
{
    public class UsuariosServicio : IUsuariosServicio
    {
        #region CONSTRUCTOR
        public IUsuariosDac UsuariosDac { get; set; }
        public IDireccion_clientesDac Direccion_clientesDac { get; set; }
        public IVentasDac VentasDac { get; set; }
        public IAgendamiento_cobrosDac Agendamiento_cobrosDac { get; set; }
        public UsuariosServicio(IUsuariosDac UsuariosDac,
            IDireccion_clientesDac Direccion_clientesDac,
            IVentasDac VentasDac,
            IAgendamiento_cobrosDac Agendamiento_cobrosDac)
        {
            this.UsuariosDac = UsuariosDac;
            this.Direccion_clientesDac = Direccion_clientesDac;
            this.VentasDac = VentasDac;
            this.Agendamiento_cobrosDac = Agendamiento_cobrosDac;
        }
        #endregion

        #region MÉTODOS
        public RespuestaServicioModel ProcesarLogin(LoginModel login)
        {
            RespuestaServicioModel respuesta = new();
            try
            {
                var result = this.UsuariosDac.Login(login.Usuario, login.Clave, login.Fecha).Result;

                string rpta = result.rpta;
                LoginDataModel loginData = result.loginData;

                if (loginData == null)
                    throw new Exception("Error obteniendo los datos de inicio de sesión");

                if (!rpta.Equals("OK"))
                    throw new Exception(rpta);

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
        #endregion
    }
}

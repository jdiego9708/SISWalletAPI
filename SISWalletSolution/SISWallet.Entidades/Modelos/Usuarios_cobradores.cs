namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Usuarios_cobradores
    {
        public Usuarios_cobradores()
        {

        }

        public Usuarios_cobradores(DataTable dt)
        {
            try
            {
                if (dt != null)
                {
                    this.Id_jefe_ruta = ConvertValueHelper.ConvertirNumero(dt.Rows[0]["Id_jefe_ruta"]);
                    this.Jefe_ruta = new Usuarios
                    {
                        Id_usuario = ConvertValueHelper.ConvertirNumero(dt.Rows[0]["Id_usuario_jefe_ruta"]),
                        Fecha_ingreso = ConvertValueHelper.ConvertirFecha(dt.Rows[0]["Fecha_ingreso_jefe_ruta"]),
                        Alias = ConvertValueHelper.ConvertirCadena(dt.Rows[0]["Alias_jefe_ruta"]),
                        Nombres = ConvertValueHelper.ConvertirCadena(dt.Rows[0]["Nombres_jefe_ruta"]),
                        Apellidos = ConvertValueHelper.ConvertirCadena(dt.Rows[0]["Apellidos_jefe_ruta"]),
                        Identificacion = ConvertValueHelper.ConvertirCadena(dt.Rows[0]["Identificacion_jefe_ruta"]),
                        Celular = ConvertValueHelper.ConvertirCadena(dt.Rows[0]["Celular_jefe_ruta"]),
                        Email = ConvertValueHelper.ConvertirCadena(dt.Rows[0]["Email_jefe_ruta"]),
                        Tipo_usuario = ConvertValueHelper.ConvertirCadena(dt.Rows[0]["Tipo_usuario_jefe_ruta"]),
                        Estado_usuario = ConvertValueHelper.ConvertirCadena(dt.Rows[0]["Estado_usuario_jefe_ruta"]),
                    };

                    this.Cobradores = new List<Usuarios_cobradores>();

                    foreach (DataRow row in dt.Rows)
                    {
                        this.Cobradores.Add(new Usuarios_cobradores(row));
                    };
                }
            }
            catch (Exception)
            {

            }
        }

        public Usuarios_cobradores(DataRow row)
        {
            try
            {
                this.Id_jefe_ruta = ConvertValueHelper.ConvertirNumero(row["Id_jefe_ruta"]);
                this.Jefe_ruta = new Usuarios
                {
                    Id_usuario = ConvertValueHelper.ConvertirNumero(row["Id_usuario_jefe_ruta"]),
                    Fecha_ingreso = ConvertValueHelper.ConvertirFecha(row["Fecha_ingreso_jefe_ruta"]),
                    Alias = ConvertValueHelper.ConvertirCadena(row["Alias_jefe_ruta"]),
                    Nombres = ConvertValueHelper.ConvertirCadena(row["Nombres_jefe_ruta"]),
                    Apellidos = ConvertValueHelper.ConvertirCadena(row["Apellidos_jefe_ruta"]),
                    Identificacion = ConvertValueHelper.ConvertirCadena(row["Identificacion_jefe_ruta"]),
                    Celular = ConvertValueHelper.ConvertirCadena(row["Celular_jefe_ruta"]),
                    Email = ConvertValueHelper.ConvertirCadena(row["Email_jefe_ruta"]),
                    Tipo_usuario = ConvertValueHelper.ConvertirCadena(row["Tipo_usuario_jefe_ruta"]),
                    Estado_usuario = ConvertValueHelper.ConvertirCadena(row["Estado_usuario_jefe_ruta"]),
                };

                this.Id_cobrador = ConvertValueHelper.ConvertirNumero(row["Id_cobrador"]);
                this.Cobrador = new Usuarios
                {
                    Id_usuario = ConvertValueHelper.ConvertirNumero(row["Id_usuario_cobrador"]),
                    Fecha_ingreso = ConvertValueHelper.ConvertirFecha(row["Fecha_ingreso_cobrador"]),
                    Alias = ConvertValueHelper.ConvertirCadena(row["Alias_cobrador"]),
                    Nombres = ConvertValueHelper.ConvertirCadena(row["Nombres_cobrador"]),
                    Apellidos = ConvertValueHelper.ConvertirCadena(row["Apellidos_cobrador"]),
                    Identificacion = ConvertValueHelper.ConvertirCadena(row["Identificacion_cobrador"]),
                    Celular = ConvertValueHelper.ConvertirCadena(row["Celular_cobrador"]),
                    Email = ConvertValueHelper.ConvertirCadena(row["Email_cobrador"]),
                    Tipo_usuario = ConvertValueHelper.ConvertirCadena(row["Tipo_usuario_cobrador"]),
                    Estado_usuario = ConvertValueHelper.ConvertirCadena(row["Estado_usuario_cobrador"]),
                };
                this.Estado = ConvertValueHelper.ConvertirCadena(row["Estado"]);
            }
            catch (Exception)
            {

            }
        }

        public int Id_jefe_ruta { get; set; }

        public int Id_cobrador { get; set; }

        
        public virtual Usuarios Jefe_ruta { get; set; }

        
        public virtual Usuarios Cobrador { get; set; }

        
        public List<Usuarios_cobradores> Cobradores { get; set; }

        public string Estado { get; set; }
    }
}

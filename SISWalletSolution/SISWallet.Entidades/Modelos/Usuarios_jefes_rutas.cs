namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class Usuarios_jefes_rutas
    {
        public Usuarios_jefes_rutas()
        {

        }

        public Usuarios_jefes_rutas(DataTable dt)
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

                    this.Id_cobro = ConvertValueHelper.ConvertirNumero(dt.Rows[0]);
                    this.Cobro = new Cobros(dt.Rows[0]);

                    this.Jefes_Rutas = new List<Usuarios_jefes_rutas>();

                    foreach (DataRow row in dt.Rows)
                    {
                        this.Jefes_Rutas.Add(new Usuarios_jefes_rutas(row));
                    };
                }
            }
            catch (Exception)
            {

            }
        }

        public Usuarios_jefes_rutas(DataRow row)
        {
            try
            {
                this.Id_administrador = ConvertValueHelper.ConvertirNumero(row["Id_administrador"]);
                this.Administrador = new Usuarios
                {
                    Id_usuario = ConvertValueHelper.ConvertirNumero(row["Id_usuario_administrador"]),
                    Fecha_ingreso = ConvertValueHelper.ConvertirFecha(row["Fecha_ingreso_administrador"]),
                    Alias = ConvertValueHelper.ConvertirCadena(row["Alias_administrador"]),
                    Nombres = ConvertValueHelper.ConvertirCadena(row["Nombres_administrador"]),
                    Apellidos = ConvertValueHelper.ConvertirCadena(row["Apellidos_administrador"]),
                    Identificacion = ConvertValueHelper.ConvertirCadena(row["Identificacion_administrador"]),
                    Celular = ConvertValueHelper.ConvertirCadena(row["Celular_administrador"]),
                    Email = ConvertValueHelper.ConvertirCadena(row["Email_administrador"]),
                    Tipo_usuario = ConvertValueHelper.ConvertirCadena(row["Tipo_usuario_administrador"]),
                    Estado_usuario = ConvertValueHelper.ConvertirCadena(row["Estado_usuario_administrador"]),
                };

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

                this.Id_cobro = ConvertValueHelper.ConvertirNumero(row["Id_cobro"]);
                this.Cobro = new Cobros(row);

                this.Estado = ConvertValueHelper.ConvertirCadena(row["Estado"]);
            }
            catch (Exception)
            {

            }
        }

        public int Id_administrador { get; set; }

        
        public virtual Usuarios Administrador { get; set; }

        public int Id_jefe_ruta { get; set; }

        
        public virtual Usuarios Jefe_ruta { get; set; }

        public int Id_cobro { get; set; }

        
        public virtual Cobros Cobro { get; set; }

        
        public List<Usuarios_jefes_rutas> Jefes_Rutas { get; set; }

        public string Estado { get; set; }
    }
}

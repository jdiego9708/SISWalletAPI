namespace SISWallet.Entidades.Models
{
    
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Models;
    using SISWallet.Entidades.Helpers;

    public class Usuarios_reglas
    {
        public Usuarios_reglas()
        {

        }

        public Usuarios_reglas(DataTable dt)
        {
            try
            {
                if (dt != null)
                {
                    this.Id_usuario = ConvertValueHelper.ConvertirNumero(dt.Rows[0]["Id_usuario"]);
                    this.Usuario = new Usuarios(dt.Rows[0]);

                    this.Reglas = new List<Usuarios_reglas>();

                    foreach(DataRow row in dt.Rows)
                    {
                        this.Reglas.Add(new Usuarios_reglas(row));
                    };
                }                
            }
            catch (Exception)
            {

            }
        }

        public Usuarios_reglas(DataRow row)
        {
            try
            {
                this.Id_usuario = ConvertValueHelper.ConvertirNumero(row["Id_usuario"]);
                this.Usuario = new Usuarios(row);
                this.Id_regla = ConvertValueHelper.ConvertirNumero(row["Id_regla"]);
                this.Regla = new Reglas(row);
                this.Estado = ConvertValueHelper.ConvertirCadena(row["Estado"]);
            }
            catch (Exception)
            {

            }
        }

        public int Id_usuario { get; set; }
        public int Id_regla { get; set; }  
        public Usuarios Usuario { get; set; }
        public Reglas Regla { get; set; }
        public string Estado { get; set; }
        public List<Usuarios_reglas> Reglas { get; set; }
    }
}

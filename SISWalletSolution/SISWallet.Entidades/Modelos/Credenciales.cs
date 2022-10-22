namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    
    public class Credenciales
    {
        public Credenciales()
        {

        }

        public Credenciales(DataRow row)
        {
            try
            {
                Id_credencial = ConvertValueHelper.ConvertirNumero(row["Id_credencial"]);
                Id_usuario = ConvertValueHelper.ConvertirNumero(row["Id_usuario"]);
                Usuario = new Usuarios(row);
                Fecha_credencial = ConvertValueHelper.ConvertirFecha(row["Fecha_credencial"]);
                Hora_credencial = ConvertValueHelper.ConvertirHora(row["Hora_credencial"]);
                Password = ConvertValueHelper.ConvertirCadena(row["Password"]);
                Estado_credencial = ConvertValueHelper.ConvertirCadena(row["Estado_credencial"]);

            }
            catch (Exception)
            {

            }
        }

        public int Id_credencial { get; set; }
        public int Id_usuario { get; set; }
        public Usuarios Usuario { get; set; }
        public DateTime Fecha_credencial { get; set; } = DateTime.Now;
        public TimeSpan Hora_credencial { get; set; } = DateTime.Now.TimeOfDay;
        public string Password { get; set; }       
        public string Estado_credencial { get; set; }
    }
}

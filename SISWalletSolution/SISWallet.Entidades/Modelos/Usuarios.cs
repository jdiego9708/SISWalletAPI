namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Data;

    public class Usuarios
    {
        public Usuarios()
        {

        }

        public Usuarios(DataRow row)
        {
            try
            {
                this.Id_usuario = ConvertValueHelper.ConvertirNumero(row["Id_usuario"]);
                this.Alias = ConvertValueHelper.ConvertirCadena(row["Alias"]);
                this.Nombres = ConvertValueHelper.ConvertirCadena(row["Nombres"]);
                this.Apellidos = ConvertValueHelper.ConvertirCadena(row["Apellidos"]);
                this.Identificacion = ConvertValueHelper.ConvertirCadena(row["Identificacion"]);
                this.Celular = ConvertValueHelper.ConvertirCadena(row["Celular"]);
                this.Email = ConvertValueHelper.ConvertirCadena(row["Email"]);
                this.Tipo_usuario = ConvertValueHelper.ConvertirCadena(row["Tipo_usuario"]);
                this.Estado_usuario = ConvertValueHelper.ConvertirCadena(row["Estado_usuario"]);
            }
            catch (Exception)
            {

            }
        }

        public int Id_usuario { get; set; }

        public DateTime Fecha_ingreso { get; set; } = DateTime.Now;

        public string Alias { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Identificacion { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public string Tipo_usuario { get; set; } = "JEFE";

        public string Estado_usuario { get; set; } = "ACTIVO";

        public string NombreCompletoAlias
        {
            get
            {
                return string.Format("Alias: {0} | Nombre: {1} {2}", this.Alias, this.Nombres, this.Apellidos);
            }
        }
        public string NombreCompleto
        {
            get
            {
                return string.Format("{0} {1}", this.Nombres, this.Apellidos);
            }
        }

        public override string ToString()
        {
            return this.Nombres;
        }
    }
}

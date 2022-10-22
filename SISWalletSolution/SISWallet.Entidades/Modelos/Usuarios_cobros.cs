namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Data;

    public class Usuarios_cobros
    {
        public Usuarios_cobros()
        {

        }

        public Usuarios_cobros(DataRow row)
        {
            try
            {
                this.Id_usuario = ConvertValueHelper.ConvertirNumero(row["Id_usuario"]);
                this.Usuario = new Usuarios(row);
                this.Id_cobro = ConvertValueHelper.ConvertirNumero(row["Id_cobro"]);
                this.Cobro = new Cobros(row);
            }
            catch (Exception)
            {

            }
        }

        public int Id_cobro { get; set; }
        public int Id_usuario { get; set; }       
        public virtual Usuarios Usuario { get; set; }      
        public virtual Cobros Cobro { get; set; }
    }
}

namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    public class Cobros
    {
        public Cobros()
        {

        }

        public Cobros(DataRow row)
        {
            this.Id_cobro = ConvertValueHelper.ConvertirNumero(row["Id_cobro"]);
            this.Id_usuario = ConvertValueHelper.ConvertirNumero(row["Id_usuario"]);
            this.Usuario = new Usuarios(row);
            this.Id_tipo_producto = ConvertValueHelper.ConvertirNumero(row["Id_tipo_producto"]);
            this.Tipo_producto = new Tipo_productos(row);
            this.Id_zona = ConvertValueHelper.ConvertirNumero(row["Id_zona"]);
            this.Zona = new Zonas(row);
            this.Fecha_apertura = ConvertValueHelper.ConvertirFecha(row["Fecha_apertura"]);
            this.Hora_apertura = ConvertValueHelper.ConvertirHora(row["Hora_apertura"]);
            this.Valor_cobro = Convert.ToDecimal(row["Valor_cobro"]);
            this.Estado_cobro = ConvertValueHelper.ConvertirCadena(row["Estado_cobro"]);
            this.Observaciones = ConvertValueHelper.ConvertirCadena(row["Observaciones"]);
        }

        public int Id_cobro { get; set; }
        public int Id_usuario { get; set; }
        public Usuarios Usuario { get; set; }
        public int Id_tipo_producto { get; set; }
        public Tipo_productos Tipo_producto { get; set; }
        public int Id_zona { get; set; }
        public Zonas Zona { get; set; }
        public DateTime Fecha_apertura { get; set; }
        public TimeSpan Hora_apertura { get; set; }
        public decimal Valor_cobro { get; set; }
        public string Estado_cobro { get; set; }
        public string Observaciones { get; set; }
    }
}

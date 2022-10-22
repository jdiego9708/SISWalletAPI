namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data;

    public class Reglas
    {
        public Reglas()
        {

        }

        public Reglas(DataRow row)
        {
            this.Id_regla = ConvertValueHelper.ConvertirNumero(row["Id_regla"]);
            this.Id_cobro = ConvertValueHelper.ConvertirNumero(row["Id_cobro"]);
            this.Cobro = new Cobros(row);
            this.Nombre_regla = ConvertValueHelper.ConvertirCadena(row["Nombre_regla"]);
            this.Descripcion_regla = ConvertValueHelper.ConvertirCadena(row["Descripcion_regla"]);
            this.Valor_regla = Convert.ToDecimal(row["Valor_regla"]);
            this.Estado_regla = ConvertValueHelper.ConvertirCadena(row["Estado_regla"]);
            this.Tipo_usuario_regla = ConvertValueHelper.ConvertirCadena(row["Tipo_usuario_regla"]);
        }

        public int Id_regla { get; set; }
        public int Id_cobro { get; set; }
        public virtual Cobros Cobro { get; set; }
        public string Nombre_regla { get; set; }
        public string Descripcion_regla { get; set; }
        public decimal Valor_regla { get; set; }
        public string Estado_regla { get; set; }
        public string Tipo_usuario_regla { get; set; }
    }
}

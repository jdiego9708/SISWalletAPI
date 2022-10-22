namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;
    public class Gastos
    {
        public Gastos()
        {

        }

        public Gastos(DataRow row)
        {
            this.Id_gasto = ConvertValueHelper.ConvertirNumero(row["Id_gasto"]);
            this.Id_tipo_gasto = ConvertValueHelper.ConvertirNumero(row["Id_tipo_gasto"]);
            this.Tipo_gasto = new Tipo_gastos(row);
            this.Id_usuario = ConvertValueHelper.ConvertirNumero(row["Id_usuario"]);
            this.Usuario = new Usuarios(row);
            this.Id_turno = ConvertValueHelper.ConvertirNumero(row["Id_turno"]);
            this.Turno = new Turnos(row);
            this.Fecha_gasto = ConvertValueHelper.ConvertirFecha(row["Fecha_gasto"]);
            this.FechaGasto = this.Fecha_gasto.ToString("yyyy-MM-dd");
            this.Hora_gasto = ConvertValueHelper.ConvertirHora(row["Hora_gasto"]);
            this.Valor_gasto = Convert.ToDecimal(row["Valor_gasto"]);
            this.Observaciones_gasto = ConvertValueHelper.ConvertirCadena(row["Observaciones_gasto"]);
            this.Estado_gasto = ConvertValueHelper.ConvertirCadena(row["Estado_gasto"]);
        }

        public int Id_gasto { get; set; }
        public int Id_tipo_gasto { get; set; }
        public Tipo_gastos Tipo_gasto { get; set; }
        public int Id_usuario { get; set; }
        public Usuarios Usuario { get; set; }
        public int Id_turno { get; set; }
        public Turnos Turno { get; set; }
        public DateTime Fecha_gasto { get; set; }
        public string FechaGasto { get; set; }
        public TimeSpan Hora_gasto { get; set; }
        public decimal Valor_gasto { get; set; }
        public string Observaciones_gasto { get; set; }
        public string Estado_gasto { get; set; }
    }
}

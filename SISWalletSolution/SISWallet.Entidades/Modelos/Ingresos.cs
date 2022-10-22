namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;

    public class Ingresos
    {
        public Ingresos()
        {

        }

        public Ingresos(DataRow row)
        {
            this.Id_ingreso = ConvertValueHelper.ConvertirNumero(row["Id_ingreso"]);
            this.Id_usuario = ConvertValueHelper.ConvertirNumero(row["Id_usuario"]);
            this.Usuario = new Usuarios(row);
            this.Id_turno = ConvertValueHelper.ConvertirNumero(row["Id_turno"]);
            this.Turno = new Turnos(row);
            this.Fecha_ingreso = ConvertValueHelper.ConvertirFecha(row["Fecha_ingreso"]);
            this.Hora_ingreso = ConvertValueHelper.ConvertirHora(row["Hora_ingreso"]);
            this.Valor_ingreso = Convert.ToDecimal(row["Valor_ingreso"]);
            this.Observaciones_ingreso = ConvertValueHelper.ConvertirCadena(row["Observaciones_ingreso"]);
            this.Estado_ingreso = ConvertValueHelper.ConvertirCadena(row["Estado_ingreso"]);
        }
        public int Id_ingreso { get; set; }
        public int Id_usuario { get; set; }
        public virtual Usuarios Usuario { get; set; }
        public int Id_turno { get; set; }
        public virtual Turnos Turno { get; set; }
        public DateTime Fecha_ingreso { get; set; }
        public TimeSpan Hora_ingreso { get; set; }
        public decimal Valor_ingreso { get; set; }
        public string Observaciones_ingreso { get; set; }
        public string Estado_ingreso { get; set; }
    }
}

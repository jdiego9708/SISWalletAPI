namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data;

    public class Agendamiento_cobros
    {
        public Agendamiento_cobros()
        {

        }

        public Agendamiento_cobros(DataRow row)
        {
            try
            {
                this.Id_agendamiento = ConvertValueHelper.ConvertirNumero(row["Id_agendamiento"]);
                this.Id_venta = ConvertValueHelper.ConvertirNumero(row["Id_venta"]);
                this.Venta = new Ventas(row);
                this.Id_turno = ConvertValueHelper.ConvertirNumero(row["Id_turno"]);
                this.Turno = new Turnos(row);
                this.Orden_cobro = ConvertValueHelper.ConvertirNumero(row["Orden_cobro"]);
                this.Fecha_cobro = ConvertValueHelper.ConvertirFecha(row["Fecha_cobro"]);
                this.Hora_cobro = ConvertValueHelper.ConvertirHora((row["Hora_cobro"]));
                this.Valor_cobro = Convert.ToDecimal(row["Valor_cobro"]);
                this.Valor_pagado = Convert.ToDecimal(row["Valor_pagado"]);
                this.Saldo_restante = Convert.ToDecimal(row["Saldo_restante"]);
                this.Tipo_cobro = ConvertValueHelper.ConvertirCadena(row["Tipo_cobro"]);
                this.Observaciones_cobro = ConvertValueHelper.ConvertirCadena(row["Observaciones_cobro"]);
                this.Estado_cobro = ConvertValueHelper.ConvertirCadena(row["Estado_cobro"]);
            }
            catch (Exception)
            {

            }
        }

        public Agendamiento_cobros(Agendamiento_cobros agendamiento)
        {
            try
            {
                this.Id_agendamiento = agendamiento.Id_agendamiento;
                this.Id_venta = agendamiento.Id_venta;
                this.Venta = agendamiento.Venta;
                this.Id_turno = agendamiento.Id_turno;
                this.Turno = agendamiento.Turno;
                this.Orden_cobro = agendamiento.Orden_cobro;
                this.Fecha_cobro = agendamiento.Fecha_cobro;
                this.Hora_cobro = agendamiento.Hora_cobro;
                this.Valor_cobro = agendamiento.Valor_cobro;
                this.Valor_pagado = agendamiento.Valor_pagado;
                this.Saldo_restante = agendamiento.Saldo_restante;
                this.Tipo_cobro = agendamiento.Tipo_cobro;
                this.Observaciones_cobro = agendamiento.Observaciones_cobro;
                this.Estado_cobro = agendamiento.Estado_cobro;
            }
            catch (Exception)
            {

            }
        }

        [Key]
        public int Id_agendamiento { get; set; }
        public int Id_venta { get; set; }
        public virtual Ventas? Venta { get; set; }
        public int Id_turno { get; set; }
        public virtual Turnos? Turno { get; set; }
        public int Orden_cobro { get; set; }
        public DateTime Fecha_cobro { get; set; } = DateTime.Now;
        public TimeSpan Hora_cobro { get; set; } = DateTime.Now.TimeOfDay;
        public decimal Valor_cobro { get; set; }
        public decimal Valor_pagado { get; set; }
        public decimal Saldo_restante { get; set; }
        public string Tipo_cobro { get; set; }
        public string Observaciones_cobro { get; set; }
        public string Estado_cobro { get; set; }
    }
}

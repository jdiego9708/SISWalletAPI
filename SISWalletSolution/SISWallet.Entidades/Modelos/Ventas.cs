namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;

    public class Ventas
    {
        public Ventas()
        {

        }

        public Ventas(DataRow row)
        {
            try
            {
                this.Id_venta = ConvertValueHelper.ConvertirNumero(row["Id_venta"]);
                this.Id_cobro = ConvertValueHelper.ConvertirNumero(row["Id_cobro"]);
                this.Cobro = new Cobros(row);
                this.Id_tipo_producto = ConvertValueHelper.ConvertirNumero(row["Id_tipo_producto"]);
                this.Tipo_producto = new Tipo_productos(row);
                this.Id_cliente = ConvertValueHelper.ConvertirNumero(row["Id_usuario"]);
                this.Cliente = new Usuarios(row);
                this.Id_direccion = ConvertValueHelper.ConvertirNumero(row["Id_direccion"]);
                this.Direccion = new Direccion_clientes(row);
                this.Id_turno = ConvertValueHelper.ConvertirNumero(row["Id_turno"]);
                this.Turno = new Turnos(row);
                this.Fecha_venta = ConvertValueHelper.ConvertirFecha(row["Fecha_venta"]);
                this.FechaVenta = this.Fecha_venta.ToString("yyyy-MM-dd");
                this.Hora_venta = ConvertValueHelper.ConvertirHora(row["Hora_venta"]);
                this.Valor_venta = Convert.ToDecimal(row["Valor_venta"]);
                this.Interes_venta = Convert.ToDecimal(row["Interes_venta"]);
                this.Total_venta = Convert.ToDecimal(row["Total_venta"]);
                this.Numero_cuotas = ConvertValueHelper.ConvertirNumero(row["Numero_cuotas"]);
                this.Frecuencia_cobro = ConvertValueHelper.ConvertirCadena(row["Frecuencia_cobro"]);
                this.Valor_cuota = Convert.ToDecimal(row["Valor_cuota"]);
                this.Estado_venta = ConvertValueHelper.ConvertirCadena(row["Estado_venta"]);
                this.Tipo_venta = ConvertValueHelper.ConvertirCadena(row["Tipo_venta"]);

            }
            catch (Exception)
            {

            }
        }

        public int Id_venta { get; set; }
        public int Id_cobro { get; set; }
        public Cobros Cobro { get; set; }
        public int Id_tipo_producto { get; set; }
        public Tipo_productos Tipo_producto { get; set; }
        public int Id_cliente { get; set; }
        public Usuarios Cliente { get; set; }
        public int Id_direccion { get; set; }
        public Direccion_clientes? Direccion { get; set; }
        public int Id_turno { get; set; }
        public Turnos Turno { get; set; }
        public DateTime Fecha_venta { get; set; }
        public string FechaVenta { get; set; }
        public TimeSpan Hora_venta { get; set; }
        public decimal Valor_venta { get; set; }
        public decimal Interes_venta { get; set; }
        public decimal Total_venta { get; set; }
        public int Numero_cuotas { get; set; }
        public string Frecuencia_cobro { get; set; }
        public decimal Valor_cuota { get; set; }
        public string Estado_venta { get; set; }
        public string Tipo_venta { get; set; }
    }
}

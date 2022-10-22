namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;



    public class Detalle_articulos_venta
    {
        public Detalle_articulos_venta()
        {

        }

        public Detalle_articulos_venta(DataRow row)
        {
            this.Id_detalle_venta = ConvertValueHelper.ConvertirNumero(row["Id_detalle_venta"]);
            this.Id_articulo = ConvertValueHelper.ConvertirNumero(row["Id_articulo"]);
            this.Articulo = new Articulos(row);
            this.Id_venta = ConvertValueHelper.ConvertirNumero(row["Id_venta"]);
            this.Venta = new Ventas(row);
            this.Cantidad_articulo = ConvertValueHelper.ConvertirNumero(row["Cantidad_articulo"]);
            this.Estado_detalle = ConvertValueHelper.ConvertirCadena(row["Estado_detalle"]);
        }

        [Key]
        public int Id_detalle_venta { get; set; }

        public int Id_articulo { get; set; }


        public virtual Articulos Articulo { get; set; }

        public int Id_venta { get; set; }


        public virtual Ventas Venta { get; set; }

        public int Cantidad_articulo { get; set; }

        [Display(Name = "Estado de detalle")]
        public string Estado_detalle { get; set; }
    }
}

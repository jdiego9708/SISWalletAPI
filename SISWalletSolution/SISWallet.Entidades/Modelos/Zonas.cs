namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;

    public class Zonas
    {
        public Zonas()
        {

        }

        public Zonas(DataRow row)
        {
            this.Id_zona = ConvertValueHelper.ConvertirNumero(row["Id_zona"]);
            this.Id_ciudad = ConvertValueHelper.ConvertirNumero(row["Id_ciudad"]);
            this.Ciudad = new Ciudades(row);
            this.Nombre_zona = ConvertValueHelper.ConvertirCadena(row["Nombre_zona"]);
            this.Observaciones_zona = ConvertValueHelper.ConvertirCadena(row["Observaciones_zona"]);
            this.Estado_zona = ConvertValueHelper.ConvertirCadena(row["Estado_zona"]);
        }
        public int Id_zona { get; set; }
        public int Id_ciudad { get; set; }
        public virtual Ciudades Ciudad { get; set; }
        public string Nombre_zona { get; set; }
        public string Observaciones_zona { get; set; }
        public string Estado_zona { get; set; }
    }
}

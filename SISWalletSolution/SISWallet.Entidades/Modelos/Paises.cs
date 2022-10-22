namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data;

    public class Paises
    {
        public Paises()
        {

        }

        public Paises(DataRow row)
        {
            this.Id_pais = ConvertValueHelper.ConvertirNumero(row["Id_pais"]);
            this.Nombre_pais = ConvertValueHelper.ConvertirCadena(row["Nombre_pais"]);
            this.Observaciones_pais = ConvertValueHelper.ConvertirCadena(row["Observaciones_pais"]);
            this.Estado_pais = ConvertValueHelper.ConvertirCadena(row["Estado_pais"]);
        }
        public int Id_pais { get; set; }
        public string Nombre_pais { get; set; }
        public string Observaciones_pais { get; set; }
        public string Estado_pais { get; set; }
    }
}

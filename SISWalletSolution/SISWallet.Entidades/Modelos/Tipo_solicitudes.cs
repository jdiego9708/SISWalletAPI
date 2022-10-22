namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;


    public class Tipo_solicitudes
    {
        public Tipo_solicitudes()
        {

        }

        public Tipo_solicitudes(DataRow row)
        {
            this.Id_tipo_solicitud = ConvertValueHelper.ConvertirNumero(row["Id_tipo_solicitud"]);
            this.Nombre_tipo_solicitud = ConvertValueHelper.ConvertirCadena(row["Nombre_tipo_solicitud"]);
            this.Estado_tipo_solicitud = ConvertValueHelper.ConvertirCadena(row["Estado_tipo_solicitud"]);
        }

        public int Id_tipo_solicitud { get; set; }
        public string Nombre_tipo_solicitud { get; set; }
        public string Estado_tipo_solicitud { get; set; }
    }
}

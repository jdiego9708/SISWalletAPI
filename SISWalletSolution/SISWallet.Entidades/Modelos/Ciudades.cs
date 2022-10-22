namespace SISWallet.Entidades.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    using System.Runtime.Serialization;
    using System.Data;
    using SISWallet.Entidades.Helpers;

    //using SQLite;

    public class Ciudades
    {
        public Ciudades()
        {

        }

        public Ciudades(DataRow row)
        {
            try
            {
                this.Id_ciudad = ConvertValueHelper.ConvertirNumero(row["Id_ciudad"]);
                this.Id_pais = ConvertValueHelper.ConvertirNumero(row["Id_pais"]);
                this.Pais = new Paises(row);
                this.Nombre_ciudad = ConvertValueHelper.ConvertirCadena(row["Nombre_ciudad"]);
                this.Observaciones_ciudad = ConvertValueHelper.ConvertirCadena(row["Observaciones_ciudad"]);
                this.Estado_ciudad = ConvertValueHelper.ConvertirCadena(row["Estado_ciudad"]);
            }
            catch (Exception)
            {

            }
        }


        [Key]
        public int Id_ciudad { get; set; }

        public int Id_pais { get; set; }

        
        public Paises Pais { get; set; }


        
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo 50 carácteres.")]
        public string Nombre_ciudad { get; set; }

        [Display(Name = "Observaciones")]
        
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener como máximo 50 carácteres.")]
        public string Observaciones_ciudad { get; set; }

        [Display(Name = "Estado")]
        
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo 50 carácteres.")]
        public string Estado_ciudad { get; set; }
       
    }
}

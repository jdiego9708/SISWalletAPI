namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    

    public class Tipo_gastos
    {
        public Tipo_gastos()
        {

        }

        public Tipo_gastos(DataRow row)
        {
            try
            {
                this.Id_tipo_gasto = ConvertValueHelper.ConvertirNumero(row["Id_tipo_gasto"]);
                this.Nombre_tipo_gasto = ConvertValueHelper.ConvertirCadena(row["Nombre_tipo_gasto"]);
                this.Estado_tipo_gasto = ConvertValueHelper.ConvertirCadena(row["Estado_tipo_gasto"]);     
            }
            catch (Exception)
            {

            }
        }

        [Key]
        public int Id_tipo_gasto { get; set; }


        
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo de 50 carácteres.")]
        public string Nombre_tipo_gasto { get; set; }

        [Display(Name = "Estado del gasto")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener como máximo de 150 carácteres.")]
        public string Estado_tipo_gasto { get; set; }

    }
}

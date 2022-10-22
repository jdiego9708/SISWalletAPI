namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    

    public class Tipo_productos
    {
        public Tipo_productos()
        {

        }

        public Tipo_productos(DataRow row)
        {
            try
            {
                this.Id_tipo_producto = ConvertValueHelper.ConvertirNumero(row["Id_tipo_producto"]);
                this.Nombre_producto = ConvertValueHelper.ConvertirCadena(row["Nombre_producto"]);
                this.Descripcion_producto = ConvertValueHelper.ConvertirCadena(row["Descripcion_producto"]);
                this.Estado_producto = ConvertValueHelper.ConvertirCadena(row["Estado_producto"]);              
            }
            catch (Exception)
            {

            }
        }

        [Key]
        public int Id_tipo_producto { get; set; }

        [Display(Name = "Nombre del producto")]
        
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener como máximo de 50 carácteres.")]
        public string Nombre_producto { get; set; }

        [Display(Name = "Descripción del producto")]
        [MaxLength(150, ErrorMessage = "El campo {0} debe tener como máximo de 150 carácteres.")]
        public string Descripcion_producto { get; set; }

        [Display(Name = "Estado del producto")]
        public string Estado_producto { get; set; } = "ACTIVO";

        
        public virtual ICollection<Cobros> Cobros { get; set; }

    }
}

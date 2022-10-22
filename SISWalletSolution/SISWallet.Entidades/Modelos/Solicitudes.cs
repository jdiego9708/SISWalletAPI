namespace SISWallet.Entidades.Models
{
    using SISWallet.Entidades.Helpers;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Windows.Input;
    


    public class Solicitudes
    {
        public Solicitudes()
        {

        }

        public Solicitudes(DataRow row)
        {
            try
            {
                this.Id_solicitud = ConvertValueHelper.ConvertirNumero(row["Id_solicitud"]);
                this.Id_tipo_solicitud = ConvertValueHelper.ConvertirNumero(row["Id_tipo_solicitud"]);
                this.Tipo_solicitud = new Tipo_solicitudes(row);
                this.Id_usuario = ConvertValueHelper.ConvertirNumero(row["Id_usuario"]);
                this.Usuario = new Usuarios(row);
                this.Parametro_solicitud = ConvertValueHelper.ConvertirCadena(row["Parametro_solicitud"]);
                this.Fecha_solicitud = ConvertValueHelper.ConvertirFecha(row["Fecha_solicitud"]);
                this.Hora_solicitud = ConvertValueHelper.ConvertirHora((row["Hora_solicitud"]));
                this.Asunto_solicitud = ConvertValueHelper.ConvertirCadena(row["Asunto_solicitud"]);
                this.Descripcion_solicitud = ConvertValueHelper.ConvertirCadena(row["Descripcion_solicitud"]);
                this.Estado_solicitud = ConvertValueHelper.ConvertirCadena(row["Estado_solicitud"]);
            }
            catch (Exception)
            {

            }
        }


        [Key]
        public int Id_solicitud { get; set; }

        public int Id_tipo_solicitud { get; set; }

        
        public virtual Tipo_solicitudes Tipo_solicitud { get; set; }

        public int Id_usuario { get; set; }

        
        public virtual Usuarios Usuario { get; set; }

        [Display(Name = "Parametro")]
        public string Parametro_solicitud { get; set; }

        [Display(Name = "Fecha de la solicitud")]
        
        [DataType(DataType.Date)]
        public DateTime Fecha_solicitud { get; set; } = DateTime.Now;

        [Display(Name = "Hora de la solicitud")]
        
        [DataType(DataType.Time)]
        public TimeSpan Hora_solicitud { get; set; } = DateTime.Now.TimeOfDay;

        [Display(Name = "Asunto")]
        
        public string Asunto_solicitud { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener como máximo de 200 carácteres.")]
        public string Descripcion_solicitud { get; set; }

        [Display(Name = "Estado")]
        
        public string Estado_solicitud { get; set; }
    }
}


using System.ComponentModel.DataAnnotations;

namespace ClinicaMD.Web.Models
{
    public class Procedure
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Procedimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public ProcedureType ProcedureType { get; set; }

        [Display(Name = "Doctor")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Doctor Doctor { get; set; }

        [Display(Name = "Paciente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Patient Patient { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }

        [Display(Name = "Observaciones")]
        [MaxLength(50, ErrorMessage = "El Campo {0} No Puede Tener Mas De {1} Caracteres.")]
        public string Remarks { get; set; }

    }
}

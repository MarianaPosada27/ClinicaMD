using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicaMD.Web.Models
{
    public class ClinicHistory
    {
        public int Id { get; set; }

        [Display(Name = "Paciente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Patient Patient { get; set; }

        [Display(Name = "Doctor")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Doctor Doctor  { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

    }
}


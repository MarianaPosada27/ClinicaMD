using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaMD.Web.Models.ViewModels
{
    public class ProcedureViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Procedimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ProcedureTypeId { get; set; }
        public IEnumerable<SelectListItem> ProcedureTypes { get; set; }

        [Display(Name = "Doctores")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int DoctorId { get; set; }
        public IEnumerable<SelectListItem> Doctors { get; set; }

        [Display(Name = "Pacientes")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int PatientId { get; set; }
        public IEnumerable<SelectListItem> Patients { get; set; }

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

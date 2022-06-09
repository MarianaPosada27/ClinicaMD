using ClinicaMD.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicaMD.Web.ViewModels
{
    public class ClinicHistoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Paciente")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Patient Patient { get; set; }

        [Display(Name = "Doctor")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int DoctorId { get; set; }
        public IEnumerable<SelectListItem> Doctors { get; set; }

        [Display(Name = "Fecha")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime Date { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(1000, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Description { get; set; }

    }
}


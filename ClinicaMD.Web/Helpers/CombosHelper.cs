using ClinicaMD.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace ClinicaMD.Web.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly ApplicationDbContext _context;

        public CombosHelper(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboDoctors()
        {
            List<SelectListItem> list = _context.Doctors.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " +x.LastName,
                Value = $"{x.Id}"
            })
               .OrderBy(x => x.Text)
               .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Doctor...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboPatients()
        {
            List<SelectListItem> list = _context.Patients.Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = $"{x.Id}"
            })
                .OrderBy(x => x.Text)
                .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un Paciente...]",
                Value = "0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboProcedureTypes()
        {
            List<SelectListItem> list = _context.ProcedureTypes.Select(x => new SelectListItem
            {
                Text = x.Description,
                Value = $"{x.Id}"
            })
               .OrderBy(x => x.Text)
               .ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un tipo de Procedimiento...]",
                Value = "0"
            });

            return list;
        }
    }
}

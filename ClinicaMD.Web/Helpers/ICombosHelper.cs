using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ClinicaMD.Web.Helpers
{
    public interface ICombosHelper
    {
        IEnumerable<SelectListItem> GetComboProcedureTypes();
        IEnumerable<SelectListItem> GetComboDoctors();
        IEnumerable<SelectListItem> GetComboPatients();

    }
}

using ClinicaMD.Web.Models;
using ClinicaMD.Web.Models.ViewModels;
using System.Threading.Tasks;

namespace ClinicaMD.Web.Helpers
{
    public interface IConvertHelper
    {
        Task<Procedure> ToProcedureAsync(ProcedureViewModel model, bool isNew);
        ProcedureViewModel ToProcedureViewModel(Procedure procedure);
    }
}

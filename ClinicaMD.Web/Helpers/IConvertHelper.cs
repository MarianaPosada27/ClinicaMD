using ClinicaMD.Web.Models;
using ClinicaMD.Web.Models.ViewModels;
using ClinicaMD.Web.ViewModels;
using System.Threading.Tasks;

namespace ClinicaMD.Web.Helpers
{
    public interface IConvertHelper
    {
        Task<Procedure> ToProcedureAsync(ProcedureViewModel model, bool isNew);
        ProcedureViewModel ToProcedureViewModel(Procedure procedure);

        Task<ClinicHistory> ToClinicHistoryAsync(ClinicHistoryViewModel model, bool isNew);
        ClinicHistoryViewModel ToClinicHistoryViewModel (ClinicHistory clinicHistory);

        Task<Patient> ToPatientAsync(PatientViewModel model, bool isNew);
        Task<PatientViewModel> ToPatientViewModelAsync(Patient patient);


    }
}

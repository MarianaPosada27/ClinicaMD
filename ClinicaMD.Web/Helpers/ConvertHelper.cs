using ClinicaMD.Web.Data;
using ClinicaMD.Web.Models;
using ClinicaMD.Web.Models.ViewModels;
using System.Threading.Tasks;

namespace ClinicaMD.Web.Helpers
{
    public class ConvertHelper : IConvertHelper
    {
        private readonly ApplicationDbContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConvertHelper (ApplicationDbContext context, ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }
        public async Task<Procedure> ToProcedureAsync(ProcedureViewModel model, bool isNew)
        {

            return new Procedure
            {
                Description = model.Description,
                Price = model.Price,
                Id = isNew ? 0 : model.Id,
                ProcedureType = await _context.ProcedureTypes.FindAsync(model.ProcedureTypeId),
                Remarks = model.Remarks,
                Doctor = await _context.Doctors.FindAsync(model.DoctorId),
                Patient = await _context.Patients.FindAsync(model.PatientId),
             
            };
        }

        public ProcedureViewModel ToProcedureViewModel(Procedure procedure)
        {
            return new ProcedureViewModel
            {
                Description = procedure.Description,
                Price = procedure.Price,
                Id = procedure.Id,
                ProcedureTypeId = procedure.ProcedureType.Id,
                ProcedureTypes = _combosHelper.GetComboProcedureTypes(),
                Remarks = procedure.Remarks,
                DoctorId = procedure.Doctor.Id,
                Doctors = _combosHelper.GetComboDoctors(),
                PatientId = procedure.Patient.Id,
                Patients = _combosHelper.GetComboPatients(),

            };
        }

    
    }
}

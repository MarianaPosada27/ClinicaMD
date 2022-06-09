using ClinicaMD.Web.Data;
using ClinicaMD.Web.Models;
using ClinicaMD.Web.Models.ViewModels;
using ClinicaMD.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<ClinicHistory> ToClinicHistoryAsync(ClinicHistoryViewModel model, bool isNew)
        {
            return new ClinicHistory
            {
                Description = model.Description,
                Id = isNew ? 0 : model.Id,
                Date = DateTime.Now,
                Doctor = await _context.Doctors.FindAsync(model.DoctorId),
                Patient = await _context.Patients.FindAsync(model.Patient.Id),
            };
        }

        public ClinicHistoryViewModel ToClinicHistoryViewModel(ClinicHistory clinicHistory)
        {
            return new ClinicHistoryViewModel

            {
    
                Patient = clinicHistory.Patient,
                DoctorId = clinicHistory.Doctor.Id,
                Doctors = _combosHelper.GetComboDoctors(),
                Date = clinicHistory.Date,
                Description = clinicHistory.Description
                

            };
        }

        public async Task<Patient> ToPatientAsync(PatientViewModel model, bool isNew)
        {
            if (isNew)
            {
                return new Patient
                {
                    Id = isNew ? 0 : model.Id,
                    Address = model.Address,
                    Document = model.Document,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,

                };
            }
            return await _context.Patients
                .Include(x => x.Procedures)
                .ThenInclude(x => x.ProcedureType)
                .Include(x => x.Procedures)
                 .ThenInclude(x => x.Doctor)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            
            
        }

        public async Task<PatientViewModel> ToPatientViewModelAsync(Patient patient)
        {
            return new PatientViewModel
            {
                Id = patient.Id,
                Address = patient.Address,
                Document = patient.Document,
                Email = patient.Email,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                PhoneNumber = patient.PhoneNumber,
                Procedures = patient.Procedures,
                ClinicHistory = await _context.ClinicHistories.FirstOrDefaultAsync(x => x.Patient.Id == patient.Id)
            };
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


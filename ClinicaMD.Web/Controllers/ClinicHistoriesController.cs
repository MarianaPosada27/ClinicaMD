using ClinicaMD.Web.Data;
using ClinicaMD.Web.Helpers;
using ClinicaMD.Web.Models;
using ClinicaMD.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ClinicaMD.Web.Controllers
{
    public class ClinicHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly IConvertHelper _convertHelper;

        public ClinicHistoriesController(ApplicationDbContext context, ICombosHelper combosHelper, IConvertHelper converterHelper)
        {
            _context = context;
            _convertHelper = converterHelper;
            _combosHelper = combosHelper;
        }

        public async Task<RedirectToActionResult> Index(int id)
        {
            if (await _context.ClinicHistories.FirstOrDefaultAsync(x => x.Patient.Id == id) == null)
            {
                return RedirectToAction("Create", new {id});

            }
            return RedirectToAction("Edit", new { id });

        }

        public async Task<IActionResult> Create(int? id)
        {
            return View(new ClinicHistoryViewModel {
                      Patient = await _context.Patients.Include(x => x.Procedures)
                     .ThenInclude(x => x.ProcedureType)
                     .Include(x => x.Procedures)
                     .ThenInclude(x => x.Doctor)
                     .FirstOrDefaultAsync(x => x.Id == id),
                    Doctors = _combosHelper.GetComboDoctors(),
                    Date = DateTime.Now

            });  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClinicHistoryViewModel clinicHistoryViewModel)
        {
            ClinicHistory clinicHistory = await _convertHelper.ToClinicHistoryAsync(clinicHistoryViewModel, true);

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Add(clinicHistory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("index", "Patients", new { area = "" });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya existe una Historia Clinica para este Paciente");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            clinicHistoryViewModel.Doctors = _combosHelper.GetComboDoctors();

            return View(clinicHistoryViewModel);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ClinicHistory clinicHistory = await _context.ClinicHistories
                    .Include(x => x.Patient)
                    .ThenInclude(x => x.Procedures)
                    .ThenInclude(x => x.ProcedureType)
                    .Include(x => x.Patient)
                    .ThenInclude(x => x.Procedures)
                    .ThenInclude(x => x.Doctor)
                    .FirstOrDefaultAsync(x => x.Patient.Id == id);

            if (clinicHistory == null)
            {
                return NotFound();
            }
            ClinicHistoryViewModel clinicHistoryViewModel =  _convertHelper.ToClinicHistoryViewModel(clinicHistory);
            return View(clinicHistoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClinicHistory clinicHistory)
        {
            if (id != clinicHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clinicHistory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "Ya Existe una Historia Clinica con este Id.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }

            }
            return View(clinicHistory);
        }
        
    }
}

    


using ClinicaMD.Web.Data;
using ClinicaMD.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ClinicaMD.Web.Controllers
{
    public class ClinicHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClinicHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Create(int? id)
        {
            return View(new ClinicHistory {
                Patient = await _context.Patients.Include(x => x.Procedures)
                     .ThenInclude(x => x.ProcedureType)
                     .Include(x => x.Procedures)
                     .ThenInclude(x => x.Doctor)
                     .FirstOrDefaultAsync(x => x.Id == id),

            });  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClinicHistory clinicHistory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(clinicHistory);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
            return View(clinicHistory);
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

            return View(clinicHistory);
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

    


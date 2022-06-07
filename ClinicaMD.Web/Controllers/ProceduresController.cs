using ClinicaMD.Web.Data;
using ClinicaMD.Web.Helpers;
using ClinicaMD.Web.Models;
using ClinicaMD.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ClinicaMD.Web.Controllers
{
    public class ProceduresController : Controller
    {

            private readonly ApplicationDbContext _context;
            private readonly ICombosHelper _combosHelper;
            private readonly IConvertHelper _convertHelper;

            public ProceduresController(ApplicationDbContext context, ICombosHelper combosHelper, IConvertHelper converterHelper)
            {
                _context = context;
                _convertHelper = converterHelper;
                _combosHelper = combosHelper;
            }
            public async Task<IActionResult> Index()
            {
                return View(await _context.Procedures
                    .Include(x => x.ProcedureType)
                    .Include(x => x.Doctor)
                    .Include(x=>x.Patient).ToListAsync());
            }
            public IActionResult Create()
            {
                ProcedureViewModel model = new ProcedureViewModel
                {
                    ProcedureTypes = _combosHelper.GetComboProcedureTypes(),
                    Doctors = _combosHelper.GetComboDoctors(),
                    Patients = _combosHelper.GetComboPatients(),

                };
                return View(model);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(ProcedureViewModel procedureViewModel)
            {
                Procedure procedure = await _convertHelper.ToProcedureAsync(procedureViewModel, true);

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Add(procedure);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException dbUpdateException)
                    {
                        if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                        {
                            ModelState.AddModelError(string.Empty, "Ya Existe este procedimiento.");
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
                procedureViewModel.ProcedureTypes = _combosHelper.GetComboProcedureTypes();
                procedureViewModel.Doctors = _combosHelper.GetComboDoctors();
                procedureViewModel.Patients = _combosHelper.GetComboPatients();

                return View(procedureViewModel);
            }
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                Procedure procedure = await _context.Procedures
                    .Include(x => x.ProcedureType)
                    .Include(x => x.Doctor)
                    .Include(x => x.Patient)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (procedure == null)
                {
                    return NotFound();
                }
                ProcedureViewModel model = _convertHelper.ToProcedureViewModel(procedure);
                return View(model);
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(ProcedureViewModel procedureViewModel)
            {
                Procedure procedure = await _convertHelper.ToProcedureAsync(procedureViewModel, false);

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(procedure);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException dbUpdateException)
                    {
                        if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                        {
                            ModelState.AddModelError(string.Empty, "Ya Existe este procedimiento.");
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
                procedureViewModel.ProcedureTypes = _combosHelper.GetComboProcedureTypes();
                procedureViewModel.Doctors = _combosHelper.GetComboDoctors();
                procedureViewModel.Patients = _combosHelper.GetComboPatients();

                return View(procedureViewModel);
            }
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                Procedure procedure = await _context.Procedures.FindAsync(id);
                if (procedure == null)
                {
                    return NotFound();
                }
                _context.Procedures.Remove(procedure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }
    }


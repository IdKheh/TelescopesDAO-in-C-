using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Drozdzynski_Debowska.Telescopes.Interfaces;
using System.Xml.Linq;
using System.Runtime.ConstrainedExecution;

namespace Drozdzynski_Debowska.Telescopes.WebApp.Controllers
{
    public class TelescopesController : Controller
    {
        private readonly IDAO dao;

        public TelescopesController(BLC.BLC blc)
        {
            dao = blc.DAO;
        }

        // GET: Telescopes
        public async Task<IActionResult> Index()
        {
            var Telescopes = dao.GetAllTelescopes();
            return View(Telescopes);
        }

        // GET: Telescopes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telescope = dao.GetAllTelescopes().FirstOrDefault(x => x.Id == id);
            if (telescope == null)
            {
                return NotFound();
            }

            return View(telescope);
        }

        // GET: Telescopes/Create
        public IActionResult Create()
        {
            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name");
            return View();
        }

        // POST: Telescopes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, string Name, int OpticalSystem, int Aperture, int FocalLength, int ProducerId)
        {
            ITelescope telescope = dao.CreateNewTelescope();
            telescope.Name = Name;
            telescope.OpticalSystem = (OpticalSystem)OpticalSystem;
            telescope.Aperture = Aperture;
            telescope.FocalLength = FocalLength;
            telescope.Producer = dao.GetAllProducers().First(p => p.Id == ProducerId);

            this.ModelState.Clear();
            this.TryValidateModel(telescope);

            if (ModelState.IsValid)
            {
                dao.AddTelescope(telescope);
                dao.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name");
            return View(telescope);
        }

        // GET: Telescopes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telescope = dao.GetAllTelescopes().FirstOrDefault(x => x.Id == id);
            if (telescope == null)
            {
                return NotFound();
            }
            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name");
            return View(telescope);
        }

        // POST: Telescopes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string Name, int OpticalSystem, int Aperture, int FocalLength, int ProducerId)
        {
            if (id == null)
            {
                return NotFound();
            }
            ITelescope telescope = dao.GetAllTelescopes().FirstOrDefault(t => t.Id == id);
            ITelescope tmp = dao.CreateNewTelescope();
            if (id != telescope.Id)
            {
                return NotFound();
            }

            tmp.Name = Name;
            tmp.OpticalSystem = (OpticalSystem)OpticalSystem;
            tmp.Aperture = Aperture;
            tmp.FocalLength = FocalLength;
            tmp.Producer = dao.GetAllProducers().First(p => p.Id == ProducerId);

            this.ModelState.Clear();
            this.TryValidateModel(tmp);

            if (ModelState.IsValid)
            {
                telescope.Name = tmp.Name;
                telescope.OpticalSystem = tmp.OpticalSystem;
                telescope.Aperture = tmp.Aperture;
                telescope.FocalLength = tmp.FocalLength;
                telescope.Producer = tmp.Producer;
                try
                {
                    dao.SaveChanges();
                }
                catch (Exception e)
                {
                    if (!TelescopeExists(telescope.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Producer"] = new SelectList(dao.GetAllProducers(), "Id", "Name");
            return View(tmp);
        }

        // GET: Telescopes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var telescope = dao.GetAllTelescopes().FirstOrDefault(x => x.Id == id);
            if (telescope == null)
            {
                return NotFound();
            }

            return View(telescope);
        }

        // POST: Telescopes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var telescope = dao.GetAllTelescopes().FirstOrDefault(x => x.Id == id);
            if (telescope != null)
            {
                dao.RemoveTelescope(telescope);
            }
            dao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool TelescopeExists(int id)
        {
            return dao.GetAllTelescopes().Any(tel => tel.Id == id);
        }
    }
}

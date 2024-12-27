using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IDAO dao;

        public ProducersController(BLC.BLC blc)
        {
            dao = blc.DAO;
        }

        // GET: Producers
        public async Task<IActionResult> Index()
        {
            var Producers = dao.GetAllProducers();
            return View(Producers);
        }

        // GET: Producers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = dao.GetAllProducers().FirstOrDefault(x => x.Id == id);

            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // GET: Producers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, string Name)
        {
            IProducer producer = dao.CreateNewProducer();
            producer.Name = Name;

            this.ModelState.Clear();
            this.TryValidateModel(producer);

            if (ModelState.IsValid)
            {
                dao.AddProducer(producer);
                dao.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }

        // GET: Producers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = dao.GetAllProducers().FirstOrDefault(x => x.Id == id);
            if (producer == null)
            {
                return NotFound();
            }
            return View(producer);
        }

        // POST: Producers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string Name)
        {
            if (id == null)
            {
                return NotFound();
            }
            IProducer producer = dao.GetAllProducers().FirstOrDefault(p => p.Id == id);
            IProducer tmp = dao.CreateNewProducer();
            if (id != producer.Id)
            {
                return NotFound();
            }

            tmp.Name = Name;

            this.ModelState.Clear();
            this.TryValidateModel(tmp);

            if (ModelState.IsValid)
            {
                producer.Name = tmp.Name;
                try
                {
                    dao.SaveChanges();
                }
                catch (Exception e)
                {
                    if (!ProducerExists(producer.Id))
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
            return View(tmp);
        }

        // GET: Producers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producer = dao.GetAllProducers().FirstOrDefault(x => x.Id == id);

            if (producer == null)
            {
                return NotFound();
            }

            return View(producer);
        }

        // POST: Producers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producer = dao.GetAllProducers().FirstOrDefault(x => x.Id == id);
            if (producer != null)
            {
                dao.RemoveProducer(producer);
            }

            dao.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ProducerExists(int id)
        {
            return dao.GetAllProducers().Any(p => p.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarWebApp.Data;
using CarWebApp.Models;

namespace CarWebApp.Controllers
{
    public class CarSpecsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarSpecsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarSpecs
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarSpecs.ToListAsync());
        }

        // GET: CarSpecs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carSpecs = await _context.CarSpecs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carSpecs == null)
            {
                return NotFound();
            }

            return View(carSpecs);
        }

        // GET: CarSpecs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarSpecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarProperty")] CarSpecs carSpecs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carSpecs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carSpecs);
        }

        // GET: CarSpecs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carSpecs = await _context.CarSpecs.FindAsync(id);
            if (carSpecs == null)
            {
                return NotFound();
            }
            return View(carSpecs);
        }

        // POST: CarSpecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarProperty")] CarSpecs carSpecs)
        {
            if (id != carSpecs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carSpecs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarSpecsExists(carSpecs.Id))
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
            return View(carSpecs);
        }

        // GET: CarSpecs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carSpecs = await _context.CarSpecs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carSpecs == null)
            {
                return NotFound();
            }

            return View(carSpecs);
        }

        // POST: CarSpecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carSpecs = await _context.CarSpecs.FindAsync(id);
            _context.CarSpecs.Remove(carSpecs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarSpecsExists(int id)
        {
            return _context.CarSpecs.Any(e => e.Id == id);
        }
    }
}

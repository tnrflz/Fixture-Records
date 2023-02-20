using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Demirbas.Data;
using Demirbas.Models;
using Microsoft.Data.SqlClient;

namespace Demirbas.Controllers
{
    public class FixturesController : Controller
    {
        private readonly FixturesDbContext _context;

        public FixturesController(FixturesDbContext context)
        {
            _context = context;
        }

        // GET: Fixtures
        public async Task<IActionResult> Index()
        {
              return _context.Fixtures != null ? 
                          View(await _context.Fixtures.ToListAsync()) :
                          Problem("Entity set 'FixturesDbContext.Fixtures'  is null.");
        }

        // GET: Fixtures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fixtures == null)
            {
                return NotFound();
            }

            var fixture = await _context.Fixtures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fixture == null)
            {
                return NotFound();
            }

            return View(fixture);
        }

        // GET: Fixtures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fixtures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Count")] Fixture fixture)
        {
      
       
             if (ModelState.IsValid)
               {

                    try
                    {
                  _context.Add(fixture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                    }

                catch (Exception )
                {


                    ModelState.AddModelError("" , " Please enter an ID not exist!");

                }

            }
          

         



            string errors = string.Join("; ", ModelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage) );

            ModelState.AddModelError("", errors);

            return View(fixture);
        }

        // GET: Fixtures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fixtures == null)
            {
                return NotFound();
            }

            var fixture = await _context.Fixtures.FindAsync(id);
            if (fixture == null)
            {
                return NotFound();
            }
            return View(fixture);
        }

        // POST: Fixtures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Count")] Fixture fixture)
        {
            if (id != fixture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fixture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FixtureExists(fixture.Id))
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
            return View(fixture);
        }

        // GET: Fixtures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fixtures == null)
            {
                return NotFound();
            }

            var fixture = await _context.Fixtures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fixture == null)
            {
                return NotFound();
            }

            return View(fixture);
        }

        // POST: Fixtures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fixtures == null)
            {
                return Problem("Entity set 'FixturesDbContext.Fixtures'  is null.");
            }
            var fixture = await _context.Fixtures.FindAsync(id);
            if (fixture != null)
            {
                _context.Fixtures.Remove(fixture);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FixtureExists(int id)
        {
          return (_context.Fixtures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarWebApp.Data;
using CarWebApp.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace CarWebApp.Controllers
{
    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment env;

        public CarsController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Car.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            List<String> carProperty = new List<string>();

            using (SqlConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=aspnet-CarWebApp-E4C26C99-0E40-4845-A8D3-36E74AF917EF;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                SqlCommand com = new SqlCommand("Select CarProperty from CarSpecs", con);
                con.Open();
                using (SqlDataReader oReader = com.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        carProperty.Add(oReader["CarProperty"].ToString());
                    }

                    con.Close();
                }


            }

                
            ViewBag.Message = carProperty;
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarModel,ReleaseDate,Description,Photo,Price,Currency,PriceGel,Photoname,CarSpecs")] Car car, IFormCollection collection)
        {
            double exchangerate = CurrencyConvert.ExchangeRate(car.Currency) * car.Price;
            car.PriceGel = (int) exchangerate;

            if (ModelState.IsValid)
            {
                string unique_filename = null;
                if (car.Photo != null)
                {
                    string uploadsFolder = Path.Combine(env.WebRootPath, "images");
                    unique_filename = Guid.NewGuid().ToString() + "_" + car.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, unique_filename);
                    car.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                    car.Photoname = unique_filename;
                }
            }

            if (!string.IsNullOrEmpty(collection["CarSpecs"]))
            {
                string carSpecs = collection["CarSpecs"];
                car.CarSpecs = carSpecs;
            }


            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            List<String> carProperty = new List<string>();

            using (SqlConnection con = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=aspnet-CarWebApp-E4C26C99-0E40-4845-A8D3-36E74AF917EF;Trusted_Connection=True;MultipleActiveResultSets=true"))
            {
                SqlCommand com = new SqlCommand("Select CarProperty from CarSpecs", con);
                con.Open();
                using (SqlDataReader oReader = com.ExecuteReader())
                {
                    while (oReader.Read())
                    {
                        carProperty.Add(oReader["CarProperty"].ToString());
                    }

                    con.Close();
                }


            }


            ViewBag.Message = carProperty;

            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarModel,ReleaseDate,Description,Photo,Price,Currency,PriceGel,Photoname")] Car car, IFormCollection collection)
        {
            double exchangerate = CurrencyConvert.ExchangeRate(car.Currency) * car.Price;
            car.PriceGel = (int)exchangerate;

            if (!string.IsNullOrEmpty(collection["CarSpecs"]))
            {
                string carSpecs = collection["CarSpecs"];
                car.CarSpecs = carSpecs;
            }

            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
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
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }
    }
}

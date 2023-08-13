using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarsServiceAPI.Data;
using CarsServiceAPI.Models;

namespace CarsServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarContext _context;

        public CarsController(CarContext context)
        {
            _context = context;
        }

        // GET: api/Cars
        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetCars()
        {
          if (_context.Cars == null)
          {
              return NotFound();
          }
            return  _context.Cars.ToList();
        }

        // GET: api/Cars/5
        [HttpGet("{id}")]
        public ActionResult<Car> GetCar(int id)
        {
          if (_context.Cars == null)
          {
              return NotFound();
          }
            var car =  _context.Cars.Find(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        // POST: api/Cars        
        [HttpPost]
        public ActionResult<Car> PostCar(Car car)
        {
            if (_context.Cars == null)
            {
                return Problem("Entity set 'CarContext.Cars'  is null.");
            }
            _context.Cars.Add(car);
            _context.SaveChanges();

            return CreatedAtAction("GetCar", new { id = car.Id }, car); // Status 201 "Created"
        }

        // PUT: api/Cars/5        
        [HttpPut("{id}")]
        public IActionResult PutCar(int id, Car car)
        {
            if (id != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                 _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        

        // DELETE: api/Cars/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            if (_context.Cars == null)
            {
                return NotFound();
            }
            var car =  _context.Cars.Find(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            _context.SaveChanges();

            return NoContent();
        }

        private bool CarExists(int id)
        {
            return (_context.Cars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

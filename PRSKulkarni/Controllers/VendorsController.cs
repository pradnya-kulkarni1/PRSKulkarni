using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSKulkarni.Models;

namespace PRSKulkarni.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // converts to and from JSON on API calls
    public class VendorsController : ControllerBase
    {
        private readonly PrsDbContext _context;

        // constructor
        public VendorsController(PrsDbContext context)
        {
            _context = context;
        }

        // GET: api/Vendors
        [HttpGet] // routing engine finds action by this statement.
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            if (_context.Vendors == null)
            {
                return NotFound();
            }
            return await _context.Vendors.Include(v => v.Products).ToListAsync();

        }

        // api/vendors/code/{code}

        //[HttpGet("code/{vendorcode}")]
        //public ActionResult<Vendor> GetVendorByCode(string vendorcode)

        //HttpHet : api.vendors/code
        //Content-Type: application/json
        //<blank line>
        //"abc"

        [HttpPost("code")]
        public ActionResult GetVendorByCode([FromBody] string vendorcode)
        {

            //POST : api.vendors/code
            //Content-Type: application/json
            //<blank line>
            //"abc"
            var vendor = _context.Vendors.Where(v => v.Code == vendorcode).FirstOrDefault();
            //              mine     mine   LINQ                            Entity Framework
            if (vendor == null)
            {
                return NotFound();
            }
            return Ok(vendor);
        }
        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            if (_context.Vendors == null)
            {
                return NotFound();
            }

            return await _context.Vendors.FindAsync(id);
        }

        // PUT: api/Vendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST: api/Vendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor)
        {
            if (_context.Vendors == null)
            {
                return Problem("Entity set 'PrsDbContext.Vendors'  is null.");
            }
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.Id }, vendor);
        }

        [HttpPost("byCityState")]
        public ActionResult GetVendorByCityState([FromBody] CityStateDTO location)
            {
                // find all vendors in a city and state
                var vendors = _context.Vendors.Where(v => v.City == location.City &&
                                                           v.State == location.State);
            // writes SQL and our SQL is not case sensitive
        // return all vendors in a city and state

            return Ok(vendors); 

            }
        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            if (_context.Vendors == null)
            {
                return NotFound();
            }
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorExists(int id)
        {
            return (_context.Vendors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

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
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PrsDbContext _context;

        public ProductsController(PrsDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
        {
          if (_context.Product == null)
          {
              return NotFound();
          }
            return await _context.Product.Include(p=> p.Vendor).ToListAsync();
            
        }
        //Created a new method which is Action copied existing 
        //Gane new name for Action
        // Fixed route and parameters
        // fixed code

        // GET: api/Products/5
        //http://server/api/products/GetProductPartNumber/1/A100
        [HttpGet("get-product-by-part-number/{VendorID}/{partNum}")]
     //displays details of Product and Vendor when VendorID and partnumber are given
        public async Task<ActionResult<Product>> GetProductByPartNum(int vendorid, string partNum)
        {

            var product = await _context.Product.Include(p => p.Vendor)
                         .FirstOrDefaultAsync(p => p.PartNumber == partNum && p.VendorID == vendorid);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
    
        
        [HttpPost] //we can insert new data into Product Table
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'PrsDbContext.Product'  is null.");
            }
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ID }, product);
        }
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Product == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Product?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}

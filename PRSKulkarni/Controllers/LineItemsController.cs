﻿using System;
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
    public class LineItemsController : ControllerBase
    {
        private readonly PrsDbContext _context;
        //we can add / Get / Update / Delete RequestID, ProductID for product and Quantity of products ordered.
        public LineItemsController(PrsDbContext context) // constructor
        {
            _context = context;
        }

        // GET: api/LineItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineItem>>> GetLineItems()
        {
          if (_context.LineItems == null)
          {
              return NotFound();
          }
            return await _context.LineItems.Include(li => li.Product).Include(li => li.Request).ToListAsync();
        }

        // GET: api/LineItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineItem>> GetLineItem(int id)
        {
          if (_context.LineItems == null)
          {
              return NotFound();
          }
            // var lineItem = await _context.LineItems.FindAsync(id);

            var lineItem = await _context.LineItems.Include(li => li.Product)
                                                   .Include(li => li.Request).
                                                   FirstOrDefaultAsync(li => li.Id == id);
            if (lineItem == null)
            {
                return NotFound();
            }

            return lineItem;
        }

        [HttpGet("lines-for-pr/{Requestid}")] 
        // This method shows Request and Product for a LineItem
        public async Task<ActionResult> GetLineItemForPR(int Requestid)
        {
            if (_context.LineItems == null)
            {
                return NotFound();
            }
            // var lineItem = await _context.LineItems.FindAsync(id);

            var lineItem = await _context.LineItems.Include(li => li.Product)
                                                   .Where(li => li.RequestId == Requestid).ToListAsync();
            if (lineItem == null)
            {
                return NotFound();
            }

            return Ok(lineItem);
        }
        // PUT: api/LineItems/5
     
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLineItem(int id, LineItem lineItem)
        {
            if (id != lineItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(lineItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await DoRecalculateRequestTotal(lineItem.RequestId);
                // recalculate the Request Total

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineItemExists(id))
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

        // POST: api/LineItems
        
        [HttpPost]
        public async Task<ActionResult<LineItem>> PostLineItem(LineItem lineItem)
        {
          if (_context.LineItems == null)
          {
              return Problem("Entity set 'PrsDbContext.LineItems'  is null.");
          }
            _context.LineItems.Add(lineItem);
            await _context.SaveChangesAsync();
            await DoRecalculateRequestTotal(lineItem.RequestId);

            return CreatedAtAction("GetLineItem", new { id = lineItem.Id }, lineItem);
            
        }

        // DELETE: api/LineItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLineItem(int id)
        {
            if (_context.LineItems == null)
            {
                return NotFound();
            }
            var lineItem = await _context.LineItems.FindAsync(id);
            if (lineItem == null)
            {
                return NotFound();
            }

            int requestId = lineItem.RequestId;
            _context.LineItems.Remove(lineItem);




            await _context.SaveChangesAsync();
            // recalculate the Request total
            await DoRecalculateRequestTotal(requestId);

            return NoContent();
        }

        private bool LineItemExists(int id)
        {
            return (_context.LineItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // method calculate total and update Request total

        //private decimal DoRecalculateRequestTotal(int requestId) {


        //    // calculate the sum of quantity times price of a product for all each request
        //    var total = _context.LineItems.Include(p => p.Product)
        //                                           .Where(li => li.RequestId == requestId) 
        //                                           .Sum((li) => li.Quantity * li.Product.Price);

        //    // find the request, update the Total and SaveChanges

        //    var request1 = _context.Requests.Where(re => re.Id == requestId).FirstOrDefault();

        //    request1.Total = total;



        //    _context.SaveChangesAsync();
        //    return total;

        private async Task<decimal>DoRecalculateRequestTotal(int requestId)
        {


            // calculate the sum of quantity times price of a product for all each request
            var total =  _context.LineItems.Include(p => p.Product)
                                                   .Where(li => li.RequestId == requestId)
                                                   .Sum((li) => li.Quantity * li.Product.Price);

            // find the request, update the Total and SaveChanges

            var request1 = await _context.Requests.Where(re => re.Id == requestId).FirstOrDefaultAsync();


             request1.Total = total;



            await _context.SaveChangesAsync();
            return  total;

        }
    }
}

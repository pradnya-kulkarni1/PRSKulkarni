﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PRSKulkarni.Models;

namespace PRSKulkarni.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {

        const string statusRejected = "REJECTED";
        const string statusApproved = "APPROVED";
        const string statusNew = "NEW";
        const string statusReview = "REVIEW";

        private readonly PrsDbContext _context;

        public RequestsController(PrsDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {
          if (_context.Requests == null)
          {
              return NotFound();
          }
            return await _context.Requests.Include(r => r.User).ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {
          if (_context.Requests == null)
          {
              return NotFound();
          }
            //var request = await _context.Requests.FindAsync(id);
            

            var request = await _context.Requests.Include( r => r.User).FirstOrDefaultAsync(r => r.Id == id);
            if (request == null)
            {
                return NotFound();
            }

          

            return request;
        }

        // GET: api/Requests/5
        [HttpGet("Reviews/{id}")]
        public async Task<ActionResult<IEnumerable<Request>>> GetReviewRequests (int id)
        {
            if (_context.Requests == null)
            {
                return NotFound();
            }
            //var request = await _context.Requests.FindAsync(id);
            
           var userRequests = await _context.Requests.Include(r => r.User).Where(r => r.UserId != id && r.Status == statusReview).ToListAsync();

            return userRequests;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
          if (_context.Requests == null)
          {
              return Problem("Entity set 'PrsDbContext.Requests'  is null.");
          }
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.Id }, request);
        }
        [HttpPost("reject/{RequestId}")]
        public async Task<ActionResult<Request>> Reject (int requestid, [FromBody] string Reason) //([FromBody]
        {
            var req = await _context.Requests.FindAsync(requestid);
            if (req == null)
            {
                return NotFound();
            }

            req.ReasonForRejection = Reason;

            req.Status = statusRejected;

            //if(req.Status.ToUpper() == "REJECTED")
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 

            {
                return Problem(ex.Message);
            }
            //return CreatedAtAction("GetRequest", new { id = requestid }, req);
            return req;
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            if (_context.Requests == null)
            {
                return NotFound();
            }
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

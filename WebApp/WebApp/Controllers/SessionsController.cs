using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using WebApp;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("rest/[controller]")]
    [ApiController]
    public class SessionsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SessionsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Sessions
        [HttpGet]
        public IEnumerable<SessionRec> GetSessionRecs()
        {
            if (_context.SessionRecs.ToList().Count == 0)
            {
                InitData();
            }

            return _context.SessionRecs;
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSessionRec([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sessionRec = await _context.SessionRecs.FindAsync(id);

            if (sessionRec == null)
            {
                return NotFound();
            }

            return Ok(sessionRec);
        }

        // PUT: api/Sessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSessionRec([FromRoute] int id, [FromBody] SessionRec sessionRec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sessionRec.Id)
            {
                return BadRequest();
            }

            _context.Entry(sessionRec).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionRecExists(id))
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

        // POST: api/Sessions
        [HttpPost]
        public async Task<IActionResult> PostSessionRec([FromBody] SessionRec sessionRec)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SessionRecs.Add(sessionRec);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSessionRec", new { id = sessionRec.Id }, sessionRec);
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessionRec([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sessionRec = await _context.SessionRecs.FindAsync(id);
            if (sessionRec == null)
            {
                return NotFound();
            }

            _context.SessionRecs.Remove(sessionRec);
            await _context.SaveChangesAsync();

            return Ok(sessionRec);
        }

        private bool SessionRecExists(int id)
        {
            return _context.SessionRecs.Any(e => e.Id == id);
        }

        private void InitData()
        {
            string file;
            var assembly = Assembly.GetEntryAssembly();
            string[] resources = assembly.GetManifestResourceNames(); // debugging purposes only to get list of embedded resources
            using (var stream = assembly.GetManifestResourceStream("WebApp.Data.sessions.json"))
            {
                using (var reader = new StreamReader(stream))
                {
                    file = reader.ReadToEnd();
                }
            }
            List<SessionRec> sessionRecs = JsonConvert.DeserializeObject<SessionRec[]>(file).ToList();
            foreach (var session in sessionRecs)
            {
                _context.SessionRecs.Add(session);
            }
            _context.SaveChanges();
            return;
        }
    }
}
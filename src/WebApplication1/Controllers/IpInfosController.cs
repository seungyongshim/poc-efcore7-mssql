using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EfCore;
using EfCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IpInfosController : ControllerBase
    {
        private IIpInfosDbContext Context { get; }

        public IpInfosController(IIpInfosDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IpInfo>>> GetIpInfos()
        {
            return await Context.IpInfos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IpInfo>> GetIpInfo(int id)
        {
            var ipInfo = await Context.IpInfos.FindAsync(id);

            if (ipInfo == null)
            {
                return NotFound();
            }

            return ipInfo;
        }

        // PUT: api/IpInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIpInfo(int id, IpInfo ipInfo)
        {
            if (id != ipInfo.IpInfoId)
            {
                return BadRequest();
            }

            Context.IpInfos.Update(ipInfo);

            //_context.Entry(ipInfo).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IpInfoExists(id))
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

        // POST: api/IpInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IpInfo>> PostIpInfo(IpInfo ipInfo)
        {
            Context.IpInfos.Add(ipInfo);
            await Context.SaveChangesAsync();

            return CreatedAtAction("GetIpInfo", new { id = ipInfo.IpInfoId }, ipInfo);
        }

        // DELETE: api/IpInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIpInfo(int id)
        {
            var ipInfo = await Context.IpInfos.FindAsync(id);
            if (ipInfo == null)
            {
                return NotFound();
            }

            Context.IpInfos.Remove(ipInfo);
            await Context.SaveChangesAsync();

            return NoContent();
        }

        private bool IpInfoExists(int id)
        {
            return Context.IpInfos.Any(e => e.IpInfoId == id);
        }
    }
}

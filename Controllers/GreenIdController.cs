using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreenIdController : ControllerBase
    {
        public readonly GreenIdContext _greenIdContext;
        public GreenIdController(GreenIdContext greenIdContext)
        {
            _greenIdContext = greenIdContext;

        }
        [HttpGet]
        public async Task<ActionResult<List<Tenant>>> GetTenants()
        {
            List<Tenant> TenantList = new List<Tenant>();

            var result = await _greenIdContext.Tenants.ToListAsync();
            if (result != null)
            {
                return result;

            }
            return NotFound();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Tenant>> GetTenantsById(int id)
        {
            Tenant tenantresponse = new Tenant();
            if (id != null)
            {
                var result = await _greenIdContext.Tenants.FirstOrDefaultAsync(x => x.TenantId == id);
                if (result != null)
                {
                    return result;

                }
                return NoContent();
            }
            return NoContent();


        }
        [HttpPost]
        public async Task<ActionResult> CreateTenant(Tenant request)
        {
            if (request != null)
            {
                var result = await _greenIdContext.Tenants.FirstOrDefaultAsync(x => x.TenantId == request.TenantId);
                if (result == null)
                {
                    await _greenIdContext.Tenants.AddAsync(request);
                    await _greenIdContext.SaveChangesAsync();
                    return Ok();

                }
                return Conflict();


            }
            return BadRequest();
        }

        [HttpDelete("id")]
        public async Task<ActionResult> Deletetenant(int id)
        {
            if (id != null)
            {
                var result = await _greenIdContext.Tenants.FirstOrDefaultAsync(x => x.TenantId == id);
                if (result != null)
                {
                    _greenIdContext.Tenants.Remove(result);
                    await _greenIdContext.SaveChangesAsync();
                    return Ok();

                }
                return NoContent();
            }
            return NoContent();
        }

        //[HttpPut("id")]
        //public async Task<ActionResult> UpdateTenant(Tenant request,int id)
        //{
        //    if (id != null)
        //    {
        //        var result = await _greenIdContext.Tenants.FirstOrDefaultAsync(x => x.TenantId == id);
        //        if (result != null)
        //        {
        //            //_greenIdContext.Entry(request).State = EntityState.Modified;


        //            _greenIdContext.Tenants.Update(result);
        //            await _greenIdContext.SaveChangesAsync();
        //            return Ok();

        //        }
        //        return Conflict();
        //    }
        //    return NoContent();
        //}

        [HttpPut]
        public async Task<IActionResult> EditTenant(int id,Tenant request)
        {
            if (request.TenantId != id)
                return BadRequest("ID mismatch");

            bool exists = await _greenIdContext.Tenants.AnyAsync(x => x.TenantId == id);
            if (!exists)
                return NotFound();

            _greenIdContext.Tenants.Update(request);
            await _greenIdContext.SaveChangesAsync();

            return NoContent();
        }
    }
        
}

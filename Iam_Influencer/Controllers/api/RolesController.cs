using Iam_Influencer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Iam_Influencer.Controllers.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ModelContext _context;
        public RolesController(ModelContext context)
        {
            _context = context;
        }

        [HttpDelete]
        public IActionResult DeleteRole(long id)
        {
            var roles = _context.Roles.ToList();
            var role = roles.Find(r => r.Id == id);

            if (role == null) return NotFound();
            try
            {
                _context.Roles.Remove(role);
                _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}

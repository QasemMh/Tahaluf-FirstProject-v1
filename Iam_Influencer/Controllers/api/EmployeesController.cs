using Iam_Influencer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iam_Influencer.Controllers.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly ModelContext _context;
        public EmployeesController(ModelContext context)
        {
            _context = context;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var user = await _context.Logins.Where(e => e.Id == id).FirstOrDefaultAsync();

            if (user == null) return NotFound();
            try
            {
                long accId = (long)user.AccountatntId;

                _context.Logins.Remove(user);

                var employee = await _context.Employees.Where(w => w.Id == accId).FirstOrDefaultAsync();

                if (employee == null) return NotFound();


                _context.Employees.Remove(employee);


                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
    }
}

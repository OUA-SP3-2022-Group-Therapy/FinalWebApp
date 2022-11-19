using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GroupTherapyWebAppFinal.Data;
using GroupTherapyWebAppFinal.Models;

namespace GroupTherapyWebAppFinal.Views.UserModels
{
    public class DetailsModel : PageModel
    {
        private readonly GroupTherapyWebAppFinal.Data.ApplicationDbContext _context;

        public DetailsModel(GroupTherapyWebAppFinal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public UserModel UserModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserModels == null)
            {
                return NotFound();
            }

            var usermodel = await _context.UserModels.FirstOrDefaultAsync(m => m.UserModelID == id);
            if (usermodel == null)
            {
                return NotFound();
            }
            else 
            {
                UserModel = usermodel;
            }
            return Page();
        }
    }
}

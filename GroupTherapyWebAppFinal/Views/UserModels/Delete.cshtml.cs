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
    public class DeleteModel : PageModel
    {
        private readonly GroupTherapyWebAppFinal.Data.ApplicationDbContext _context;

        public DeleteModel(GroupTherapyWebAppFinal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.UserModels == null)
            {
                return NotFound();
            }
            var usermodel = await _context.UserModels.FindAsync(id);

            if (usermodel != null)
            {
                UserModel = usermodel;
                _context.UserModels.Remove(UserModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

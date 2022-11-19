using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroupTherapyWebAppFinal.Data;
using GroupTherapyWebAppFinal.Models;

namespace GroupTherapyWebAppFinal.Views.UserModels
{
    public class EditModel : PageModel
    {
        private readonly GroupTherapyWebAppFinal.Data.ApplicationDbContext _context;

        public EditModel(GroupTherapyWebAppFinal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserModel UserModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.UserModels == null)
            {
                return NotFound();
            }

            var usermodel =  await _context.UserModels.FirstOrDefaultAsync(m => m.UserModelID == id);
            if (usermodel == null)
            {
                return NotFound();
            }
            UserModel = usermodel;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(UserModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(UserModel.UserModelID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserModelExists(int id)
        {
          return _context.UserModels.Any(e => e.UserModelID == id);
        }
    }
}

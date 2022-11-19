using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GroupTherapyWebAppFinal.Data;
using GroupTherapyWebAppFinal.Models;

namespace GroupTherapyWebAppFinal.Views.UserModels
{
    public class CreateModel : PageModel
    {
        private readonly GroupTherapyWebAppFinal.Data.ApplicationDbContext _context;

        public CreateModel(GroupTherapyWebAppFinal.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public UserModel UserModel { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.UserModels.Add(UserModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

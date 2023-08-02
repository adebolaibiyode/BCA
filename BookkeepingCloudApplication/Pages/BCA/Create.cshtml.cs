using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookkeepingCloudApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class CreateModel : PageModel
    {
        private readonly BookkeepingCloudApplication.Data.ApplicationDbContext _context;

        //public CreateModel(BookkeepingCloudApplication.Data.ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
       

        public CreateModel(
            BookkeepingCloudApplication.Data.ApplicationDbContext context,
            Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            // If the user is logged in, user will be non-null.
            if (user != null)
            {
                // Fetch max Invoice Number from the database and increment by 1
                var maxInvoiceNumber = _context.Invoices.Max(i => i.InvoiceNumber);
                var newInvoiceNumber = maxInvoiceNumber + 1;

                // Set DateEntered to today's date and set new invoice number.
                Invoice = new Invoice
                {
                    DateEntered = DateTime.Now,
                    InvoiceNumber = newInvoiceNumber,
                    EnteredBy = user.Email
                };
            }

            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Invoices == null || Invoice == null)
            {
                return Page();
            }

            _context.Invoices.Add(Invoice);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookkeepingCloudApplication.Models;
using Microsoft.AspNetCore.Identity;
using BookkeepingCloudApplication.Managers;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class CreateModel : PageModel
    {

        private readonly IInvoiceManager _invoiceManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Constructor for initializing the CreateModel class with dependencies.
        /// </summary>
        /// <param name="invoiceManager">The invoice manager instance.</param>
        /// <param name="userManager">The user manager instance.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor instance.</param>
        public CreateModel(
            IInvoiceManager invoiceManager,
            Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _invoiceManager = invoiceManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Handles the HTTP GET request for creating a new invoice asynchronously.
        /// </summary>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

            // If the user is logged in, user will be non-null.
            if (user != null)
            {
                // Set DateEntered to today's date and set new invoice number.
                Invoice = new Invoice
                {
                    DateEntered = DateTime.Now,
                    EnteredBy = user.Email
                };
            }

            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;

        /// <summary>
        /// Handles the HTTP POST request for creating a new invoice asynchronously.
        /// </summary>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _invoiceManager.GetIsInvoicesNull() || Invoice == null)
            {
                 return Page();
            }

            _invoiceManager.CreateInvoice(Invoice);

            return RedirectToPage("/BCA/Details", new { id = Invoice.Id });

        }
    }
}

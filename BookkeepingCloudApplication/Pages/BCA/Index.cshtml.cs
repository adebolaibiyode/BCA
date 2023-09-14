using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookkeepingCloudApplication.Models;
using BookkeepingCloudApplication.Managers;
using Microsoft.AspNetCore.Identity;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class IndexModel : PageModel
    {
        private readonly IInvoiceManager _invoiceManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;


        /// <summary>
        /// Constructor for initializing the IndexModel class with dependencies.
        /// </summary>
        /// <param name="invoiceManager">The invoice manager instance.</param>
        /// /// <param name="userManager">The user manager instance.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor instance.</param>
        public IndexModel(IInvoiceManager invoiceManager,
            Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _invoiceManager = invoiceManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Invoice> Invoice { get;set; } = default!;

        /// <summary>
        /// Handles the HTTP GET request for the index page asynchronously.
        /// Retrieves invoices if they are not null in the invoice manager.
        /// </summary>
        public async Task OnGetAsync()
        {
            if (!_invoiceManager.GetIsInvoicesNull())
            {
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);

                // If the user is logged in, user will be non-null.
                if (user != null)
                {
                    Invoice = _invoiceManager.GetInvoices(int.MaxValue, 0,user.Email);
                }
                
            }
        }
    }
}

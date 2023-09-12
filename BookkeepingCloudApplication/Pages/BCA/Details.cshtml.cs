using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookkeepingCloudApplication.Models;
using BookkeepingCloudApplication.Managers;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class DetailsModel : PageModel
    {
        private readonly IInvoiceManager _invoiceManager;

        /// <summary>
        /// Constructor for initializing the DetailsModel class with dependencies.
        /// </summary>
        /// <param name="invoiceManager">The invoice manager instance.</param>
        public DetailsModel(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        public Invoice Invoice { get; set; } = default!;

        /// <summary>
        /// Handles the HTTP GET request for displaying the details of an invoice asynchronously.
        /// </summary>
        /// <param name="id">Optional ID parameter for specifying the invoice to display.</param>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _invoiceManager.GetIsInvoicesNull())
            {
                return NotFound();
            }

            var invoice = _invoiceManager.GetInvoiceById((int)id);
            if (invoice == null)
            {
                return NotFound();
            }
            else 
            {
                Invoice = invoice;
            }
            return Page();
        }
    }
}

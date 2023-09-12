using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookkeepingCloudApplication.Models;
using BookkeepingCloudApplication.Managers;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class DeleteModel : PageModel
    {
        private readonly IInvoiceManager _invoiceManager;

        /// <summary>
        /// Constructor for initializing the DeleteModel class with dependencies.
        /// </summary>
        /// <param name="invoiceManager">The invoice manager instance.</param>
        public DeleteModel(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        [BindProperty]
      public Invoice Invoice { get; set; } = default!;

        /// <summary>
        /// Handles the HTTP GET request for displaying an invoice to delete.
        /// </summary>
        /// <param name="id">Optional ID parameter for specifying the invoice to delete.</param>
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

        /// <summary>
        /// Handles the HTTP POST request for deleting an invoice asynchronously.
        /// </summary>
        /// <param name="id">Optional ID parameter for specifying the invoice to delete.</param>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _invoiceManager.GetIsInvoicesNull())
            {
                return NotFound();
            }

            _invoiceManager.DeleteInvoiceById((int)id);

            return RedirectToPage("./Index");
        }
    }
}

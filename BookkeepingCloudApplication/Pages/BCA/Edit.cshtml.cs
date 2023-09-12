using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookkeepingCloudApplication.Models;
using BookkeepingCloudApplication.Managers;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class EditModel : PageModel
    {
        private readonly IInvoiceManager _invoiceManager;

        /// <summary>
        /// Constructor for initializing the EditModel class with dependencies.
        /// </summary>
        /// <param name="invoiceManager">The invoice manager instance.</param>
        public EditModel(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;

        /// <summary>
        /// Handles the HTTP GET request for editing an invoice asynchronously.
        /// </summary>
        /// <param name="id">Optional ID parameter for specifying the invoice to edit.</param>
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
            Invoice = invoice;
            return Page();
        }

        /// <summary>
        /// Handles the HTTP POST request for updating an invoice asynchronously.
        /// </summary>
        /// <returns>An IActionResult representing the result of the operation.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _invoiceManager.UpdateInvoice(Invoice);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_invoiceManager.GetInvoiceById((int)Invoice.Id) == null)
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
    }
}

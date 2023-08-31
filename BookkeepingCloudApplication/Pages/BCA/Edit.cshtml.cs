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

        public EditModel(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;

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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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

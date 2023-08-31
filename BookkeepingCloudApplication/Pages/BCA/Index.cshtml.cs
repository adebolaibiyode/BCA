using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookkeepingCloudApplication.Models;
using BookkeepingCloudApplication.Managers;

namespace BookkeepingCloudApplication.Pages.BCA
{
    public class IndexModel : PageModel
    {
        private readonly IInvoiceManager _invoiceManager;

        public IndexModel(IInvoiceManager invoiceManager)
        {
            _invoiceManager = invoiceManager;
        }

        public IList<Invoice> Invoice { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (!_invoiceManager.GetIsInvoicesNull())
            {
                Invoice = _invoiceManager.GetInvoices(int.MaxValue, 0);
            }
        }
    }
}

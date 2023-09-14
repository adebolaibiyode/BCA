using BookkeepingCloudApplication.Data;
using BookkeepingCloudApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookkeepingCloudApplication.Managers
{
    public class InvoiceManager : IInvoiceManager
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Creates a new instance of the Invoice Manager class. The Invoice manager is responsible for business logic and database access.
        /// </summary>
        /// <param name="context"></param>
        public InvoiceManager (ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public bool CreateInvoice(Invoice invoice)
        {
            // TODO - additional model validation.

            // Fetch max Invoice Number from the database and increment by 1
            int newInvoiceNumber;
            try
            {
                int maxInvoiceNumber = GetMaximumInvoiceNumber();
                newInvoiceNumber = maxInvoiceNumber + 1;
            }
            catch (Exception ex)
            {
                newInvoiceNumber = 1;
            }

            // this caused an identity runtime error.Invoice.Id is an identity column and should not be passed to
            //invoice.Id = newInvoiceNumber;

            //correct implementation :AI
            invoice.InvoiceNumber = newInvoiceNumber;

            _context.Invoices.Add(invoice);
            return SaveChanges();
        }

        /// <inheritdoc/>
        public bool DeleteInvoiceById(int id)
        {
            var dbInvoice = _context.Invoices.FirstOrDefault(x => x.Id == id);
            if (dbInvoice != null)
            {
                _context.Invoices.Remove(dbInvoice);
            }
            return SaveChanges();
        }

        /// <inheritdoc/>
        public Invoice GetInvoiceById(int id)
        {
            return _context.Invoices.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <inheritdoc/>

        //Retrieves all invoices
        public IList<Invoice> GetInvoices(uint count, uint page,string username="")
        {
            if (username != "")
            {
                return _context.Invoices.Where(i => i.EnteredBy == username).OrderBy(i => i.InvoiceNumber).Skip((int)(page * count)).Take((int)count).ToList();
            }
            else
            {
                return _context.Invoices.OrderBy(i => i.InvoiceNumber).Skip((int)(page * count)).Take((int)count).ToList();
            }
        }

        public InvoiceModel GetInvoiceByInvoiceNumber(int invoicenumber)
        {
            IList<Invoice> invoices = _context.Invoices.Where(i => i.InvoiceNumber == invoicenumber).ToList();

            // Create an InvoiceHeader object from the first invoice
            InvoiceHeader invoiceHeader = new InvoiceHeader
            {
                InvoiceType = invoices[0].InvoiceType,
                InvoiceDate = invoices[0].InvoiceDate,
                InvoiceNumber = invoices[0].InvoiceNumber,
                InvoiceAccount = invoices[0].InvoiceAccount,
                InvoiceControlAccount = invoices[0].InvoiceControlAccount,
                InvoiceStatus = invoices[0].InvoiceStatus,
                AccountEmail = invoices[0].AccountEmail,
                AccountName = invoices[0].AccountName,
                AccountReference = invoices[0].AccountReference,
                AccountType = invoices[0].AccountType,
                CurrencyCode = invoices[0].CurrencyCode              
            };

            // Create InvoiceLine objects from all invoices
            List<InvoiceLine> invoiceLines = invoices.Select(invoice => new InvoiceLine
            {
                InvoiceItem = invoice.InvoiceItem,
                Description = invoice.Description,
                Quantity = invoice.Quantity,
                UnitPrice = invoice.UnitPrice,
                Id = invoice.Id,
                GrossAmount = invoice.GrossAmount,
                NetAmount = invoice.NetAmount,
                TaxAmount = invoice.TaxAmount,
                TaxCode = invoice.TaxCode,
                EnteredBy = invoice.EnteredBy,
                DateEntered = invoice.DateEntered
                
            }).ToList();

            // Create an Invoice object and set the InvoiceHeader and InvoiceLines
            InvoiceModel finalInvoice = new InvoiceModel
            {
                InvoiceHeader = invoiceHeader,
                InvoiceLines = invoiceLines,
            };
            return finalInvoice;
        }

        /// <inheritdoc/>
        public bool GetIsInvoicesNull()
        {
            return _context.Invoices == null ? true : false;
        }

        /// <inheritdoc/>
        //public int GetMaximumInvoiceNumber()
        //{
        //    return _context.Invoices.Max(i => i.InvoiceNumber);
        //}

        //updated GetMaximumInvoiceNumber() method to handle the case when there are no invoices in the database:AI
        public int GetMaximumInvoiceNumber()
        {
            return _context.Invoices.Any() ? _context.Invoices.Max(i => i.InvoiceNumber) : 0;
        }

        public bool AddInvoiceLine(Invoice invoice)
        {
            // TODO - additional model validation.

            _context.Invoices.Add(invoice);
            return SaveChanges();
        }

        /// <inheritdoc/>
        public bool UpdateInvoice(Invoice invoice)
        {
            _context.Attach(invoice).State = EntityState.Modified;
            return SaveChanges();
        }

        /// <summary>
        /// Saves changes in the database and returns true if more than one row was updated.
        /// </summary>
        /// <returns>Indicates if the save was successful</returns>
        private bool SaveChanges()
        {
            return _context.SaveChanges() > 0 ? true : false;
        }
    }
}

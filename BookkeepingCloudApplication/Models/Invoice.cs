namespace BookkeepingCloudApplication.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceType { get; set; } = "SI"; //SI or PI
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public int InvoiceNumber { get; set; }
        public string InvoiceStatus { get; set; } = "Not Paid"; //Paid or Not Paid
        public string InvoiceControlAccount { get; set; } = "DRCTL"; //DRCTL or CRCTL
        public string InvoiceAccount { get; set; } = "Sales"; //Sales or Cost of Sale
        public string AccountName { get; set; } = string.Empty;
        public string AccountEmail { get; set; } = string.Empty;
        public string AccountReference { get; set; } = string.Empty;
        public string AccountType { get; set; } = "Customer"; //Customer or Supplier
        public string InvoiceItem { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public double UnitPrice { get; set; }
        public double NetAmount { get; set; }
        public double TaxCode { get; set; } = 0.2; //20%
        public double TaxAmount { get; set; }
        public double GrossAmount { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;
        public DateTime DateEntered { get; set; } = DateTime.Now;
        public string EnteredBy { get; set; }
    }

    public class InvoiceHeader
    {

        public string InvoiceType { get; set; } = "SI"; //SI or PI
        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public int InvoiceNumber { get; set; }
        public string InvoiceStatus { get; set; } = "Not Paid"; //Paid or Not Paid
        public string InvoiceControlAccount { get; set; } = "DRCTL"; //DRCTL or CRCTL
        public string InvoiceAccount { get; set; } = "Sales"; //Sales or Cost of Sale
        public string AccountName { get; set; } = string.Empty;
        public string AccountEmail { get; set; } = string.Empty;
        public string AccountReference { get; set; } = string.Empty;
        public string AccountType { get; set; } = "Customer"; //Customer or Supplier
        public string CurrencyCode { get; set; } = string.Empty;     
    }

    public class InvoiceLine
    {
        public int Id { get; set; }     
        public string InvoiceItem { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; } = 1;
        public double UnitPrice { get; set; }
        public double NetAmount { get; set; }
        public double TaxCode { get; set; } = 0.2; //20%
        public double TaxAmount { get; set; }
        public double GrossAmount { get; set; }
        public DateTime DateEntered { get; set; } = DateTime.Now;
        public string? EnteredBy { get; set; }
    }

    public class InvoiceModel
    {
        public int Id { get; set; }
        public InvoiceHeader InvoiceHeader { get; set; }
        public IList<InvoiceLine> InvoiceLines { get; set; }
    }
}

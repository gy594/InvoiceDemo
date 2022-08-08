using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Dto
{
    public class Invoice
    {
        public string CustomerName { get; set; }
        public DateTime IssuedDate { get; set; }
        public decimal OriginalAmount { get; set; }
        public decimal OutstandingAmount { get; set; }
    }
    public class InvoicesPageViewModel
    {
        public IEnumerable<Invoice> Invoices { get; set; }
        public string CustomerId { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}

using Service.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InvoiceDemo.Pages
{
    public class InvoiceModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IInvoiceService _invoiceService;

        public InvoiceModel(ILogger<IndexModel> logger, IInvoiceService invoiceService)
        {
            _logger = logger;
            _invoiceService = invoiceService;
        }
        public IEnumerable<Service.Dto.Invoice> InvoicesModel { get; set; }

        public async Task OnGetAsync(int id)
        {
            InvoicesModel = await _invoiceService.GetInvoicesByAccount(id);
        }


    }
}

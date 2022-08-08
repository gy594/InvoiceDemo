using Microsoft.AspNetCore.Mvc.RazorPages;
using Service.Interface;

namespace InvoiceDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHealthyAccountService _healthyAccountService;
        private const int pageSzie = 10;

        public IndexModel(ILogger<IndexModel> logger, IHealthyAccountService healthyAccountService)
        {
            _logger = logger;
            _healthyAccountService = healthyAccountService;
        }

        public Service.Dto.AccountsPage AccountsModel { get; set; }

        public async Task OnGetAsync(int? pageNumber)
        {
            AccountsModel = await _healthyAccountService.GetAccounts(pageNumber, pageSzie);
        }
    }
}
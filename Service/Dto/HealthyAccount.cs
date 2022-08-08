namespace Service.Dto
{
    public class HealthyAccount
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool? IsHealthy { get; set; }
        public DateTime LastUpdated { get; set; }
    }
    public class AccountsPage
    {
        public IEnumerable<HealthyAccount>? Accounts { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }
}


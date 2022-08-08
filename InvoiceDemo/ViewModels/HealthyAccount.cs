namespace InvoiceDemo.ViewModels
{
    public class HealthyAccount
    {
        public string CustomerName { get; set; }
        public bool? IsHealthy { get; set; }
        public decimal SumOfAmount { get; set; }
        public bool? OutstandingAmountOver90Days { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}

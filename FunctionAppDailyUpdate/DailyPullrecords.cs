using Domain.Models;
using Flurl;
using Flurl.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FunctionAppDailyUpdate
{
    // the idea here is using Function App to reduce the workloads and do it daily
    public class DailyPullrecords
    {
        [FunctionName("DailyPullrecords")]
        public async Task Run([TimerTrigger("0 0 23 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Timer trigger DailyPullrecords executed at: {DateTime.Now}");
            var invoices = await GetDailyInvoiceList();
            try
            {
                var table = ToDataTable(invoices.ToList());
                var connString = Environment.GetEnvironmentVariable("ConnectionString");
                BulkInsertIntoTable(table, connString);
                log.LogInformation($"DailyPullrecords completed: {DateTime.Now}");

            }
            catch (Exception e)
            {
                log.LogError(e.Message);
            }
        }

        public async Task<IEnumerable<Invoice>> GetDailyInvoiceList()
        {
            var token = Environment.GetEnvironmentVariable("Token");
            var integrationUrl = Environment.GetEnvironmentVariable("IntegrationUrl");

            try
            {
                var to = DateTime.Now;
                var from = to.AddDays(-1);
                var invoices = await integrationUrl
                    .SetQueryParam("token", token)
                    .SetQueryParam("from", from.ToShortDateString())
                    .SetQueryParam("to", to.ToShortDateString())
                    .AllowAnyHttpStatus()
                    .WithHeader("Accept", "application/json")
                    .GetJsonAsync<List<Invoice>>();
                return invoices;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        
        // using bulk copy to copy over rather than using EF insert
        public void BulkInsertIntoTable(DataTable dataTable, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                SqlTransaction transaction = null;
                connection.Open();
                try
                {
                    transaction = connection.BeginTransaction();
                    using (var sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock, transaction))
                    {
                        sqlBulkCopy.DestinationTableName = "invoices";
                        sqlBulkCopy.ColumnMappings.Add("Id", "Id");
                        sqlBulkCopy.ColumnMappings.Add("CustomerId", "CustomerId");
                        sqlBulkCopy.ColumnMappings.Add("CustomerName", "CustomerName");
                        sqlBulkCopy.ColumnMappings.Add("IssuedDate", "IssuedDate");
                        sqlBulkCopy.ColumnMappings.Add("OriginalAmount", "OriginalAmount");
                        sqlBulkCopy.ColumnMappings.Add("OutstandingAmount", "OutstandingAmount");

                        sqlBulkCopy.WriteToServer(dataTable);
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }

            }
        }

    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewPrepsCS.AfricanIdea.GroupingAlgo
{
    public class SalesReport
    {
        public string Province { get; set; }
        public string CustomerName { get; set; }
        public decimal SalesValue { get; set; }
    }

    public class SalesAnalyzer
    {
        public static List<SalesReport> GenerateSalesReport(List<SalesReport> salesData)
        {
            List<SalesReport> report = new List<SalesReport>();
            string currentProvince = null;
            decimal totalSalesValue = 0;

            foreach (SalesReport sales in salesData)
            {
                if (currentProvince == null)
                {
                    currentProvince = sales.Province;
                }
                else if (currentProvince != sales.Province)
                {
                    // Output the accumulated sales value for the previous province
                    SalesReport provinceTotal = new SalesReport
                    {
                        Province = currentProvince,
                        CustomerName = "Total Sales Value",
                        SalesValue = totalSalesValue
                    };
                    report.Add(provinceTotal);

                    // Reset the total sales value for the new province
                    currentProvince = sales.Province;
                    totalSalesValue = 0;
                }

                // Output the individual customer name and sales value
                report.Add(sales);

                // Accumulate the sales value for the province
                totalSalesValue += sales.SalesValue;
            }

            // Output the accumulated sales value for the last province
            SalesReport lastProvinceTotal = new SalesReport
            {
                Province = currentProvince,
                CustomerName = "Total Sales Value",
                SalesValue = totalSalesValue
            };
            report.Add(lastProvinceTotal);

            return report;
        }

        public static void Main(string[] args)
        {
            // Sample data
            List<SalesReport> salesData = new List<SalesReport>
            {
                new SalesReport { Province = "Gauteng", CustomerName = "Michael", SalesValue = 100 },
                new SalesReport { Province = "Gauteng", CustomerName = "Shepherd", SalesValue = 200 },
                new SalesReport { Province = "Western Cape", CustomerName = "Zoe", SalesValue = 300 },
                new SalesReport { Province = "Western Cape", CustomerName = "Aimee", SalesValue = 400 },
                new SalesReport { Province = "Mpumalanga", CustomerName = "Nathan", SalesValue = 500 },
                new SalesReport { Province = "Mpumalanga", CustomerName = "Mary", SalesValue = 600 }
            };

            List<SalesReport> salesReport = GenerateSalesReport(salesData);

            // Output the sales report
            foreach (SalesReport report in salesReport)
            {
                Console.WriteLine($"{report.Province}, {report.CustomerName}: {report.SalesValue}");
            }
        }
    }
}

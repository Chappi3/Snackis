using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisWebApp.Gateways;
using SnackisWebApp.Models;
using System.Threading.Tasks;

namespace SnackisWebApp.Pages.Admin.Reports
{
    public class ManageReportModel : PageModel
    {
        private readonly ReportGateway _reportGateway;

        public Report Report { get; set; }

        public ManageReportModel(ReportGateway reportGateway)
        {
            _reportGateway = reportGateway;
        }

        public async Task<IActionResult> OnGetAsync(string reportId)
        {
            if (reportId == null)
            {
                return NotFound();
            }

            Report = await _reportGateway.GetReportById(reportId);

            return Page();
        }
    }
}

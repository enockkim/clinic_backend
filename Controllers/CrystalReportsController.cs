using clinic.Reports;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using CrystalDecisions.Shared;
using System.IO;

namespace clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CrystalReportsController : Controller
    {
        //[Route("Financial/VarianceAnalysisReport")]
        //[ClientCacheWithEtag(60)]  //1 min client side caching
        [HttpGet("GetActivePatientReport")]
        public HttpResponseMessage GetActivePatientReport()
        {
            string reportPath = "~/Reports/Financial";
            string reportFileName = "YTDVarianceCrossTab.rpt";
            string exportFilename = "YTDVarianceCrossTab.pdf";

            HttpResponseMessage result = CrystalReport.RenderReport(reportPath, reportFileName, exportFilename);
            return result;
        }

        [HttpGet("RenderPatientsReport")]
        public IActionResult RenderPatientsReport([FromQuery] string reportPath)
        {
            // Initialize Crystal Reports runtime
            ReportDocument report = new ReportDocument();
            report.Load(reportPath);
            report.SetDatabaseLogon("remote4", "$C3u+X[Nm-gg6E!j", "localhost", "clinic");

            // Set data source
            report.SetDataSource("remote");

            // Set report parameters
            //foreach (KeyValuePair<string, object> param in reportParams)
            //{
            //    report.SetParameterValue(param.Key, param.Value);
            //}

            // Render report using a Crystal Reports Viewer
            var stream = report.ExportToStream(ExportFormatType.CrystalReport);
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return File(buffer, "application/octet-stream", reportPath);
        }
    }
}

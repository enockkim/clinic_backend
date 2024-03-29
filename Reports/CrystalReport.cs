﻿
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
//using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Net;
using System.Net.Http;

namespace clinic.Reports
{
    public static class CrystalReport
    {
        public static HttpResponseMessage RenderReport(string reportPath, string reportFileName, string exportFilename)
        {
            var rd = new ReportDocument();

            rd.Load(Path.Combine(reportPath, reportFileName));
            MemoryStream ms = new MemoryStream();
            using (var stream = rd.ExportToStream(ExportFormatType.PortableDocFormat))
            {
                stream.CopyTo(ms);
            }

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(ms.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = exportFilename
                };
            result.Content.Headers.ContentType =
                new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");
            return result;
        }
    }
}
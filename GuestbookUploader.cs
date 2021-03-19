using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.File.Protocol;
using System.Net.Http;

namespace Blueghost.Wedding.Guestbook
{
    public static class GuestbookUploader
    {
        [FunctionName("GuestbookUploader")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            [Blob("guestbook/{name}", FileAccess.Write)] Stream entry,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string secret = req.Query["secret"];
            string name = req.Query["name"];

            await req.Body.CopyToAsync(entry);

            return new AcceptedResult();
        }
    }
}

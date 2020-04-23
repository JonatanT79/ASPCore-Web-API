using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CloudNine.Praktik.Controllers
{
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/products
        [Route("api/[controller]")]
        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync(int? page, int? pageSize)
        {
            // TODO: Returnera alla produkter, ta hänsyn till pagineringsparametrar om sådana skickats in.

            var webClient = new WebClient();
            var Json = webClient.DownloadString(@"C:\Users\jonat\OneDrive\Dokument\Cloudnine\Cloudnine-Codetest\Data\products.json");
            var convert = JsonConvert.SerializeObject(Json);

            // var h = convert.ToString().Where(e => e == 'j').ToString();

            return new string[] { convert };
        }

        // GET: api/products/5
   
    }
}
// snygga till utskriften av json listan
// använda where sats för att få fram specifierad id?
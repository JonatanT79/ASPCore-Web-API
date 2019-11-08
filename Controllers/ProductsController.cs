using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CloudNine.Praktik.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/products
        [HttpGet]
        public async Task<IEnumerable<string>> GetAsync(int? page, int? pageSize)
        {
            // TODO: Returnera alla produkter, ta hänsyn till pagineringsparametrar om sådana skickats in.
            return new string[] { "value1", "value2" };
        }

        // GET: api/products/5


        // GET: api/productColors

    }
}

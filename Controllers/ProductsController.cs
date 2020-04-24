using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CloudNine.Praktik.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CloudNine.Praktik.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductsController : ControllerBase
    {
        JsonData _jsonData = new JsonData();

        [HttpGet("[controller]/{Page=}/{pageSize=}/{Color=}")]
        // GET: api/products
        public async Task<IEnumerable<string>> GetAsync(int? page, int? pageSize, string Color)
        {
            //localhost:6600
            var Client = new WebClient();
            string Json = Client.DownloadString(JsonData.JsonFile);

            if (Color == null && page == null && pageSize == null)
            {
                var convert = JsonConvert.SerializeObject(Json);

                return new string[] { convert };
            }
            else if ((page != null && pageSize != null) && Color == null)
            {
                List<string> Pagelist = new List<string>();

                return _jsonData.GetPages(Json, Pagelist, page, pageSize);
            }
            else
            {
                List<string> stringlist = new List<string>();

                var Completelist = _jsonData.GetPagesFilteredByColor(Json, stringlist, page, pageSize, Color);

                if (stringlist.Count != 0)
                {
                    return Completelist;
                }
                else
                {
                    return new string[] { "No product have that color you entered" };
                }
            }
        }

        // GET: api/products/5
        [HttpGet("[controller]/{ID}")]
        public string GetProductsById(string ID)
        {
            JToken product = null;
            product = _jsonData.GetProductFilteredByID(ID, product);

            if (product == null)
            {
                return "No product with that ID found :(";
            }
            else
            {
                return product.ToString();
            }
        }

        // GET: api/productColors
        [HttpGet("productColors")]
        public IEnumerable<string> GetColors()
        {
            return _jsonData.GetColorList();
        }
    }
}

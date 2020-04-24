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
        [HttpGet("[controller]")]
        // GET: api/products
        public async Task<IEnumerable<string>> GetAsync(int? page, int? pageSize, string Color)
        {
            //localhost:6600
            // filtrera produkt med färg
            // TODO: Returnera alla produkter, ta hänsyn till pagineringsparametrar om sådana skickats in.
            var Client = new WebClient();
            string Json = Client.DownloadString(JsonData.JsonFile);
            var convert = JsonConvert.SerializeObject(Json);
            var split = convert.Split("},");

            return split.ToList();
        }

        // GET: api/products/5
        // **************************************************** gör query color först ************************************
        [HttpGet("[controller]/{ID}")]
        public List<JsonData> GetProductsById(string ID)
        {
            List<JsonData> list = new List<JsonData>();
            var client = new WebClient();
            string json = client.DownloadString(JsonData.JsonFile);

            var JsonArray = JArray.Parse(json);

            var query = from e in JsonArray
                       // where JsonArray["id"].ToString() == ID
                        select e;

            JsonData jd = new JsonData();
            foreach (var item in query)
            {
                jd.ID = item["id"].ToString();
                jd.ProductName = item["productName"].ToString();
                list.Add(jd);
            }

            return list;
        }

        // GET: api/productColors
        [HttpGet("productColors")]
        public IEnumerable<string> GetProductByColor(string ID)
        {
            var client = new WebClient();
            string json = client.DownloadString(JsonData.JsonFile);

            var JsonArray = JArray.Parse(json);
            List<string> Colorlist = new List<string>();
            for (int i = 0; i < JsonArray.Count; i++)
            {
                Colorlist.Add(JsonArray[i]["color"].ToString());
            }
            return Colorlist;
        }
    }
}
// snygga till utskriften av json listan
// använda where sats för att få fram specifierad id?
// skapa en metod för json --- senare
//spara strängen i en property i en json dataklass och skriv klass.sträng
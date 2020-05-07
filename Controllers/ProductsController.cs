using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductsController : ControllerBase
    {
        JsonData _jsonData = new JsonData();

        [HttpGet("[controller]/{Page=}/{pageSize=}/{Color=}")]
        // GET: api/products
        // This Endpoint is accessible by api/products ******************************************************************
        public async Task<IEnumerable<string>> GetAsync(int? page, int? pageSize, string Color)
        {
            //local host is localhost:6600 (might be dependent on your local host)
            var Client = new WebClient();

            //Set Json to the Jsonfile. JsonData.JsonFile is a const in class 'JsonData'
            string Json = Client.DownloadString(JsonData.JsonFile);

            //Check if URL is only api/products
            if (Color == null && page == null && pageSize == null)
            {
                //Searialize jsonobject to string
                var convert = JsonConvert.SerializeObject(Json);

                return new string[] { convert };
            }

            // check if URL is api/products/0/1 (aka page and pageSize is not null)
            else if ((page != null && pageSize != null) && Color == null)
            {
                List<string> Pagelist = new List<string>();

                return _jsonData.GetPages(Json, Pagelist, page, pageSize);
            }

            //Check if URL is api/products/0/1/ColorName (aka page, pageSize and Color is not null)
            else
            {
                List<string> stringlist = new List<string>();

                //Declare a 'Completelist' that is equal to the list that this method returns
                stringlist = _jsonData.GetPagesFilteredByColor(Json, stringlist, page, pageSize, Color);

                //Check if any product have that color that the user entered. List.Count() is empty if none product have that color
                if (stringlist.Count != 0)
                {
                    return stringlist;
                }
                else
                {
                    return new string[] { "No product have that color you entered" };
                }
            }
        }

        // GET: api/products/5
        //This Endpoint is accessible by api/products/IdOfProduct ********************************************************
        [HttpGet("[controller]/{ID}")]
        public string GetProductsById(string ID)
        {
            //Declare JToken
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
        //This Endpoint is accessible by api/productColors **************************************************************
        [HttpGet("productColors")]
        public IEnumerable<string> GetColors()
        {
            return _jsonData.GetColorList();
        }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Data
{
    public class JsonData
    {
        //This path is dependent on where the products.json is located in your documents
        //You may need to change this path to your own path by dragging the products.json file here (from your right/left)
        public const string JsonFile = @"C:\Users\jonat\OneDrive\Dokument\Cloudnine API\Cloudnine-.NET Web API\Data\products.json";
        public List<string> GetPages(string json, List<string> stringlist, int? Page, int? Pagesize)
        {
            //Create an Array of the Jsonfile-string
            var JsonArray = JArray.Parse(json);

            //convert int? to int
            int Pageint = Convert.ToInt32(Page);
            int PageSizeint = Convert.ToInt32(Pagesize);

            //Use LINQ to filter Pages
            var query = (from ArrayPage in JsonArray
                         select ArrayPage)
                            .Skip(Pageint)
                            .Take(PageSizeint);

            foreach (var item in query)
            {
                stringlist.Add(item.ToString());
            }

            return stringlist;
        }
        public List<string> GetPagesFilteredByColor(string json, List<string> stringlist, int? Page, int? Pagesize, string color)
        {
            string Filterstring = "";
            // Forloop that always make the first letter of string'Color' capital and the rest in lowercase
            Filterstring += color.Substring(0, 1).ToUpper();

            for (int i = 1; i < color.Length; i++)
            {
                Filterstring += color[i];
            }

            //Create an Array of the Jsonfile-string
            var JsonArray = JArray.Parse(json);
            int Pageint = Convert.ToInt32(Page);
            int PageSizeint = Convert.ToInt32(Pagesize);

            //Use LINQ to filter by pages AND color
            var query = (from ArrayPage in JsonArray
                         where ArrayPage["color"].ToString() == Filterstring
                         select ArrayPage)
                            .Skip(Pageint)
                            .Take(PageSizeint);

            foreach (var item in query)
            {
                stringlist.Add(item.ToString());
            }

            return stringlist;
        }
        public JToken GetProductFilteredByID(string ID, JToken product)
        {
            var client = new WebClient();
            string json = client.DownloadString(JsonFile);

            var JsonArray = JArray.Parse(json);
           
            //Check through all elements in array to see if 'ID' matches the 'id' in Jsonfile
            for (int i = 0; i < JsonArray.Count; i++)
            {
                if (JsonArray[i]["id"].ToString() == ID)
                {
                    //If any productsID matches 'ID' then the compiler will break of the the forloop with 'break' keyword
                    product = JsonArray[i];
                    break;
                }
            }
            return product;
        }
        public List<string> GetColorList()
        {
            List<string> Colorlist = new List<string>();
            var client = new WebClient();

            string json = client.DownloadString(JsonData.JsonFile);
            var JsonArray = JArray.Parse(json);

            //Add all elements in JsonArray to Colorlist
            for (int i = 0; i < JsonArray.Count; i++)
            {
                Colorlist.Add(JsonArray[i]["color"].ToString());
            }

            //Return Colorlist with all the elements in JsonFile
            return Colorlist;
        }
    }
}

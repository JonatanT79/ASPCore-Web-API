using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudNine.Praktik.Data
{
    public class JsonData
    {
        public List<string> GetPages(string json, List<string> stringlist, int? Page, int? Pagesize)
        {
            var JsonArray = JArray.Parse(json);
            int Pageint = Convert.ToInt32(Page);
            int PageSizeint = Convert.ToInt32(Pagesize);

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
        //This path is dependent on where the products.json is located in your documents
        //You may need to change this path to your own path by dragging the products.json file here (from your right/left)
        public const string JsonFile = @"C:\Users\jonat\OneDrive\Dokument\Cloudnine\Cloudnine-.NET Web API\Data\products.json";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudNine.Praktik.Data
{
    public class JsonData
    {
        private string productName;
        private string iD;
        private string color;
        private DateTime publishDate;

        public string ProductName { get => productName; set => productName = value; }
        public string ID { get => iD; set => iD = value; }
        public string Description { get; set; }
        public string Color { get => color; set => color = value; }
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }

        public const string JsonFile = @"C:\Users\jonat\OneDrive\Dokument\Cloudnine\Cloudnine-Codetest\Data\products.json";
    }
}

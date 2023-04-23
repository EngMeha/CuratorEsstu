using AngleSharp.Html.Dom;
using DataLayer.Entity;
using InformationParser.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationParser.Implementation
{
    public class BTOBParser : IParser
    {
        public string BaseUrl { get; set; } = "https://uuopera.ru/afisha/";

        public Event[] Parser(IHtmlDocument document)
        {
            List<Event> events = new List<Event>();
            var listItem = document.QuerySelectorAll("div").Where(x => x.ClassName != null && x.ClassName.Contains("b-post b-post-list"));
            foreach (var item in listItem)
            {
                var title = item.QuerySelectorAll("h2").Where(x => x.ClassName.Contains("post-title")).First().QuerySelector("a").TextContent;
            }

            return events.ToArray();
        }
    }
}

using AngleSharp.Dom;
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

        public List<Event> Parser(IHtmlDocument document)
        {
            List<Event> events = new List<Event>();
            var listItem = document.QuerySelectorAll("div").Where(x => x.ClassName != null && x.ClassName.Contains("b-post b-post-list"));
            foreach (var item in listItem)
            {
                string title = item.QuerySelectorAll("h2").Where(x => x.ClassName.Contains("post-title")).First().QuerySelector("a").TextContent;
                string dateParser = item.QuerySelectorAll("div").Where(x => x.ClassName.Contains("b-post-txt")).First().QuerySelector("p").TextContent;
                string description = item.QuerySelectorAll("div").Where(x => x.ClassName.Contains("b-post-txt")).First().QuerySelector("a").QuerySelector("p").TextContent;
                var img = item.QuerySelectorAll("div").Where(x => x.ClassName.Contains("b-post-img")).First().QuerySelector("a>img[src]").Attributes["src"].Value;
                var url = item.QuerySelectorAll("div").Where(x => x.ClassName.Contains("b-post-txt")).First().QuerySelector("a").Attributes["href"].Value;
                var date = DateTime.Parse(dateParser);
                events.Add(new Event()
                {
                    CreateDate = DateTime.Now,
                    Comment = description,
                    Url = url,
                    DateTimeEvent = date,
                    Title = title,
                    Img = img
                });
            }

            return events.ToList();
        }
    }
}

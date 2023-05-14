using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using DataLayer.Entity;
using InformationParser.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace InformationParser.Implementation
{
    public class CultureParser : ICultureParser
    {
        public string BaseUrl { get; set; } = null;
        public List<string> Urls { get; set; } = new List<string> { "https://www.culture.ru/afisha/respublika-buryatiya-ulan-ude/institute-25743-koncertno-teatralnyi-centr-feniks",
            "https://www.culture.ru/afisha/respublika-buryatiya-ulan-ude/institute-11492-khudozhestvennyi-muzei-im-c-s-sampilova",
            "https://www.culture.ru/afisha/respublika-buryatiya-ulan-ude/institute-10426-gosudarstvennyi-russkii-dramaticheskii-teatr-imeni-n-a-bestuzheva"};

        public List<Event> Parser(IHtmlDocument document)
        {
            List<Event> events = new List<Event>();

            var listBlock = document.QuerySelectorAll("div").Where(x => x.ClassName != null && x.ClassName.Contains("CHPy6"));
            foreach (var block in listBlock)
            {
                Event @event = new Event();
                @event.Url = $"https://www.culture.ru{block.QuerySelector("a").GetAttribute("href")}";
                @event.Img = block.QuerySelector("img").GetAttribute("src");
                @event.Title = block.QuerySelectorAll("div").Where(x => x.ClassName != null && x.ClassName.Contains("fRPti")).First().TextContent;
                //@event.DateTimeEvent = DateTime.Parse(block.QuerySelectorAll("div").Where(x => x.ClassName != null && x.ClassName.Contains("r8tBP")).First().TextContent);
                @event.CreateDate = DateTime.Now;
                HtmlLoader html = new HtmlLoader(@event.Url);
                string sourceEvent = html.GetSourceAsync().Result;
                HtmlParser parser = new HtmlParser();
                IHtmlDocument documentEvent = parser.ParseDocument(sourceEvent);
                @event.Comment = documentEvent.QuerySelectorAll("div").Where(x => x.ClassName != null && x.ClassName.Contains("xZmPc")).First().QuerySelector("p").TextContent;
                DateTime dateEvent;
                string dateTime = block.QuerySelectorAll("div").Where(x => x.ClassName != null && x.ClassName.Contains("r8tBP")).First().TextContent;
                if (!DateTime.TryParse(dateTime, out dateEvent))
                {
                    dateEvent = DateTime.Parse(dateTime.Substring(2, 12));
                }
                @event.DateTimeEvent = dateEvent;
                events.Add(@event);
            }
            
            
            return events;
        }
    }
}

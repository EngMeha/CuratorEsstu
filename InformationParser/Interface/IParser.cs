using AngleSharp.Html.Dom;
using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationParser.Interface
{
    public interface IParser
    {
        Event[] Parser(IHtmlDocument document);
        string BaseUrl { get; set; }
    }
}

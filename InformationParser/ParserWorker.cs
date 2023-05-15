using AngleSharp.Dom.Events;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using BusinessLayer;
using DataLayer.Entity;
using InformationParser.Implementation;
using InformationParser.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationParser
{
    public class ParserWorker
    {


        DataManager _dataManager;

        /// <summary>
        /// Парсер автоматически записывает данные в базу.
        /// При инициализации объекта запускается парсинг данных
        /// </summary>
        public ParserWorker(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public async Task Worker()
        {
            List<DataLayer.Entity.Event> events = await _dataManager.Events.GetAllEvents();
            if (events.Count() == 0 || DateTime.Now.Day - events.First().CreateDate.Day > 30)
            {
                List<IParser> listParser = new List<IParser>()
                {
                    new BTOBParser(),
                    new CultureParser()
                };
                await _dataManager.Events.DeleteEvent();
                foreach (IParser item in listParser)
                {
                    if (item.BaseUrl == null)
                    {
                        ICultureParser culture = (ICultureParser)item;
                        HtmlLoader htmlLoader = new HtmlLoader(culture.Urls);
                        List<string> sources = await htmlLoader.GetListSourceAsync();
                        HtmlParser domParser = new HtmlParser();
                        foreach (string source in sources)
                        {
                            IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                            List<DataLayer.Entity.Event> result = item.Parser(document);
                            await _dataManager.Events.SaveEvent(result);
                        }
                    }
                    else
                    {
                        HtmlLoader htmlLoader = new HtmlLoader(item.BaseUrl);
                        string source = await htmlLoader.GetSourceAsync();
                        HtmlParser domParser = new HtmlParser();
                        IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                        List<DataLayer.Entity.Event> result = item.Parser(document);
                        await _dataManager.Events.SaveEvent(result);
                    }
                }
            }

        }
    }
}

using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
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
        bool _isActive;

        public bool IsActive { get { return _isActive; } }

        public event Action<object> OnCompleted;

        /// <summary>
        /// Парсер автоматически записывает данные в базу.
        /// При инициализации объекта запускается парсинг данных
        /// </summary>
        public ParserWorker()
        {
            _isActive = true;
            Worker();
        }

        private async void Worker()
        {
            List<IParser> listParser = new List<IParser>()
            {
                new BTOBParser()
            };
            foreach (IParser item in listParser)
            {
                if (!IsActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }
                HtmlLoader htmlLoader = new HtmlLoader(item.BaseUrl);
                string source = await htmlLoader.GetSourceAsync();
                HtmlParser domParser = new HtmlParser();
                IHtmlDocument document = await domParser.ParseDocumentAsync(source);
                Event[] result = item.Parser(document);
                OnCompleted?.Invoke(this);
                _isActive = false;
            }
            
        }
    }
}

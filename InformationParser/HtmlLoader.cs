using InformationParser.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InformationParser
{
    public class HtmlLoader
    {
        readonly HttpClient client;
        readonly string url;
        readonly List<string> listUrl;

        public HtmlLoader(string url)
        {
            client = new HttpClient();
            this.url = url;
        }
        public HtmlLoader(List<string> listUrl)
        {
            client = new HttpClient();
            this.listUrl = listUrl;
        }

        public async Task<string> GetSourceAsync()
        {
            string source = null;
            var response = await client.GetAsync(url);
            if (response != null && response.StatusCode == HttpStatusCode.OK)
            {
                source = await response.Content.ReadAsStringAsync();
            }
            return source;
        }

        public async Task<List<string>> GetListSourceAsync()
        {
            List<string> sources = new List<string>();
            foreach (string url in listUrl)
            {
                string source = null;
                var response = await client.GetAsync(url);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    source = await response.Content.ReadAsStringAsync();
                    sources.Add(source);
                }
            }
            return sources;
        }
    }
}

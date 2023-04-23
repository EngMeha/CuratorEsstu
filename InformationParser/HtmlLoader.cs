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

        public HtmlLoader(string url)
        {
            client = new HttpClient();
            this.url = url;
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
    }
}

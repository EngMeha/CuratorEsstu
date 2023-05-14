using AngleSharp.Io;
using BusinessLayer;
using CuratorEsstu.Models;
using DataLayer.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace CuratorEsstu.Controllers
{
    public class HomeController : Controller
    {

        //private HttpClientHandler hdl;
        //private CookieContainer cookie;
        //private string fkey;
        //9KtTV37s
        public async Task<IActionResult> Index()
        {
            //cookie = new CookieContainer();
            //hdl = new HttpClientHandler
            //{
            //    AllowAutoRedirect = false,
            //    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None,
            //    CookieContainer = cookie,
                
            //    Proxy = new WebProxy("127.0.0.1:8888"),
            //};
            //await LoginValidationtrack();
            ////await GetLoginPage();
            ////await LoginValidationtrack();
            ////await Login();
            //await GetPage2();
            //await GetPage3();
            //await Load();
            return View();
        }

        

        //public async Task<bool> LoginValidationtrack(string login = "emelnikova", string password = "9KtTV37s")
        //{
        //    using (var clnt = new HttpClient(hdl, false))
        //    {
        //        clnt.DefaultRequestHeaders.Referrer = new Uri("https://esstu.ru/auth/");
        //        clnt.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
        //        clnt.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
        //        clnt.DefaultRequestHeaders.Add("Origin", "https://esstu.ru");
        //        clnt.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
        //        clnt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");

        //        using (var content = new StringContent($"username={login}&password={password}", Encoding.UTF8, "application/x-www-form-urlencoded"))
        //        {
        //            using (var resp = await clnt.PostAsync("https://esstu.ru/auth/login", content))
        //            {
        //                if (resp.StatusCode == HttpStatusCode.Found)
        //                {
                            
        //                }
        //            }
        //        }
        //    }
        //    return false;
        //}

        //public async Task<bool> GetPage2()
        //{
        //    using (var clnt = new HttpClient(hdl,false))
        //    {
        //        clnt.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
        //        clnt.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
        //        clnt.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
        //        clnt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
        //        clnt.DefaultRequestHeaders.Referrer = new Uri("https://esstu.ru/auth/");

                
        //        using (var resp = await clnt.GetAsync("https://esstu.ru/contingent/login"))
        //        {
                    
        //            string s = resp.Headers.Location.AbsoluteUri;
        //            var resp2 = await clnt.GetAsync(s);
        //            var resp3 = await clnt.GetAsync(resp2.Headers.Location.AbsoluteUri);
        //            var resp4 = await clnt.GetAsync("https://esstu.ru/contingent/structure/viewDepartment.do?departmentCode=4001");
        //            var html = await resp4.Content.ReadAsStringAsync();
        //            var doc = new HtmlAgilityPack.HtmlDocument();
        //            doc.LoadHtml(html);
        //        }
        //    }
        //    return false;
        //}

        //public async Task<bool> GetPage3()
        //{
        //    using (var clnt = new HttpClient(hdl, false))
        //    {
        //        clnt.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
        //        clnt.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
        //        clnt.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
        //        clnt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
        //        clnt.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
        //        clnt.DefaultRequestHeaders.Referrer = new Uri("https://esstu.ru/auth/");


        //        using (var resp = await clnt.GetAsync("https://esstu.ru/contingent/login"))
        //        {
        //            string s = resp.Headers.Location.AbsoluteUri;
        //            var resp2 = await clnt.GetAsync(s);
        //            var resp3 = await clnt.GetAsync(resp2.Headers.Location.AbsoluteUri);
        //            var resp4 = await clnt.GetAsync(resp3.Headers.Location.AbsoluteUri);
        //            var html = await resp4.Content.ReadAsStringAsync();
        //            var doc = new HtmlAgilityPack.HtmlDocument();
        //            doc.LoadHtml(html);
        //        }
        //    }
        //    return false;
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using BusinessLayer;
using DataLayer.Entity;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace EsstuParser
{
    public class EsstuParserWorker
    {
        string _login;
        string _password;

        private HttpClientHandler hdl;
        private CookieContainer cookie;

        public EsstuParserWorker(string login, string password)
        {
            _login = login;
            _password = password;

            cookie = new CookieContainer();
            hdl = new HttpClientHandler
            {
                AllowAutoRedirect = false,
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None,
                CookieContainer = cookie,

            };
        }

        private async Task<IHtmlDocument> LoadHint(string source)
        {
            HtmlParser htmlParser = new HtmlParser();
            IHtmlDocument html = await htmlParser.ParseDocumentAsync(source);
            return html;
        }

        public async Task ParsStudentsByGroup(List<GroupsDirectory> groups, User user, DataManager _dataManager)
        {
            foreach (GroupsDirectory groupsDirectory in groups)
            {
                
                using(var clnt = new HttpClient(hdl, false))
                {
                    clnt.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                    clnt.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
                    clnt.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                    clnt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
                    clnt.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                    clnt.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
                    clnt.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                    clnt.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
                    clnt.DefaultRequestHeaders.Referrer = new Uri(groupsDirectory.Href);
                    using (var resp = await clnt.GetAsync(groupsDirectory.Href))
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            var html = await LoadHint(await resp.Content.ReadAsStringAsync());
                            var commonBlock = html.QuerySelectorAll("div");
                            var blockGroup = commonBlock[5].QuerySelector("div").QuerySelectorAll("div").First(x => x.Id.Equals("_title_id_groupinfo_id"))
                                .QuerySelector("table").QuerySelector("tbody").QuerySelectorAll("tr");
                            string department = blockGroup[1].QuerySelectorAll("td")[1].TextContent;
                            await _dataManager.CraduationDepartaments.CreateDepartment(department);
                            CraduationDepartament craduationDepartament = await _dataManager.CraduationDepartaments.GetCraduationDepartament(department);
                            GroupsOfTeacher groupsOfTeacher = new GroupsOfTeacher()
                            {
                                User = user,
                                CraduationDepartament = craduationDepartament,
                                GroupDirectory = groupsDirectory,
                                Well = Convert.ToInt32(blockGroup[7].QuerySelectorAll("td")[1].TextContent),
                                
                            };
                            if (!(groupsDirectory.Students.Count() == 0))
                            {
                                await _dataManager.Groups.SaveGroup(groupsOfTeacher);
                                continue;
                            }
                            else
                            {
                                await _dataManager.Groups.SaveGroup(groupsOfTeacher);
                            }
                            var blockStudent = commonBlock[5].QuerySelector("div").QuerySelector("form").QuerySelector("table").QuerySelector("tbody").QuerySelectorAll("tr");
                            foreach (var student in blockStudent)
                            {
                                string href = $"https://esstu.ru{student.QuerySelectorAll("td")[2].QuerySelector("a").GetAttribute("href")}";
                                Student createStudent = new Student();
                                using (var respStudent = await clnt.GetAsync(href))
                                {
                                    
                                    var htmlStudent = await LoadHint(await respStudent.Content.ReadAsStringAsync());
                                    var blockList = htmlStudent.QuerySelector("div").QuerySelector("div").QuerySelectorAll("div");
                                    var topBlock = blockList[4].QuerySelector("table").QuerySelector("tbody").QuerySelectorAll("tr");
                                    var downBlock = blockList[6].QuerySelector("table").QuerySelector("tbody").QuerySelectorAll("tr");
                                    createStudent.FIO = topBlock[2].QuerySelectorAll("td")[1].TextContent;
                                    var t = topBlock[11].QuerySelectorAll("td")[1].TextContent;
                                    createStudent.BasisOfLerning = await _dataManager.BasisOfLearnings.GetBasisOfLearning(topBlock[11].QuerySelectorAll("td")[1].TextContent);
                                    createStudent.Gender = downBlock[4].QuerySelectorAll("td")[1].TextContent;
                                    createStudent.DateOfBirth = DateTime.Parse(downBlock[5].QuerySelectorAll("td")[1].TextContent);
                                    string citizenship = downBlock[7].QuerySelectorAll("td")[1].TextContent;
                                    createStudent.Citizenship = string.IsNullOrEmpty(citizenship) ? "Россия" : citizenship;
                                    createStudent.FullFamily = downBlock[8].QuerySelectorAll("td")[1].TextContent.Equals("нет") ? false : true;
                                    createStudent.Orphan = downBlock[9].QuerySelectorAll("td")[1].TextContent.Equals("нет") ? false : true;
                                    createStudent.NeedHostel = downBlock[10].QuerySelectorAll("td")[1].TextContent.Equals("нет") ? false : true;
                                    createStudent.CreateDate = DateTime.Now;
                                    createStudent.Group = groupsDirectory;
                                    createStudent.PercentOfAttedence = 0;
                                    createStudent.PercentOfProgress = 0;
                                    createStudent.Href = href;
                                    var respContact = await clnt.GetAsync(href.Replace("personalInfo", "contactInfo"));
                                    var contactInfo = await LoadHint(await respContact.Content.ReadAsStringAsync());
                                    var downBlockContact = contactInfo.QuerySelector("div").QuerySelector("div").QuerySelectorAll("div")[6].QuerySelector("table").QuerySelector("tbody").QuerySelectorAll("tr");
                                    string[] phone = downBlockContact[0].QuerySelectorAll("td")[1].TextContent.Split(",");
                                    if (phone.Length == 1)
                                    {
                                        createStudent.PhoneOfStudent = phone[0].Trim();
                                        createStudent.PhoneOfParents = "-";
                                    }
                                    else
                                    {
                                        createStudent.PhoneOfStudent = phone[0].Trim();
                                        createStudent.PhoneOfParents = phone[1].Trim();
                                    }
                                }
                                await _dataManager.Students.SaveStudent(createStudent);
                            }
                            await _dataManager.HistoryChangeStudents.CreateHistoryChangeStudent(_dataManager.Students.GetStudentsByGroupForParse(groupsDirectory));
                        }
                    }
                }
            }
        }

        public async Task GetDirectoryAsync(DataManager _dataManager)
        {

            if (!await _dataManager.GropsDepartmen.CheckGroup())
            {

                await _dataManager.GropsDepartmen.DeleteAllGroup();
                await _dataManager.Speciality.AllDeleteSpesciality();
                using (var clnt = new HttpClient(hdl, false))
                {
                    clnt.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                    clnt.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
                    clnt.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                    clnt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
                    clnt.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                    clnt.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
                    clnt.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                    clnt.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");

                    using (var resp = await clnt.GetAsync("https://esstu.ru/contingent/structure/viewDepartment.do?departmentCode=4001"))
                    {
                        if (resp.StatusCode == HttpStatusCode.OK)
                        {
                            var html = await LoadHint(await resp.Content.ReadAsStringAsync());
                            var blocksEducation = html.QuerySelectorAll("div").Where(x=>x.ClassName != null && x.ClassName.Contains("department"));
                            int countBlock = 1;
                            foreach (var item in blocksEducation)
                            {
                                if (countBlock <= 2)
                                {
                                    var listTr = item.QuerySelector("tbody").QuerySelectorAll("tr");
                                    int countTr = 1;
                                    foreach (var tr in listTr)
                                    {
                                        if (countTr <= listTr.Count() - 1)
                                        {
                                            List<string> speciality = new List<string>();
                                            int count = 1;
                                            foreach (var td in tr.QuerySelectorAll("td"))
                                            {
                                                foreach (var div in td.QuerySelectorAll("div"))
                                                {
                                                    if (count <= 2)
                                                    {
                                                        speciality.Add(div.TextContent.Replace("\t", "").Replace("\n", ""));
                                                    }
                                                    if (count == 2)
                                                    {
                                                        await _dataManager.Speciality.CreateSpeciality(speciality[2], speciality[0]);
                                                    }
                                                    if (div.QuerySelector("a") != null)
                                                    {
                                                        string title = div.TextContent;

                                                        if (!title.Equals("АКАДЕМ"))
                                                        {
                                                            string href = div.QuerySelector("a").GetAttribute("href");
                                                            Speciality speciality1 = await _dataManager.Speciality.GetSpeciality(speciality[0]);
                                                            await _dataManager.GropsDepartmen.CreateGroup($"https://esstu.ru{href}", title, speciality1);
                                                        }
                                                    }
                                                }
                                                count++;
                                            }
                                        }
                                        countTr++;
                                    }
                                }
                                countBlock++;
                            }
                        }
                    }
                }
            }
        }

        public async Task<bool> AutrorizeAsync()
        {
            if (_login == null)
            {
                return false;
            }
            using (var clnt = new HttpClient(hdl, false))
            {
                clnt.DefaultRequestHeaders.Referrer = new Uri("https://esstu.ru/auth/");
                clnt.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                clnt.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
                clnt.DefaultRequestHeaders.Add("Origin", "https://esstu.ru");
                clnt.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                clnt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
                clnt.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                clnt.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
                clnt.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                clnt.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");

                using (var content = new StringContent($"username={_login}&password={_password}", Encoding.UTF8, "application/x-www-form-urlencoded"))
                {
                    using (var resp = await clnt.PostAsync("https://esstu.ru/auth/login", content))
                    {
                        if (resp.StatusCode == HttpStatusCode.Found)
                        {
                            await InternalAutorization();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public async Task InternalAutorization()
        {
            using (var clnt = new HttpClient(hdl, false))
            {
                clnt.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                clnt.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
                clnt.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                clnt.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/112.0.0.0 Safari/537.36");
                clnt.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
                clnt.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "navigate");
                clnt.DefaultRequestHeaders.Add("Sec-Fetch-User", "?1");
                clnt.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "document");
                clnt.DefaultRequestHeaders.Referrer = new Uri("https://esstu.ru/auth/");

                using (var resp = await clnt.GetAsync("https://esstu.ru/contingent/login"))
                {
                    var resp2 = await clnt.GetAsync(resp.Headers.Location.AbsoluteUri);
                    var resp3 = await clnt.GetAsync(resp2.Headers.Location.AbsoluteUri);
                }
            }
        }
    }
}
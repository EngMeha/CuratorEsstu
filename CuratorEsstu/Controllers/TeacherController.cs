using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer;
using Microsoft.AspNetCore.Identity;
using DataLayer.Entity;
using PresentationLayer;
using EsstuParser;
using PresentationLayer.Models;
using Word = Microsoft.Office.Interop.Word;
using System.Data;
using BusinessLayer.Interface;
using Microsoft.Office.Interop.Word;
using System.Globalization;
using BusinessLayer.Implementation;

namespace CuratorEsstu.Controllers
{
    [Authorize(Roles = "Куратор")]
    public class TeacherController : Controller
    {
        DataManager _dataManager;
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        ServiceManager _serviceManager;
        static EsstuParserWorker _esstuParserWorker;
        IWebHostEnvironment _appEnvironment;
        bool f = false;
        public TeacherController(DataManager dataManager, UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment appEnvironment)
        {
            _dataManager = dataManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _serviceManager = new ServiceManager(dataManager);
            _appEnvironment = appEnvironment;
        }

        public static class GlobalTeacher
        {
            public static User Teacher { get; set; }
        }

        public async Task<IActionResult> AutorizeForEsstu()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AutorizeForEsstu(AuthorizationModel model)
        {
            _esstuParserWorker = new EsstuParserWorker(model.Login, model.Password);
            if (await _esstuParserWorker.AutrorizeAsync())
            {
                await _esstuParserWorker.GetDirectoryAsync(_dataManager);
                return RedirectToAction("ShowAllGroup", "Teacher");
                /*
                await _esstuParserWorker.ParsStudentsByGroup(await _dataManager.GropsDepartmen.GetAllGroupByTitle(new[] { "К102/1", "К79/1" }), GlobalTeacher.Teacher);*/
            }
            return View();
        }

        public async Task<IActionResult> CreateUser()
        {
            GlobalTeacher.Teacher = await _userManager.GetUserAsync(User);
            
            return RedirectToAction("AutorizeForEsstu", "Teacher");
        }

        public async Task<IActionResult> Index(int mounth)
        {
            mounth = mounth == 0 ? DateTime.Now.Month : mounth;
            return View(await _serviceManager.EventService.ViewEventOfCalendar(mounth));
        }

        public async Task<IActionResult> Report(string findGroup = "Все")
        {
            ViewData["listGroup"] = await _dataManager.Groups.GetListGroupOfTeacher(GlobalTeacher.Teacher, false);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Report(string dat1, string dat2)
        {
            List<EventOfStudent> eventsWithGroup = await _dataManager.Events.GetAllEventsByDate(dat1, dat2, GlobalTeacher.Teacher);
            string templateFileName = $@"{_appEnvironment.WebRootPath}\TemplateFiles\TemplateReport.docx".Replace(@"\\", @"\");
            Word.Application wordApplication = new Word.Application();
            wordApplication.Visible = true;
            Word.Document wordDocument = wordApplication.Documents.Add(templateFileName);

            wordDocument.Bookmarks["TEACHER"].Range.Text = $"{GlobalTeacher.Teacher.SecondName} {GlobalTeacher.Teacher.FirstName} {GlobalTeacher.Teacher.LastName}";
            wordDocument.Bookmarks["MOUNTH"].Range.Text = DateTime.Now.ToString("MMMM", CultureInfo.GetCultureInfo("ru-RU"));
            wordDocument.Bookmarks["YEAR"].Range.Text = DateTime.Now.Year.ToString();
            Word.Table tableEvent = wordDocument.Bookmarks["EVENTTABLE"].Range.Tables[1];
            string groupWithCountStudent = "";
            foreach (GroupsOfTeacher item in await _dataManager.Groups.GetListGroupOfTeacher(GlobalTeacher.Teacher, true))
            {
                groupWithCountStudent = $"{groupWithCountStudent}{item.GroupDirectory.Title} - {item.GroupDirectory.Students.Count()}\n";
            }
            wordDocument.Bookmarks["COUNTSTUDENT"].Range.Text = $"{groupWithCountStudent}";

            foreach (var item in eventsWithGroup.Select(x => new { x.Event.Title, x.Event.DateTimeEvent, GroupTitle = x.Student.Group.Title }).Distinct())
            {
                Row rows = tableEvent.Rows.Add();
                rows.Cells[1].Range.Text = $"{item.GroupTitle}";
                rows.Cells[2].Range.Text = $"{item.DateTimeEvent}";
                rows.Cells[3].Range.Text = $"Мероприятие {item.Title}";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ReportSocialPassport(string group, string dat1, string dat2)
        {
            
            GroupsDirectory groupsDirectory = await _dataManager.GropsDepartmen.GetGroupByTitle("К79/1", true);
            string templateFileName = $@"{_appEnvironment.WebRootPath}\TemplateFiles\TemplatePassport.docx".Replace(@"\\", @"\");

            Word.Application wordApplication = new Word.Application();
            wordApplication.Visible = true;
            Word.Document wordDocument = wordApplication.Documents.Add(templateFileName);

            wordDocument.Bookmarks["GROUP"].Range.Text = groupsDirectory.Title;
            wordDocument.Bookmarks["COUNTGIRL"].Range.Text = groupsDirectory.Students.Where(x=>x.Gender.ToLower().Equals("женский")).Count().ToString();
            wordDocument.Bookmarks["COUNTMAN"].Range.Text = groupsDirectory.Students.Where(x=>x.Gender.ToLower().Equals("мужской")).Count().ToString();
            wordDocument.Bookmarks["TEACHER"].Range.Text = $"{GlobalTeacher.Teacher.SecondName} {GlobalTeacher.Teacher.FirstName} {GlobalTeacher.Teacher.LastName}";
            wordDocument.Bookmarks["COUNT"].Range.Text = groupsDirectory.Students.Count().ToString();
            wordDocument.Bookmarks["SPEC"].Range.Text = groupsDirectory.Speciality.Title;
            wordDocument.Bookmarks["YEAR"].Range.Text = DateTime.Now.Year.ToString();

            Word.Table tableOrphan = wordDocument.Bookmarks["ORPHAN"].Range.Tables[1];
            int countRows = 1;
            foreach (Student student in groupsDirectory.Students.Where(x=>x.Orphan == true))
            {
                Row rows = tableOrphan.Rows.Add();
                rows.Cells[1].Range.Text = $"{countRows}. {student.FIO}";
                rows.Cells[2].Range.Text = $"{student.DateOfBirth.ToShortDateString()}";
                rows.Cells[5].Range.Text = $"{student.PhoneOfStudent}";
                countRows++;
            }
            countRows = 1;
            Word.Table tableNotFullName = wordDocument.Bookmarks["NOTFULLFAMILY"].Range.Tables[1];
            foreach (Student student in groupsDirectory.Students.Where(x => x.FullFamily == false))
            {
                Row rows = tableOrphan.Rows.Add();
                rows.Cells[1].Range.Text = $"{countRows}. {student.FIO}";
                rows.Cells[2].Range.Text = $"{student.DateOfBirth.ToShortDateString()}";
                rows.Cells[5].Range.Text = $"{student.PhoneOfStudent}";
                countRows++;
            }


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowAllGroup()
        {
            return View(await _serviceManager.GroupOfTeacherService.ViewAllGroup(GlobalTeacher.Teacher));
        }
        public async Task<IActionResult> AddGroup(string[] groups)
        {
            List<GroupsDirectory> groupsDirectories = await _dataManager.GropsDepartmen.GetAllGroupByTitle(groups);
            if (_esstuParserWorker == null)
            {
                return RedirectToAction("AutorizeForEsstu", "Teacher");
            }

            await _esstuParserWorker.ParsStudentsByGroup(groupsDirectories, GlobalTeacher.Teacher, _dataManager);

            return RedirectToAction("GroupInfo", "Teacher");

        }
        public async Task<IActionResult> DeleteGroup(int[] groups)
        {
            foreach (int group in groups)
            {
                GroupsOfTeacher groupsOfTeacher = await _dataManager.Groups.GetGroup(group, false);
                await _dataManager.Groups.DeleteGroup(groupsOfTeacher);
            }
            return RedirectToAction("GroupInfo", "Teacher");
        }
        [HttpGet]
        public async Task<IActionResult> GroupInfo()
        {
            ViewData["listGroup"] = await _dataManager.Groups.GetListGroupOfTeacher(GlobalTeacher.Teacher, false);
            return View("GroupInfo", await _serviceManager.GroupOfTeacherService.ViewInfoGroup(GlobalTeacher.Teacher));
        }

        [HttpPost]
        public async Task<IActionResult> CheckGroup()
        {
            int idGroup = Convert.ToInt16(Request.Form["idGroup"]);
            ViewData["listGroup"] = await _dataManager.Groups.GetListGroupOfTeacher(GlobalTeacher.Teacher, false);
            return View("GroupInfo", await _serviceManager.GroupOfTeacherService.ViewInfoGroup(idGroup));
        }

        #region WorkWithEvent
        public async Task<IActionResult> EventCity()
        {
            return View(await _serviceManager.EventService.ViewAllEvent());
        }

        public async Task<IActionResult> DescriptionEvent(int id)
        {
            return PartialView("DescriptionEvent", await _serviceManager.EventService.ViewDescriptionEvent(id));
        }
        public async Task<IActionResult> DescriptionEventFromCalendar(int id)
        {
            List<string> titleGroup = await _dataManager.Events.GetEventWithGroup(id);
            return PartialView("DescriptionEventFromCalendar", await _serviceManager.EventService.ViewDescriptionEvent(id, titleGroup.Distinct().ToList()));
        }

        [HttpGet]
        public async Task<IActionResult> CreateEvent(int idEvent, string findGroup = "Все")
        {
            if (idEvent == 0)
            {
                return View(await _serviceManager.EventService.ViewCreateEvent(GlobalTeacher.Teacher, findGroup));
            }
            return View(await _serviceManager.EventService.ViewCreateEvent(idEvent, GlobalTeacher.Teacher, findGroup));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(string[] groups, string title, string dateEvent, string idEvent)
        {
            if (!(groups.Count() == 0))
            {
                foreach (string group in groups)
                {
                    if (!idEvent.Equals("0"))
                    {
                        GroupsOfTeacher groupsOfTeacher = await _dataManager.Groups.GetGroup(Convert.ToInt32(group), true);
                        Event @event = await _dataManager.Events.GetEvent(Convert.ToInt32(idEvent));
                        await _dataManager.EventOfStudents.CreateEventOfStudent(@event, groupsOfTeacher.GroupDirectory.Students);
                    }
                    else
                    {
                        Event @event = new Event()
                        {
                            CreateDate = DateTime.Now,
                            DateTimeEvent = DateTime.Parse(dateEvent),
                            Title = title,
                            Comment = "",
                            Img = "",
                            Url = ""
                        };
                        await _dataManager.Events.SaveEvent(@event);
                        GroupsOfTeacher groupsOfTeacher = await _dataManager.Groups.GetGroup(Convert.ToInt32(group), true);
                        await _dataManager.EventOfStudents.CreateEventOfStudent(@event, groupsOfTeacher.GroupDirectory.Students);
                    }
                }
                return Json(new { redirectToUrl = Url.Action("Index", "Teacher") });
            }
            else
            {
                return View(await _serviceManager.EventService.ViewCreateEvent(Convert.ToInt32(idEvent), GlobalTeacher.Teacher, "Все"));
            }
        }
        #endregion

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Autorization");
        }
    }
}

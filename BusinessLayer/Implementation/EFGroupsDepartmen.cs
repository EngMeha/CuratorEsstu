using BusinessLayer.Interface;
using DataLayer;
using DataLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class EFGroupsDepartmen : IGropsDepartmen
    {
        DiplomContext _context;
        public EFGroupsDepartmen(DiplomContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckGroup()
        {
            return  _context.GroupDirectory.Count() != 0 ? true : false && _context.GroupDirectory.FirstAsync().Result.CreateDate.Subtract(DateTime.Now).TotalDays < 90;
        }

        public async Task CreateGroup(string href, string title, Speciality speciality)
        {
            GroupsDirectory groupsDirectory = new GroupsDirectory()
            {
                Href = href,
                Title = title,
                CreateDate = DateTime.Now,
                Speciality = speciality
            };
            await _context.GroupDirectory.AddAsync(groupsDirectory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllGroup()
        {
            foreach (GroupsDirectory groupsDirectory in await _context.GroupDirectory.Include(x=>x.GroupsOfTeacher).ToListAsync())
            {
                if (groupsDirectory.GroupsOfTeacher.Count == 0)
                {
                    _context.GroupDirectory.Remove(groupsDirectory);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<GroupsDirectory>> GetAllGroupByTitle(string[] title)
        {
            List<GroupsDirectory> groupsDirectories = new();
            foreach (string item in title)
            {
                groupsDirectories.Add(await _context.GroupDirectory.Include(x=>x.Students).FirstAsync(x => x.Title.Equals(item)));
            }
            return groupsDirectories;
        }

        public async Task<GroupsDirectory> GetGroupByTitle(string title, bool include = false)
        {
            if (include)
            {
                return await _context.GroupDirectory.Include(x=>x.Students).FirstAsync(x => x.Title.Equals(title));
            }
            else
            {
                return await _context.GroupDirectory.FirstAsync(x => x.Title.Equals(title));
            }
        }

        public async Task<List<GroupsDirectory>> GetAllGroupWithOutTeacher()
        {
            return await _context.GroupDirectory.Include(x => x.GroupsOfTeacher).Where(x => x.GroupsOfTeacher.Count() == 0).ToListAsync();
        }
    }
}

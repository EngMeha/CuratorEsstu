using BusinessLayer.Interface;
using DataLayer;
using DataLayer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class EFGroup: IGroup
    {
        DiplomContext _context;
        public EFGroup(DiplomContext context) 
        { 
            _context= context;
        }

        public async Task DeleteGroup(GroupsOfTeacher group)
        {
            /*List<Student> studentOfGroup = _context.Student.Where(x=>x.Group == group).ToList();
            foreach (Student student in studentOfGroup)
            {
                _context.Student.Remove(student);
            }*/
            _context.Group.Remove(group);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GroupsOfTeacher>> GetListGroupOfTeacher(User user, bool include)
        {
            if (include)
            {
                return await _context.Group.Include(x => x.GroupDirectory).Include(x => x.GroupDirectory.Students)
                    .Include(x => x.User).Where(x => x.User.Id == user.Id).ToListAsync();
            }
            else
            {
                return await _context.Group.Include(x => x.User).Include(x=>x.GroupDirectory).Where(x => x.User.Id == user.Id).ToListAsync();
            }
        }

        public async Task<List<GroupsOfTeacher>> GetListGroupOfTeacherBySort(User user, string findGroup)
        {
            if (findGroup.Equals("Все"))
            {
                return await _context.Group.Include(x => x.GroupDirectory)
                .Include(x => x.User).Where(x => x.User.Id == user.Id).ToListAsync();
            }
            else
            {
                return await _context.Group.Include(x => x.GroupDirectory)
                .Include(x => x.User).Where(x => x.User.Id == user.Id && x.GroupDirectory.Title.Contains(findGroup)).ToListAsync();
            }
            
        }

        public async Task<bool> CheckGroupOnCurator()
        {
            
            return _context.Group.Count() == 0?false : true;
        }


        public async Task<GroupsOfTeacher> GetFirstGroup(User user)
        {
            try
            {
                return await _context.Group.Include(x => x.CraduationDepartament)
                .Include(x => x.GroupDirectory).Include(x => x.GroupDirectory.Students).Include(x => x.GroupDirectory.Speciality).FirstAsync(x => x.User.Id == user.Id);
            }
            catch
            {
                return await _context.Group.FirstAsync();
            }
            
        }

        public async Task<List<GroupsOfTeacher>> GetAllGroups(bool include = true)
        {
            if (include)
            {
                return await _context.Group.Include(x=>x.User).Include(x=>x.CraduationDepartament).ToListAsync();
            }
            else
            {
                return await _context.Group.ToListAsync();
            }
        }

        public async Task<GroupsOfTeacher> GetGroup(int id, bool include = true)
        {
            if (include)
            {
                GroupsOfTeacher groups = await (from gr in _context.Group.Include(x => x.CraduationDepartament)
                .Include(x => x.GroupDirectory.Students).Include(x => x.GroupDirectory.Speciality)
                                                      join st in _context.Student.Include(x => x.BasisOfLerning).Include(x => x.Group)
                                                      on gr.GroupDirectory.Id equals st.Group.Id
                                                      select gr).FirstOrDefaultAsync(x=>x.Id == id);
                return groups;
            }
            else
            {
                return await _context.Group.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task SaveGroup(GroupsOfTeacher group)
        {
            if (group.Id == 0)
            {
                _context.Entry(group).State = EntityState.Added;
            }
            else
            {
                _context.Entry(group).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
    }
}

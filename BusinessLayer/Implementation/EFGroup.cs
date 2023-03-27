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
    public class EFGroup: IGroup
    {
        DiplomContext _context;
        public EFGroup(DiplomContext context) 
        { 
            _context= context;
        }

        public async Task DeleteGroup(Group group)
        {
            List<Student> studentOfGroup = _context.Student.Where(x=>x.Group == group).ToList();
            foreach (Student student in studentOfGroup)
            {
                _context.Student.Remove(student);
            }
            _context.Group.Remove(group);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Group>> GetAllGroups(bool include = true)
        {
            if (include)
            {
                return await _context.Group.Include(x => x.Students).Include(x=>x.User).Include(x=>x.CraduationDepartament).Include(x=>x.FormOfStudy).ToListAsync();
            }
            else
            {
                return await _context.Group.ToListAsync();
            }
        }

        public async Task<Group> GetGroup(int id, bool include = true)
        {
            if (include)
            {
                return await _context.Group.Include(x=>x.Students).Include(x => x.User).Include(x => x.CraduationDepartament).Include(x => x.FormOfStudy).FirstOrDefaultAsync(x=>x.Id==id);
            }
            else
            {
                return await _context.Group.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task SaveGroup(Group group)
        {
            if (group.Id == 0)
            {
                _context.Group.Add(group);
            }
            else
            {
                _context.Entry(group).State= EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
    }
}

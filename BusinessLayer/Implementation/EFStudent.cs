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
    public class EFStudent: IStudent
    {
        DiplomContext _context;
        public EFStudent(DiplomContext context) 
        {
            _context = context;
        }

        public async Task SaveStudent(Student student)
        {
            if (student.Id == 0)
            {
                _context.Student.Add(student);
            }
            else
            {
                _context.Entry(student).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(Student student)
        {
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetAllStudent(bool include = true)
        {
            if (include)
            {
                return await _context.Student.Include(x => x.BasisOfLerning).Include(x => x.HistoryChangeStudent).Include(x => x.EventOfStudents).Include(x => x.Group)
                    .Include(x => x.Group.GroupsOfTeacher).ToListAsync();
            }
            else
            {
                return await _context.Student.ToListAsync();
            }
        }

        public async Task<Student> GetStudent(int id, bool include = true)
        {
            if (include)
            {
                return await _context.Student.Include(x => x.BasisOfLerning).Include(x => x.HistoryChangeStudent).Include(x => x.EventOfStudents)
                    .Include(x => x.Group).FirstOrDefaultAsync(x=>x.Id == id);
            }
            else
            {
                return await _context.Student.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public List<Student> GetStudentsByGroupForParse(GroupsDirectory groupsOfTeacher)
        {
            return groupsOfTeacher.Students.ToList();
        }

        public async Task<List<Student>> GetStudentsByGroup(GroupsOfTeacher groupsOfTeacher)
        {
            return await _context.Student.Include(x => x.Group).Include(x=>x.Group.GroupsOfTeacher).Include(x => x.BasisOfLerning).Where(x => x.Group.Id == groupsOfTeacher.GroupDirectory.Id).ToListAsync();
        }
    }
}

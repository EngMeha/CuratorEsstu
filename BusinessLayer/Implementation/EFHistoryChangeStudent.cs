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
    public class EFHistoryChangeStudent : IHistoryChangeStudent
    {
        DiplomContext _context;
        public EFHistoryChangeStudent(DiplomContext context) 
        { 
            _context = context;
        }
        public async Task CreateHistoryChangeStudent(List<Student> students)
        {
            foreach (Student student in students)
            {
                HistoryChangeStudent historyChangeStudent = new HistoryChangeStudent();
                historyChangeStudent.Student = student;
                historyChangeStudent.PercentOfAttedence = student.PercentOfAttedence;
                historyChangeStudent.PercentOfProgress = student.PercentOfProgress;
                historyChangeStudent.UpdateDate = DateTime.Now;
                _context.HistoryChangeStudent.Add(historyChangeStudent);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteHistoryChangeStudent(List<Student> students)
        {
            foreach (Student student in students)
            {
                List<HistoryChangeStudent> historyChangeStudents = _context.HistoryChangeStudent.Where(x => x.Student == student).ToList();
                foreach (HistoryChangeStudent item in historyChangeStudents)
                {
                    _context.HistoryChangeStudent.Remove(item);
                   
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<HistoryChangeStudent>> GetAllHistoryChangeStudent(bool include = true)
        {
            if (include)
            {
                return await _context.HistoryChangeStudent.Include(x=>x.Student).Include(x=>x.Student.BasisOfLerning).Include(x=>x.Student.EventOfStudents).Include(x=>x.Student.Group).ToListAsync();
            }
            else
            {
                return await _context.HistoryChangeStudent.ToListAsync();
            }
        }

        public async Task<HistoryChangeStudent> GetHistoryChangeStudent(int id, bool include = true)
        {
            if (include)
            {
                return await _context.HistoryChangeStudent.Include(x => x.Student).Include(x => x.Student.BasisOfLerning).Include(x => x.Student.Group).Include(x => x.Student.EventOfStudents).FirstOrDefaultAsync(x => x.Id == id);
            }
            else
            {
                return await _context.HistoryChangeStudent.FirstOrDefaultAsync(x => x.Id == id);
            }
        }
    }
}

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
    public class EFSpeciality : ISpeciality
    {
        DiplomContext _context;
        public EFSpeciality(DiplomContext context)
        {
            _context = context;
        }

        public async Task AllDeleteSpesciality()
        {
            foreach (Speciality item in _context.Speciality.Include(x=>x.GroupsDirectory))
            {
                if (item.GroupsDirectory.Count == 0)
                {
                    _context.Speciality.Remove(item);
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task CreateSpeciality(string title, string code)
        {
            if (!_context.Speciality.Any(x=>x.Code.Equals(code)))
            {
                Speciality speciality = new Speciality()
                {
                    Title = title,
                    Code = code
                };
                await _context.Speciality.AddAsync(speciality);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Speciality> GetSpeciality(string title)
        {
            return await _context.Speciality.FirstOrDefaultAsync(x => x.Code.Equals(title));
        }
    }
}

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
    public class EFFormOfStudy:IFormOfStudy
    {
        DiplomContext _context;
        public EFFormOfStudy(DiplomContext context) 
        {
            _context = context;
        }

        public async Task<List<FormOfStudy>> GetAllFormOfStudy(bool include = false)
        {
            if (include)
            {
                return await _context.FormOfStudy.Include(x=>x.Groups).ToListAsync();
            }
            else
            {
                return await _context.FormOfStudy.ToListAsync();
            }
        }

        public async Task<FormOfStudy> GetFormOfStudy(int id, bool include = false)
        {
            if (include)
            {
                return await _context.FormOfStudy.Include(x => x.Groups).FirstOrDefaultAsync(x=>x.Id == id);
            }
            else
            {
                return await _context.FormOfStudy.FirstOrDefaultAsync(x => x.Id == id);
            }
        }
    }
}

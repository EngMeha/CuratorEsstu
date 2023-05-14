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
    public class EFBasisOfLearning : IBasisOfLearning
    {
        DiplomContext _context;
        public EFBasisOfLearning(DiplomContext context) 
        {
            _context = context;
        }
        public async Task<BasisOfLearning> GetBasisOfLearning(string title, bool include = false)
        {
            if (title.Equals("договор"))
            {
                title = "контракт";
            }
            if (include)
            {
                return await _context.BasisOfLearning.Include(x => x.Students).FirstOrDefaultAsync(x => x.Title.ToLower().Equals(title.ToLower()));
            }
            else
            {
                return  await _context.BasisOfLearning.FirstOrDefaultAsync(x => x.Title.ToLower().Equals(title.ToLower()));
            }
        }

        public async Task<List<BasisOfLearning>> GetBasisOfLearnings(bool include = false)
        {
            if (include)
            {
                return await _context.BasisOfLearning.Include(x=>x.Students).ToListAsync();
            }
            else
            {
                return await _context.BasisOfLearning.ToListAsync();
            }
        }
    }
}

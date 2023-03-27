using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class InitializeDB
    {
        public async static Task InitDB(DiplomContext _context) 
        {
            //инициализация
            if (!await _context.BasisOfLearning.AnyAsync())
            {
                _context.BasisOfLearning.Add(new Entity.BasisOfLearning() { Title = "Бюджет" });
                _context.BasisOfLearning.Add(new Entity.BasisOfLearning() { Title = "Договор" });
                _context.BasisOfLearning.Add(new Entity.BasisOfLearning() { Title = "Целевое" });
            }
            if (!await _context.FormOfStudy.AnyAsync())
            {
                _context.FormOfStudy.AddRange(new Entity.FormOfStudy() { Title = "Очная" });
                _context.FormOfStudy.AddRange(new Entity.FormOfStudy() { Title = "Заочная" });
            }
            await _context.SaveChangesAsync();
        }
    }
}

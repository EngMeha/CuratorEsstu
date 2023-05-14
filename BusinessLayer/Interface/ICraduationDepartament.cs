using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface ICraduationDepartament
    {
        Task<CraduationDepartament> GetCraduationDepartament(string title, bool include = false);
        Task<List<CraduationDepartament>> GetAllCraduationDepartament(bool include = false);
        Task CreateDepartment(string title);
    }
}

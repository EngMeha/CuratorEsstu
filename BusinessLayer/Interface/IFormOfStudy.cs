using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IFormOfStudy
    {
        Task<FormOfStudy> GetFormOfStudy(int id, bool include = false);
        Task<List<FormOfStudy>> GetAllFormOfStudy(bool include = false);
    }
}

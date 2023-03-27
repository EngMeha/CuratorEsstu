using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IHistoryChangeStudent
    {
        Task<HistoryChangeStudent> GetHistoryChangeStudent(int id, bool include = true);
        Task<List<HistoryChangeStudent>> GetAllHistoryChangeStudent(bool include = true);
        Task CreateHistoryChangeStudent(List<Student> students);
        Task DeleteHistoryChangeStudent(List<Student> students);
    }
}

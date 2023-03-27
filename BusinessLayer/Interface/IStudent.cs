using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IStudent
    {
        Task<Student> GetStudent(int id, bool include = true);
        Task<List<Student>> GetAllStudent(bool include = true);
        Task SaveStudent(Student student);
        Task DeleteStudent(Student student);
    }
}

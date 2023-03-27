using DataLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IBasisOfLearning
    {
        Task<BasisOfLearning> GetBasisOfLearning(int id, bool include = false);
        Task<List<BasisOfLearning>> GetBasisOfLearnings(bool include = false);
    }
}

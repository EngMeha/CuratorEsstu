using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationParser.Interface
{
    public interface ICultureParser: IParser
    {
        public List<string> Urls { get; set; }
    }
}

using DataLayer.Entity;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Mappers
{
    public static class AllEventMapper
    {   
        public static AllEventModel FromDbToModel(List<Event> events)
        {
            return new AllEventModel() { Events = events };
        }
    }
}

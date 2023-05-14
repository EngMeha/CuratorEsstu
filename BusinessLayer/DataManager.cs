using BusinessLayer.Interface;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DataManager
    {
        IBasisOfLearning _basisOfLearning;
        ICraduationDepartament _craduationDepartament;
        IEvent _event;
        IEventOfStudent _eventOfStudent;
        IGroup _group;
        IHistoryChangeStudent _historyChangeStudent;
        IStudent _student;
        IUser _user;
        IGropsDepartmen _gropsDepartmen;
        ISpeciality _speciality;
        public DataManager(IBasisOfLearning basisOfLearning, ICraduationDepartament craduationDepartament, IEvent @event,
            IEventOfStudent eventOfStudent, IGroup group, IHistoryChangeStudent historyChangeStudent,
            IStudent student, IUser user, IGropsDepartmen gropsDepartmen, ISpeciality speciality) 
        {
            _basisOfLearning = basisOfLearning;
            _craduationDepartament= craduationDepartament;
            _event = @event;
            _eventOfStudent= eventOfStudent;
            _group= group;
            _historyChangeStudent= historyChangeStudent;
            _student= student;
            _user = user;
            _gropsDepartmen = gropsDepartmen;
            _speciality = speciality;
        }

        public IBasisOfLearning BasisOfLearnings { get { return _basisOfLearning; } }
        public ICraduationDepartament CraduationDepartaments { get { return _craduationDepartament; } }
        public IEvent Events { get { return _event; } }
        public IEventOfStudent EventOfStudents { get { return _eventOfStudent; } }
        public IGroup Groups { get { return _group; } }
        public IHistoryChangeStudent HistoryChangeStudents { get { return _historyChangeStudent; } }
        public IStudent Students { get { return _student; } }
        public IUser Users { get { return _user; } }
        public IGropsDepartmen GropsDepartmen { get { return _gropsDepartmen; } }
        public ISpeciality Speciality { get { return _speciality; } }
    }
}

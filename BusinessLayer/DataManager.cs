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
        IFormOfStudy _formOfStudy;
        IGroup _group;
        IHistoryChangeStudent _historyChangeStudent;
        IStudent _student;
        IUser _user;
        public DataManager(IBasisOfLearning basisOfLearning, ICraduationDepartament craduationDepartament, IEvent @event,
            IEventOfStudent eventOfStudent, IFormOfStudy formOfStudy, IGroup group, IHistoryChangeStudent historyChangeStudent,
            IStudent student, IUser user) 
        {
            _basisOfLearning = basisOfLearning;
            _craduationDepartament= craduationDepartament;
            _eventOfStudent= eventOfStudent;
            _formOfStudy= formOfStudy;
            _group= group;
            _historyChangeStudent= historyChangeStudent;
            _student= student;
            _user = user;
        }

        public IBasisOfLearning BasisOfLearnings { get { return _basisOfLearning; } }
        public ICraduationDepartament CraduationDepartaments { get { return _craduationDepartament; } }
        public IEvent Events { get { return _event; } }
        public IEventOfStudent EventOfStudents { get { return _eventOfStudent; } }
        public IFormOfStudy FormOfStudyes { get { return _formOfStudy; } }
        public IGroup Groups { get { return _group; } }
        public IHistoryChangeStudent HistoryChangeStudents { get { return _historyChangeStudent; } }
        public IStudent Students { get { return _student; } }
        public IUser Users { get { return _user; } }

    }
}

using BusinessLayer;
using PresentationLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer
{
    public class ServiceManager
    {
        UserService _userService;
        EventService _eventService;
        GroupOfTeacherService _groupOfTeacherService;
        DataManager _dataManager;
        public ServiceManager(DataManager dataManager) 
        {
            _dataManager = dataManager;
            _userService = new UserService(_dataManager);
            _eventService = new EventService(_dataManager);
            _groupOfTeacherService = new GroupOfTeacherService(_dataManager);
        }

        public UserService UserService { get { return _userService; } }
        public EventService EventService { get { return _eventService; } }
        public GroupOfTeacherService GroupOfTeacherService { get { return _groupOfTeacherService; } }
    }
}

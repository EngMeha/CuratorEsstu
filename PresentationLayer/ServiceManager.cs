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
        DataManager _dataManager;
        public ServiceManager(DataManager dataManager) 
        {
            _dataManager = dataManager;
            _userService = new UserService(_dataManager);
        }

        public UserService UserService { get { return _userService; } }
    }
}

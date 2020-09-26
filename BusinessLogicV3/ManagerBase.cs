using EventAggregatorLibrary;
using MedAssistBusinessLogic.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedAssistBusinessLogic.Events;

namespace MedAssistBusinessLogic
{
    public abstract class ManagerBase
    {
        protected AccountInfo _loggedInUser;

        protected IEventAggregator _eventAggregator;

        protected ManagerBase(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<UserLoggedInEvent>().Subscribe(OnUserLoggedIn);
            _eventAggregator.GetEvent<UserLoggedOutEvent>().Subscribe(OnUserLoggedOut);
        }

        private void OnUserLoggedIn(AccountInfo user)
        {
            _loggedInUser = user;
        }
        private void OnUserLoggedOut()
        {
            _loggedInUser = null;
        }
    }
}

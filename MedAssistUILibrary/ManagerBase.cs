using EventAggregatorLibrary;
using MedAssistUILibrary.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UILibrary.Events;

namespace MedAssistUILibrary
{
    public abstract class ManagerBase
    {
        protected UserInfo _loggedInUser;

        protected IEventAggregator _eventAggregator;

        protected ManagerBase(IEventAggregator eventAggregator)
        {
            eventAggregator = _eventAggregator;

            _eventAggregator.GetEvent<UserLoggedInEvent>().Subscribe(OnUserLoggedIn);
            _eventAggregator.GetEvent<UserLoggedOutEvent>().Subscribe(OnUserLoggedOut);
        }

        private void OnUserLoggedIn(UserInfo user)
        {
            _loggedInUser = user;
        }
        private void OnUserLoggedOut()
        {
            _loggedInUser = null;
        }
    }
}

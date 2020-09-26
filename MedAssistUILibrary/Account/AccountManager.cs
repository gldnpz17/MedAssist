using UILibrary.Authorization;
using UILibrary.Events;
using UILibrary.Models;
using EventAggregatorLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace UILibrary.Accounts
{
    public class AccountManager
    {
        private UserCredential _loggedInUser;

        private IEventAggregator _eventAggregator;

        public AccountManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<UserLoggedInEvent>().Subscribe(OnUserLoggedIn);
            _eventAggregator.GetEvent<UserLoggedOutEvent>().Subscribe(OnUserLoggedOut);
        }

        private void OnUserLoggedIn(UserCredential credential)
        {
            _loggedInUser = credential;
        }
        private void OnUserLoggedOut()
        {
            _loggedInUser = null;
        }

        public EndUserInfoModel GetInfo()
        {
            //request UserInfoModel from the API
            if (_loggedInUser.AuthToken == "ExampleAuthToken420")
            {
                return new EndUserInfoModel() { FullName = "Lorem Ipsum" };
            }
            else
            {
                throw new Exception(); //TO-DO : throw a better exception
            }
        }

        public List<string> GetAllRoles()
        {
            return new List<string>() { "role1", "role2" };
        }
    }
}

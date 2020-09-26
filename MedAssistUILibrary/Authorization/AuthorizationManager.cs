using UILibrary.Events;
using UILibrary.Models;
using EventAggregatorLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Runtime.CompilerServices;
using MedAssistUILibrary.Models;
using MedAssistUILibrary.Authorization;
using NewDataAccessLibrary.Models;
using NewDataAccessLibrary;
using System.Linq;

namespace UILibrary.Authorization
{
    public class AuthorizationManager
    {
        private IEventAggregator _eventAggregator;

        public AuthorizationManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void LogIn(string username, string password)
        {
            User matchingUser;
            using(MedAssistContext context = new MedAssistContext())
            {
                var query = from user in context.Users
                            where (user.Username == username) && (user.Password == password)
                            select user;

                matchingUser = query.ToList().First();
            }

            //if no matches were found
            if(matchingUser == null)
            {
                throw new Exception("Invalid username or password.");
            }

            //publish event
            _eventAggregator.GetEvent<UserLoggedInEvent>().Publish(new UserInfo()
            {
                Id = matchingUser.Id,
                Username = matchingUser.Username,
                FullName = matchingUser.FullName,
                Role = matchingUser.Role,
                RoleInfoId = matchingUser.RoleInfoId
            });
        }

        public void LogOut()
        {
            //publish event
            _eventAggregator.GetEvent<UserLoggedOutEvent>().Publish();
        }

        public void Register(EndUserRegistrationFormModel registrationForm)
        {
            
        }
    }
}

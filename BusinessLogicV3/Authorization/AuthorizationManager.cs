using MedAssistBusinessLogic.Events;
using EventAggregatorLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Runtime.CompilerServices;
using MedAssistBusinessLogic.Authorization;
using System.Linq;
using DataAccessV3;
using DataAccessV3.Models;

namespace MedAssistBusinessLogic.Authorization
{
    public class AuthorizationManager
    {
        private IEventAggregator _eventAggregator;
        private AccountInfo _loggedInAccount;

        public AuthorizationManager(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void LogIn(string username, string password)
        {
            using (var context = new MedAssistContext())
            {
                var matchingAccount = context.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password);

                if (matchingAccount == null)
                {
                    throw new Exception("Incorrect username or password.");
                }

                //publish event
                AccountInfo account = new AccountInfo()
                {
                    Id = matchingAccount.Id,
                    Username = matchingAccount.Username,
                    IsHospital = matchingAccount.IsHospital,
                    IsPharmacy = matchingAccount.IsPharmacy
                };
                _loggedInAccount = account;
                _eventAggregator.GetEvent<UserLoggedInEvent>().Publish(_loggedInAccount);
            }
        }

        public void LogOut()
        {
            //publish event
            _eventAggregator.GetEvent<UserLoggedOutEvent>().Publish();
        }

        public AccountInfo GetUserInfo()
        {
            return _loggedInAccount;
        }

        public void Register(string username, string password)
        {
            using (var context = new MedAssistContext())
            {
                context.Accounts.Add(new Account()
                {
                    Username = username,
                    Password = password,
                    IsHospital = false,
                    IsPharmacy = false,
                    MedWalletBalance = 0m
                });
                context.SaveChanges();
            }
        }
    }
}

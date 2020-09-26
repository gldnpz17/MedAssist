using UILibrary.Authorization;
using EventAggregatorLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using MedAssistUILibrary.Authorization;

namespace UILibrary.Events
{
    internal class UserLoggedInEvent : AggregateEvent<UserInfo>
    {
        
    }
}

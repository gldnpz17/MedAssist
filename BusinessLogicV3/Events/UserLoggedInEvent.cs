using MedAssistBusinessLogic.Authorization;
using EventAggregatorLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace MedAssistBusinessLogic.Events
{
    internal class UserLoggedInEvent : AggregateEvent<AccountInfo>
    {
        
    }
}

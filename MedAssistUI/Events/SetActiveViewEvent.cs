using EventAggregatorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MedAssistUI.Events
{
    class MainViewChildSetEvent : AggregateEvent<UserControl>
    {
    }
}

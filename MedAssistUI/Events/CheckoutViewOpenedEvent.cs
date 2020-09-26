using EventAggregatorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BusinessLogicV3.Models;

namespace MedAssistUI.Events
{
    class CheckoutViewOpenedEvent : AggregateEvent<ObservableCollection<MedicationRequestModel>>
    {
    }
}

using EventAggregatorLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicV3.Models;

namespace MedAssistUI.Events
{
    class MedicationRequestAddedEvent : AggregateEvent<MedicationRequestModel>
    {
    }
}

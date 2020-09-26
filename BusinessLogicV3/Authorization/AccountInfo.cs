using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistBusinessLogic.Authorization
{
    public class AccountInfo
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public bool IsHospital { get; set; }
        public bool IsPharmacy { get; set; }
    }
}

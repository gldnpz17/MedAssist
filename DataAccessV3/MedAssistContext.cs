using DataAccessV3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessV3
{
    public class MedAssistContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AmbulanceRequest> AmbulanceRequests { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<AppointmentRequest> AppointmentRequests { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationRequest> MedicationRequests { get; set; }
        public DbSet<MedWalletTransaction> MedWalletTransactions { get; set; }
        public DbSet<MedWalletVoucher> MedWalletVouchers { get; set; }
    }
}

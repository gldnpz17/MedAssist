using NewDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDataAccessLibrary
{
    public class MedAssistContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<EndUser> EndUsers { get; set; }
        public DbSet<HealthcareUnit> HealthcareUnits { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
        public DbSet<HealthcareStaff> HospitalStaffs { get; set; }
        public DbSet<AmbulanceRequest> AmbulanceRequests { get; set; }
        public DbSet<AppointmentRequest> AppointmentRequests { get; set; }

        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationStock> MedicationStocks { get; set; }
        public DbSet<MedicationRequest> MedicationRequests { get; set; }

        public DbSet<MedWalletTransaction> MedWalletTransactions { get; set; }
        public DbSet<MedWalletVoucher> MedWalletVouchers { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AsistensiMedisDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class DatabaseAmbulance
    {
        public int ID { get; set; }
        public Nullable<System.Guid> HealthcareID { get; set; }
        public Nullable<int> JumlahAmbulans { get; set; }
        public Nullable<float> HealthcareLatitude { get; set; }
        public Nullable<float> HealthcareLongitude { get; set; }
    }
}

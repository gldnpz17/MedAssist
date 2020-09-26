using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryMain
{
    public class DoctorInfo
    {
        public string DoctorName { get; set; }
        public Guid DoctorID { get; set; }
        public int DoctorAge { get; set; }
        public byte[] DoctorPicture { get; set; }
        public string DoctorHospital { get; set; }
        public DoctorType doctorType { get; set; }

        public DoctorInfo(string doctorName, Guid doctorID, int doctorAge, byte[] doctorPicture, string doctorHospital, DoctorType doctorType)
        {
            DoctorName = doctorName;
            DoctorID = doctorID;
            DoctorAge = doctorAge;
            DoctorPicture = doctorPicture;
            DoctorHospital = doctorHospital;
            this.doctorType = doctorType;
        }
    }
}

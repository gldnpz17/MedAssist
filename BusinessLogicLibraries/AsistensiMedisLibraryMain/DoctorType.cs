using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryMain
{
    public enum DoctorType
    {
        Psikolog = 1,
        [Display(Name = "Dokter Umum")]
        DokterUmum =2
    }
}

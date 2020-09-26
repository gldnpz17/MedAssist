using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsistensiMedisLibraryMain
{
    public enum TipeUser
    {
        Pemakai = 1,
        [Display(Name ="Pusat Kesehatan")]
        RumahSakit = 2,
        Apotek = 3
    }
}

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
    
    public partial class DatabaseMedicine
    {
        public int ObatID { get; set; }
        public string NamaObat { get; set; }
        public string JenisObat { get; set; }
        public byte[] GambarObat { get; set; }
        public string Manfaat { get; set; }
        public Nullable<decimal> HargaObat { get; set; }
        public string Keterangan { get; set; }
    }
}

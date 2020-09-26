using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace AsistensiMedisLibraryMain
{
    public class MedicineRequest : Request
    {
        public GeoCoordinate Location;
        public IEnumerable<Medicine> Medicines;
    }
}

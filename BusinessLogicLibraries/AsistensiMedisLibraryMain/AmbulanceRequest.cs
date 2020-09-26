using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;

namespace AsistensiMedisLibraryMain
{
    public class AmbulanceRequest : Request
    {
        public GeoCoordinate Location;
    }
}

using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedAssistBusinessLogic.LocationService
{
    public class LocationService : ILocationService
    {
        GeoCoordinateWatcher _watcher;

        public LocationService()
        {
            _watcher = new GeoCoordinateWatcher();
            _watcher.Start();
        }
        public GeoCoordinate GetLocation()
        {
            if (_watcher.Permission == GeoPositionPermission.Denied || _watcher.Permission == GeoPositionPermission.Unknown)
            {
                throw new Exception("Unable to acquire coordinates. Please check your location privacy settings.");
            }

            return _watcher.Position.Location;
        }
    }
}

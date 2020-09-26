using System.Device.Location;

namespace MedAssistBusinessLogic.LocationService
{
    public interface ILocationService
    {
        GeoCoordinate GetLocation();
    }
}
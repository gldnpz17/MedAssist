using System.Device.Location;

namespace MedAssistUILibrary
{
    public interface ILocationService
    {
        GeoCoordinate GetLocation();
    }
}
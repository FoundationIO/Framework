using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure.Utils
{
    public static class GpsUtils
    {
        public static double DistanceBetween(double fromLatitude, double fromLongitude, double toLatitude, double toLongitude)
        {
            var fromCoord = new GeoCoordinate(fromLatitude, fromLongitude);
            var toCoord = new GeoCoordinate(toLatitude, toLongitude);

            return fromCoord.GetDistanceTo(toCoord);
        }
    }
}

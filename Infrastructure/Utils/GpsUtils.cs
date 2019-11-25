/**
Copyright (c) 2016 Foundation.IO (https://github.com/foundationio). All rights reserved.

This work is licensed under the terms of the BSD license.
For a copy, see <https://opensource.org/licenses/BSD-3-Clause>.
**/
using GeoCoordinatePortable;

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

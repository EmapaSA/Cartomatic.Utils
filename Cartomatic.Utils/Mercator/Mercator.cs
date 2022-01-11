using System;
using System.Collections.Generic;
using System.Text;

namespace Cartomatic.Utils
{
    /// <summary>
    /// Mercator utils
    /// </summary>
    public class Mercator
    {
        //borrowed from https://gist.github.com/onderaltintas/6649521

        private const double EarthRadius = 6378137;


        /// <summary>
        /// 180 equatorial degrees in metres
        /// </summary>
        private const double SphericalMercatorHalfSphere = EarthRadius * Math.PI;

        private const double SphericalMercatorSphere = SphericalMercatorHalfSphere * 2;



        /// <summary>
        /// Converts lon lat to spherical mercator
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public static (double x, double y) FromLonLat(double lon, double lat)
        {
            //20037508.34 is spherical mercator  half sphere (0 - 180)
            var x = lon * SphericalMercatorHalfSphere / 180;

            var y = Math.Log(Math.Tan((90 + lat) * Math.PI / 360)) / (Math.PI / 180);
            y *= SphericalMercatorHalfSphere / 180;

            return (x, y);
        }

        /// <summary>
        /// Converts spherical mercator to lon lat
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static (double lon, double lat) ToLonLat(double x, double y)
        {
            var lon = x * 180 / SphericalMercatorHalfSphere;
            var lat = Math.Atan(Math.Exp(y * Math.PI / SphericalMercatorHalfSphere)) * 360 / Math.PI - 90;

            return (lon, lat);
        }


        /// <summary>
        /// Gets a spherical mercator tile bounding box from its grid address
        /// </summary>
        /// <param name="z"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="buffer">Buffer to add to each side of a bounding box in order to extend it. buffer is expressed in pixels at a given zoom level</param>
        /// <returns></returns>
        public static (double l, double b, double r, double t) GetSphericalMercatorBboxFromTileAddress(int z, int x, int y, int buffer = 0)
        {
            var tileSizeAtZoom = SphericalMercatorSphere / Math.Pow(2, z);
            var pixelSizeAtZoom = tileSizeAtZoom / 256;
            var collar = buffer * pixelSizeAtZoom;

            return (
                -SphericalMercatorHalfSphere + x * tileSizeAtZoom - collar,
                SphericalMercatorHalfSphere - y * tileSizeAtZoom + collar,
                -SphericalMercatorHalfSphere + x * tileSizeAtZoom + tileSizeAtZoom + collar,
                SphericalMercatorHalfSphere - y * tileSizeAtZoom - tileSizeAtZoom - collar
            );
        }
        
        /// <summary>
        /// Gets a spherical mercator tile address for given coordinate and zoom level
        /// </summary>
        /// <param name="mercatorX"></param>
        /// <param name="mercatorY"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static (int x, int y) GetTileAddressFromCoord(double mercatorX, double mercatorY, int z)
        {
            var tileSizeAtZoom = SphericalMercatorSphere / Math.Pow(2, z);

            var x = (int)Math.Floor(Math.Abs(-SphericalMercatorHalfSphere - mercatorX) / tileSizeAtZoom);
            var y = (int)Math.Floor(Math.Abs(SphericalMercatorHalfSphere - mercatorY) / tileSizeAtZoom);

            return (x, y);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Cartomatic.Utils.Drawing
{
    public static class RectangleExtensions
    {
        /// <summary>
        /// Converts a rectangle to path
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static System.Drawing.Drawing2D.GraphicsPath ToPath(this System.Drawing.Rectangle r)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddRectangle(new System.Drawing.Rectangle(r.X, r.Y, r.Width, r.Height));
            return path;
        }
    }
}

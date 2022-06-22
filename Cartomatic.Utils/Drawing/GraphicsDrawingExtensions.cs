using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Cartomatic.Utils.Drawing
{
    public static class GraphicsDrawingExtensions
    {
        /// <summary>
        /// Draws a shadow around a path
        /// </summary>
        /// <param name="g"></param>
        /// <param name="path"></param>
        /// <param name="color"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="feathering"></param>
        /// <param name="opacity"></param>
        public static void DrawShadow(this Graphics g, GraphicsPath path, Color color, int offsetX, int offsetY, int feathering, int opacity)
        {
            var clone = (GraphicsPath)path.Clone();
            
            var matrix = new System.Drawing.Drawing2D.Matrix();
            matrix.Translate(offsetX, offsetY);

            clone.Transform(matrix);

            g.DrawGlow(clone, color, feathering, opacity, fillPath: true);
        }

        /// <summary>
        /// Draws a shadow around a rect
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <param name="feathering"></param>
        /// <param name="opacity"></param>
        public static void DrawShadow(this Graphics g, Rectangle rect, Color color, int offsetX, int offsetY, int feathering, int opacity)
            => g.DrawShadow(rect.ToPath(), color, offsetX, offsetY, feathering, opacity);


        /// <summary>
        /// Draws a glow around an object
        /// </summary>
        /// <param name="g"></param>
        /// <param name="path"></param>
        /// <param name="color"></param>
        /// <param name="feathering"></param>
        /// <param name="opacity"></param>
        /// <param name="fillPath"></param>
        public static void DrawGlow(this Graphics g, GraphicsPath path, Color color, int feathering, int opacity, bool fillPath = false)
        {
            var baseAlpha = opacity;

            var baseColor = Color.FromArgb(baseAlpha, color);

            //each step alpha - glow should change from transparent to base color
            var stepAlpha = (int)Math.Round((double)(255 - baseAlpha) / (double)feathering);

            var stepColor = Color.FromArgb(stepAlpha, color);


            //Path bounding rectangle
            var br = path.GetBounds();

            //glow extent rectangle
            var ge = new RectangleF(br.X - feathering, br.Y - feathering, br.Width + feathering * 2, br.Height + feathering * 2);

            //Note - the above seems to be not needed. it should be enough to add a bounding box of graphicS!!!

            //create ne clipping path
            var cp = new GraphicsPath();
            cp.AddPath(path, true);
            cp.AddRectangle(ge);

            //fill path with the base color if needed
            if (fillPath)
            {
                g.FillPath(new System.Drawing.SolidBrush(baseColor), path);
            }

            //Apply clipping region that will restrict the area we are painting on
            g.Clip = new System.Drawing.Region(cp);

            for (var i = 1; i < feathering; ++i)
            {
                var pen = new Pen(stepColor, 1 * i);
                pen.LineJoin = LineJoin.Round;
                g.DrawPath(pen, path);
                pen.Dispose();
            }
        }

        /// <summary>
        /// Draws a glow around a rectangle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        /// <param name="feathering"></param>
        /// <param name="opacity"></param>
        /// <param name="fillPath"></param>
        public static void DrawGlow(this Graphics g, Rectangle rect, Color color, int feathering, int opacity, bool fillPath = false)
            => g.DrawGlow(rect.ToPath(), color, feathering, opacity, fillPath);
    }
}

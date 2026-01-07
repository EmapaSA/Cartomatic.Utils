using System;
using System.Drawing;
using System.Drawing.Imaging;
using AwesomeAssertions;
using NUnit.Framework;
#pragma warning disable CA1416

namespace Cartomatic.Utils.Drawing.Tests
{
    [TestFixture]
    public class DrawingTests
    {
        [Test]
        public void SetBitmapOpacity_WhenCalled_ShouldApplyOpacityAsExpected()
        {
            var testBitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(testBitmap))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255, 255)), new Rectangle(0, 0, testBitmap.Width, testBitmap.Height));
            }
            var color1 = testBitmap.GetPixel(0, 0);

            var modifiedBitmap = testBitmap.SetBitmapOpacity(0.5);
            var color2 = modifiedBitmap.GetPixel(0, 0);

            color2.A.Should().Be((byte)(Math.Round(255*0.5)));
        }

        [Test]
        public void OverlayBitmapWithOpacity_WhenCalled_ShouldMixArgbAsExpected()
        {
            var opacity = 0.5;
            var testBitmap1 = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            
            using (var g = Graphics.FromImage(testBitmap1))
            {
                g.FillRectangle(new SolidBrush(Color.IndianRed), new Rectangle(0, 0, testBitmap1.Width, testBitmap1.Height));
            }
            var col1 = testBitmap1.GetPixel(0, 0);

            var testBitmap2 = new Bitmap(1, 1, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(testBitmap2))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(255, 128, 128, 128)), new Rectangle(0, 0, testBitmap2.Width, testBitmap2.Height));
            }
            var col2 = testBitmap2.GetPixel(0, 0);


            var modifiedBitmap = testBitmap1.OverlayBitmapWithOpacity(testBitmap2, opacity);
            var col3 = modifiedBitmap.GetPixel(0, 0);

            //totally unsure how the colors get blended, so this test kinda sucks really
            //but for the time being the colors were dumped manually and the output looks ok.
            //looking at the input colors
            //205, 92, 92
            //128,128,128
            //when summed and multiplied by opacity give proper output of
            //166,110,110
            
            col3.A.Should().Be(255);
            //col3.R.Should().Be(166);
            //col3.G.Should().Be(110);
            //col3.B.Should().Be(110);

            //This does not work for all the colors and somtimes rounding does not work ok too
            //basically - no nearly enough knowledge to test it properly

            col3.R.Should().Be((byte)Math.Round((col1.R + col2.R) * opacity));
            col3.G.Should().Be((byte)Math.Round((col1.G + col2.G) * opacity));
            col3.B.Should().Be((byte)Math.Round((col1.B + col2.B) * opacity));
        }
    }
}

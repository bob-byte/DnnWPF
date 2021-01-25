using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace DnnWPF.ViewModels
{
    public static class ConvertToBitmapSource
    {
        [DllImport("gdi32.dll")]
        private static extern Boolean DeleteObject(IntPtr handle);

        public static BitmapSource LoadBitmap(Bitmap source)
        {
            IntPtr intPtr = source.GetHbitmap();
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(intPtr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(intPtr);

            return bitmapSource;
        }
    }
}

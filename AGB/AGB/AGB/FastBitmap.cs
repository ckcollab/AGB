using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AGB;

internal class FastBitmap
{
	private Bitmap image;

	private BitmapData bitmapData;

	private int height;

	private int width;

	private byte[] rgbValues;

	private bool locked = false;

	public int Height => height;

	public int Width => width;

	public FastBitmap(int x, int y)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		width = x;
		height = y;
		image = new Bitmap(x, y);
	}

	public byte[] GetAllPixels()
	{
		return rgbValues;
	}

	public void SetAllPixels(byte[] pixels)
	{
		rgbValues = pixels;
	}

	public Color GetPixel(int x, int y)
	{
		int blue = rgbValues[(y * ((Image)image).get_Width() + x) * 4];
		int green = rgbValues[(y * ((Image)image).get_Width() + x) * 4 + 1];
		int red = rgbValues[(y * ((Image)image).get_Width() + x) * 4 + 2];
		return Color.FromArgb(red, green, blue);
	}

	public void SetPixel(int x, int y, Color cIn)
	{
		int index = (y * ((Image)image).get_Width() + x) * 4;
		rgbValues[index] = cIn.B;
		rgbValues[index + 1] = cIn.G;
		rgbValues[index + 2] = cIn.R;
	}

	public static implicit operator Image(FastBitmap bmp)
	{
		return (Image)(object)bmp.image;
	}

	public static implicit operator Bitmap(FastBitmap bmp)
	{
		return bmp.image;
	}

	public void LockPixels()
	{
		LockPixels(new Rectangle(0, 0, ((Image)image).get_Width(), ((Image)image).get_Height()));
	}

	private void LockPixels(Rectangle area)
	{
		if (!locked)
		{
			locked = true;
			bitmapData = image.LockBits(area, (ImageLockMode)3, (PixelFormat)139273);
			IntPtr ptr = bitmapData.get_Scan0();
			int stride = bitmapData.get_Stride();
			int numBytes = ((Image)image).get_Width() * ((Image)image).get_Height() * 4;
			rgbValues = new byte[numBytes];
			Marshal.Copy(ptr, rgbValues, 0, numBytes);
		}
	}

	public void UnlockPixels()
	{
		if (locked)
		{
			locked = false;
			Marshal.Copy(rgbValues, 0, bitmapData.get_Scan0(), ((Image)image).get_Width() * ((Image)image).get_Height() * 4);
			image.UnlockBits(bitmapData);
		}
	}
}

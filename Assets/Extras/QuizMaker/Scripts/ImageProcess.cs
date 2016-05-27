using UnityEngine;
using System;
using System.Collections;

public static class ImageProcess
{
    /// <summary>
    /// Pixelates the texture.
    /// </summary>
    /// <param name="t"> Texture that is processed.</param>
    /// <param name="size"> Size of the pixel.</param>
    public static Texture2D SetPixelate(Texture2D t, int size)
    {
        Texture2D tex_ = new Texture2D(t.width, t.height, TextureFormat.ARGB32, true);
        Rect rectangle = new Rect(0, 0, t.width, t.height);
        for (int xx = (int)rectangle.x; xx < rectangle.x + rectangle.width && xx < t.width; xx += size)
        {
            for (int yy = (int)rectangle.y; yy < rectangle.y + rectangle.height && yy < t.height; yy += size)
            {
                int offsetX = size / 2;
                int offsetY = size / 2;
                while (xx + offsetX >= t.width) offsetX--;
                while (yy + offsetY >= t.height) offsetY--;
                Color pixel = t.GetPixel(xx + offsetX, yy + offsetY);
                for (Int32 x = xx; x < xx + size && x < t.width; x++)
                    for (Int32 y = yy; y < yy + size && y < t.height; y++)
                        tex_.SetPixel(x, y, pixel);
            }
        }

        tex_.Apply();
        return tex_;
    }
}

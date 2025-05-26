using System;
using System.Drawing;
using System.Collections;

class Image 
{
    private int _width;          // The width of the image.
    private int _height;         // The height of the image.
    private float _gamma;        // The gamma value of the monitor
    private Bitmap _solution;    // The Bitmap object representing the image.

    /// <summary>
    /// Initializes a new instance of the Image class with the specified width, height, and gamma value.
    /// </summary>
    /// <param name="w">The width of the image.</param>
    /// <param name="h">The height of the image.</param>
    /// <param name="g">The gamma value of the monitor.</param>
    public Image(int w, int h, float g = 1.8f)
    {
        // Create the Bitmap object with the specified width and height and gamma value
        _width = w;
        _height = h;
        _gamma = g;
        _solution = new Bitmap(_width, _height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
        
        // Initialize the image with a black background
        for (int i = 0; i < _height; i++)
        {
            for (int j = 0; j < _width; j++)
            {
                _solution.SetPixel(j, i, Color.Black);
            }
        }
    }

    /// <summary>
    /// Paints a pixel at the specified position with the specified color and alpha value.
    /// </summary>
    /// <param name="i">The X coordinate (column) of the pixel.</param>
    /// <param name="j">The Y coordinate (row) of the pixel.</param>
    /// <param name="rgb">The color represented as a Vector (red, green, blue).</param>
    /// <param name="alpha">The alpha value (transparency) of the pixel.</param>
    public void Paint(int i, int j, Vector rgb, int alpha = 255)
    {
        if (i < 0 || i > _width || j < 0 || j > _height){
            Console.WriteLine("Index Out of Bounds");
            return;
        }
        int red = Clamp((int) rgb.X, 0, 255);
        int green = Clamp((int) rgb.Y, 0, 255);
        int blue = Clamp((int) rgb.Z, 0, 255); 
        
        
        // Set the pixel at the specified position with the given color and alpha
        _solution.SetPixel(i, j, Color.FromArgb(alpha, red, green, blue));
    }

    /// <summary>
    /// Gets or sets the width of the image.
    /// </summary>
    public int Width
    {
        get {return _width;}
        set {_width = value;}
    }

    /// <summary>
    /// Gets or sets the height of the image.
    /// </summary>
    public int Height
    {
        get {return _height;}
        set {_height = value;}
    }

    /// <summary>
    /// Saves the image to a file with the specified fileName.
    /// </summary>
    /// <param name="fileName">The name of the file to save the image to.</param>
    public void SaveImage(string fileName)
    {
        double gExp = 1.0 / _gamma; 
        for (int j = 0; j < _height; j++)
        {
            for (int i = 0; i < _width; i++)
            {
                Color pixel = _solution.GetPixel(i,j);
                int correctedRed = (int) (255 * Math.Pow(pixel.R / 255.0, gExp));
                int correctedGreen = (int) (255 * Math.Pow(pixel.G / 255.0, gExp));
                int correctedBlue = (int) (255 * Math.Pow(pixel.B / 255.0, gExp));
                Color correctedPixel = Color.FromArgb(pixel.A, correctedRed, correctedGreen, correctedBlue);
                _solution.SetPixel(i, j, correctedPixel);
            }
        }
        _solution.Save(fileName);
    }

    /// <summary>
    /// Creates a helper method to clamp an int value between a min and max value
    /// </summary>
    public int Clamp(int value, int min, int max){
        if (value > max){
            value = max;
        }
        if (value < min){
            value = min;
        }
        return value;
    }
}

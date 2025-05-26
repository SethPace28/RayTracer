using System;
using System.Collections;
/// <summary>
/// Class <c>Ray</c> Creates a ray with an origin, direction, and an At class.
/// Author: Seth Pace
/// Date: 10/05/2023
/// </summary>
/// <summary>
/// The <c>Camera</c> class represents a camera in a 3D graphics environment. 
/// It defines various properties and methods related to camera settings and rendering.
/// </summary>
class Camera
{
    public enum Projection
    {
        Perspective,
        Orthographic
    }

    // Private fields to store camera properties
    private Projection _projection;
    private Vector _eye;
    private Vector _lookAt;
    private Vector _up;
    private Vector _u;
    private Vector _v;
    private Vector _w;
    private float _near;
    private float _far;
    private int _width;
    private int _height;
    private float _left;
    private float _right;
    private float _top;
    private float _bottom;

    /// <summary>
    /// Creates a new <c>Camera</c> object with the specified parameters.
    /// </summary>
    /// <param name="projection">The projection type for the camera (e.g., Perspective, Orthographic).</param>
    /// <param name="eye">The position of the camera’s eye point in world coordinates.</param>
    /// <param name="lookAt">The location the camera is looking at in world coordinates.</param>
    /// <param name="up">The camera’s up vector.</param>
    /// <param name="near">The distance from the camera’s eye point to the near clipping plane (default: 0.1).</param>
    /// <param name="far">The distance from the camera’s eye point to the far clipping plane (default: 10).</param>
    /// <param name="width">The width of the camera’s viewport in pixels (default: 512).</param>
    /// <param name="height">The height of the camera’s viewport in pixels (default: 512).</param>
    /// <param name="left">The left boundary of the camera’s viewing frustum (default: -1.0).</param>
    /// <param name="right">The right boundary of the camera’s viewing frustum (default: 1.0).</param>
    /// <param name="bottom">The bottom boundary of the camera’s viewing frustum (default: -1.0).</param>
    /// <param name="top">The top boundary of the camera’s viewing frustum (default: 1.0).</param>
    
    public Camera(){
        _projection = Projection.Orthographic;
        _eye = new Vector();
        _lookAt = new Vector(0,0,-1);
        _up = new Vector(0,1,0);
        _near = 0.0f;
        _far = 2.0f;
        _width = 512;
        _height = 512;
        _left = -1.0f;
        _right = 1.0f;
        _top = 1.0f;
        _bottom = -1.0f;
        CalcBasis();
    }
    public Camera(
        Projection projection = Projection.Orthographic,
        Vector eye = null,
        Vector lookAt = null,
        Vector up = null,
        float near = 0.0f,
        float far = 2.0f,
        int width = 512,
        int height = 512,
        float left = -1.0f,
        float right = 1.0f,
        float top = 1.0f,
        float bottom = -1.0f)
    {
        if(eye == null){
            _eye = new Vector(0,0,0);
        }
        else {
            _eye = eye;
        }

        if(lookAt == null){
            _lookAt = new Vector(0,0,-1);
        }
        else {
            _lookAt = lookAt;
        }

        if(up == null) {
            _up = new Vector(0,1,0);
        }
        else {
            _up = up;
        }

        _projection = projection;
        _near = near;
        _far = far;
        _width = width;
        _height = height;
        _left = left;
        _right = right;
        _bottom = bottom;
        _top = top;
        CalcBasis();
    }

    public void CalcBasis(){
        _w = -(_lookAt - _eye);
        Vector.Normalize(ref _w);
        _u = Vector.Cross(_up, _w);
        Vector.Normalize(ref _u);
        _v = Vector.Cross(_w, _u);
        Vector.Normalize(ref _v);
    }

    /// <summary>
    /// Renders an image using the camera's settings and saves it to a file with the specified filename.
    /// </summary>
    /// <param name="fileName">The filename to save the rendered image.</param>
    public void RenderImage(string fileName, Scene scene)
    {
        Image image = new Image(_width, _height, 1.0f);
        float[,] depth = new float[_width, _height];
        Lightsaber lightsaber = new Lightsaber(new Vector(0,0,0), new Vector(0,0,0), 0, 0);
        foreach(Shape l in scene.Shapes){
            if (l.GetType() == lightsaber.GetType() && l.E){
                lightsaber = (Lightsaber) l;
            }
        }
        for (int j = 0; j < _height; j++)
        {
            for (int i = 0; i < _width; i++)
            {
                float u = _left + (((_right - _left) * i) / _width);
                float v = _bottom + (((_top - _bottom) * j) / _height);

                Ray ray = createRay(u, v);
                List<Shape> shapes = scene.Shapes;
                depth[i,j] = float.MaxValue;
                foreach (Shape s in scene.Shapes){
                    float d = depth[i,j];
                    float t = s.Hit(ray);
                    if(t < d){
                        if(t < _far && t > _near){
                            if(s.E){
                                image.Paint(i, j, s.Emit, 255);
                            }
                            else {
                                Vector vertex = ray.At(t);
                                Vector lightBase = lightsaber.Source - vertex;
                                Vector.Normalize(ref lightBase);
                                Vector lightTip = lightsaber.Tip - vertex;
                                Vector.Normalize(ref lightTip);
                                Vector lightMid = lightsaber.Center - vertex;
                                Vector.Normalize(ref lightMid);
                                
                                bool shadowOne = shadow(vertex, lightBase, scene, s, lightsaber);
                                bool shadowTwo = shadow(vertex, lightTip, scene, s, lightsaber);
                                bool shadowThree = shadow(vertex, lightMid, scene, s, lightsaber);
                                Vector color = new Vector(0,0,0);
                                
                                if(!shadowOne){
                                    color += calcColor(vertex, lightBase, s, lightsaber);
                                }
                                if (!shadowTwo){
                                    color += calcColor(vertex, lightTip, s, lightsaber);
                                }
                                if (!shadowThree){
                                    color += calcColor(vertex, lightMid, s, lightsaber);
                                }
                                if(shadowOne && shadowTwo && shadowThree){
                                    color = new Vector(20,20,20);
                                }
                                //color = calcColor(vertex, lightDirection, s);
                                image.Paint(i, j, color, 255);
                            }
                            
                            
                            depth[i,j] = t;
                        }   
                    }
                                     
                }
            }
        }        
        image.SaveImage(fileName);
    }

    /// <summary>
    /// Creates a ray that originates from pixel (u,v).
    /// </summary>
    /// <param name="u">The width coordinate of the pixel.</param>
    /// <param name="v">The height coordinate of the pixel.</param>
    public Ray createRay(float u, float v){
        Vector rayOrigin;
        Vector rayDirection;
        if (_projection == Projection.Orthographic){
            rayDirection = -_w;
            rayOrigin = _eye + (u * _u) + (v * _v);
        }
        else {
            rayOrigin = _eye;
            rayDirection = (u * _u) + (v * _v) - _w;
        }
        return new Ray(rayOrigin, rayDirection);
    }
    
    /// <summary>
    /// Calculates the color at a pixel based on the diffuse, specular, and ambient light.
    /// </summary>
    /// <param name="vertex">The point on the shape that is being hit.</param>
    /// <param name="lighDirection">The vector pointing at the light source.</param>
    /// <param name="s">The shape that is being hit</param>
    public Vector calcColor(Vector vertex, Vector lightDirection, Shape s, Shape light)
    {
        Vector diffuse = s.D * Vector.Dot(lightDirection, s.Normal(vertex));
        
        Vector eyeVector = _eye - vertex;
        Vector.Normalize(ref eyeVector);
        Vector bisector =  eyeVector + lightDirection;
        Vector.Normalize(ref bisector);

        double hn = Vector.Dot(bisector, s.Normal(vertex));
        double shiny = s.Shiny;
        Vector specular = light.Emit * (float)Math.Pow(Math.Max(0, hn), shiny);

        Vector color = s.A + diffuse + specular;
        return color;
    }

    /// <summary>
    /// Decides if an object should be in shadow
    /// </summary>
    /// <param name="vertex">The point on the shape that is being hit.</param>
    /// <param name="lighDirection">The vector pointing at the light source.</param>
    /// <param name="scene">The scene with all of the shapes</param>
    /// <param name="t">The scene with </param>
    public bool shadow(Vector vertex, Vector lightDirection, Scene scene, Shape s, Shape lightsaber){
        Ray shadow = new Ray(vertex, lightDirection);
        float t = ~(scene.Light - vertex);
        foreach(Shape d in scene.Shapes){
            if(d.Hit(shadow) > 0 && d.Hit(shadow) < t && !d.Equals(s) && !d.Equals(lightsaber)){
                return true;
            }
        }
        return false;
    }
}

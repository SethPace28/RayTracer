using System;
using System.Drawing;
using System.Collections;

abstract class Shape {
    private Vector _center;
    private Vector _diffuseColor;
    private Vector _ambientColor;
    private Vector _specularColor;
    private float _shininess;
    private bool _emits;
    private Vector _emitColor;

    public Shape(){
        _center = new Vector();
        _diffuseColor = new Vector();
        _ambientColor = new Vector(20f, 20f, 20f);
        _specularColor = new Vector(255f, 255f, 255f);
        _shininess = 128f;
        _emits = false;
        _emitColor = new Vector(0, 0, 0);
    }
    public Shape(Vector center, Vector color, float shininess = 128, bool emits = false){
        _center = center;
        _diffuseColor = color;
        _ambientColor = new Vector(10f, 10f , 10f);
        _specularColor = new Vector(255f, 255f, 255f);
        _shininess = Clamp(shininess, 128, 1);
        _emits = emits;
        if(_emits){
            _emitColor = new Vector(255,255,255);
        } else{
            _emitColor = new Vector(0, 0, 0);
        }
    }

    public Vector A
    {
        get { return _ambientColor;}
        set { _ambientColor = value;}
    }

    public Vector D
    {
        get { return _diffuseColor;}
        set { _diffuseColor = value;}
    }

    public Vector S
    {
        get { return _specularColor;}
        set { _specularColor = value;}
    }

    public float Shiny
    {
        get { return _shininess;}
        set { _shininess = value; Clamp(_shininess, 128, 1);}
    }
    public Vector Center
    {
        get {return _center;}
        set {_center = value;}
    }

    public bool E
    {
        get{return _emits;}
        set{_emits = value;}
    }
    public Vector Emit
    {
        get {return _emitColor;}
        set {_emitColor = value; _emits = true;}
    }

    public float Clamp(float value, float max, float min){
        if(value > max){
            return max;
        }
        if (value < min){
            return min;
        }
        return value;
    }

    public abstract override bool Equals(object other);
    ///<summary> Determins if the shape object has been hit by the ray input.</summary>
    /// <param name="r">The ray.</param>
    /// <return> The intersection distiance from the ray origin. Return infinity if
    /// there is no intersection. </return>
    public abstract float Hit(Ray r);
    ///<summary> Calculates the normal of the object at the given point on the
    /// object.</summary>
    /// <param name="p">A point on the object</param>
    /// <return> A vector of the normal of the object at that point. </return>
    public abstract Vector Normal(Vector p);
}
using System;
using System.Drawing;
using System.Collections;
using System.Reflection.Metadata;

class Lightsaber : Shape
{
    private Vector _source;
    private Vector _direction;
    private float _length;
    private float _radius;
    public Lightsaber()
    {
        _source = new Vector(0, 0, 0);
        _direction = new Vector(0, 1, 0);
        _length = 2.0f;
        _radius = 0.1f;
    }
    public Lightsaber(Vector source, Vector direction, float length, float radius){
        _source = source;
        _direction = direction;
        Vector.Normalize(ref _direction);
        _length = length;
        _radius = radius;
    }

    public Vector Source
    {
        get {return _source;}
        set {_source = value;}
    }

    public Vector Tip
    {
        get {return _source + (_direction * _length);}
    }
    public Vector Center
    {
        get{return _source + (_direction * (_length / 2));}
    }

    public Vector Direction
    {
        get {return _direction;}
        set {_direction = value; Vector.Normalize(ref _direction);}
    }

    public float Length
    {
        get {return _length;}
        set {_length = value;}
    }

    public float Radius
    {
        get {return _radius;}
        set {_radius = value;}
    }

    public override Vector Normal(Vector p){
        float m = (~(_source - p) * ~(_source - p)) - (_radius * _radius);
        Vector o = _source + (float) Math.Sqrt(m) * _direction;
        return p - o;
    }

    public override float Hit(Ray r){
        
        float a = Vector.Dot(r.Direction, r.Direction) - (Vector.Dot(r.Direction, _direction) * Vector.Dot(r.Direction, _direction));
        float b = 2 * (Vector.Dot(r.Direction, r.Origin - _source) - (Vector.Dot(r.Direction, _direction) * Vector.Dot(r.Origin - _source, _direction)));
        float c = Vector.Dot(r.Origin - _source, r.Origin - _source) - (Vector.Dot(r.Origin - _source, _direction) * Vector.Dot(r.Origin - _source, _direction)) - (_radius * _radius);
        float denominator = 2 * a;
        float discriminant = b * b - 4 * a * c;
        if (discriminant == 0){
            return float.PositiveInfinity;
        }
        float t1 = (-b - (float)Math.Sqrt(discriminant)) / denominator;
        float t2 = (-b + (float)Math.Sqrt(discriminant)) / denominator;
        float t = (float) Math.Min(t1,t2);
        float m = Vector.Dot(r.Direction, _direction * t) + Vector.Dot(r.Origin - _source, _direction);
        if (m < 0 || m > _length){
            return float.PositiveInfinity;
        }
        return t;
    }

    public override bool Equals(object other)
    {
        if (other == null || GetType() != other.GetType())
        {
            return false;
        }

        Lightsaber otherShape = (Lightsaber) other;

        return _source.Equals(otherShape.Source)
             && _direction.Equals(otherShape.Direction)
             && _length == otherShape.Length;
    }

}
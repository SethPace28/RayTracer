using System;
using System.Drawing;
using System.Collections;
using System.Reflection.Metadata;

class Plane : Shape
{
    private Vector _normal;
    private Vector _point;
    public Plane()
    {
        _normal = new Vector(0, 1, 0);
        _point = new Vector(0, 0, 1);
    }
    public Plane(Vector normal, Vector point)
    {
        _normal = normal;
        _point = point;
    }

    public Vector P
    {
        get {return _point;}
        set {_point = value;}
    }

    public Vector N
    {
        get {return _normal;}
        set {_normal = value;}
    }

    public override float Hit(Ray r){
        float denominator = Vector.Dot(r.Direction, _normal);
        if (denominator == 0f){
            return float.PositiveInfinity;
        }
        Vector ao = _point - r.Origin;
        float numerator = Vector.Dot(ao, _normal);
        return numerator / denominator;
    }
    public override Vector Normal(Vector p)
    {
        return _normal;
    }

    public override bool Equals(object other)
    {
        if (other == null || GetType() != other.GetType())
        {
            return false;
        }

        Plane otherShape = (Plane)other;

        return _point.Equals(otherShape.P)
             && _normal.Equals(otherShape.N);
    }

}
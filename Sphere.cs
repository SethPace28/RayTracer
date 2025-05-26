using System;
using System.Drawing;
using System.Collections;
using System.Reflection.Metadata;

class Sphere : Shape
{
    private Vector _center;
    private float _radius;
    public Sphere()
    {
        _center = new Vector();
        _radius = 1.0f;
    }
    public Sphere(Vector center, float radius)
    {
        _center = center;
        _radius = radius;
    }

    public Vector C 
    {
        get{return _center;}
        set{_center = value;}
    }
    public float R 
    {
        get{return _radius;}
        set{_radius = value;}
    }

    public override float Hit(Ray r){
        Vector d = r.Direction;
        Vector o = r.Origin;
        Vector c = _center;
        float doc = Vector.Dot(d, o - c);
        float discriminant = (Vector.Dot(d, o - c) * Vector.Dot(d, o - c)) - Vector.Dot(d,d) * (Vector.Dot(o-c, o-c) - (_radius * _radius));
        if (discriminant < 0){
            return float.PositiveInfinity;
        }

        float t1 = (Vector.Dot(-d, o - c) + (float) Math.Sqrt(discriminant)) / Vector.Dot(d,d);
        float t2 = (Vector.Dot(-d, o - c) - (float) Math.Sqrt(discriminant)) / Vector.Dot(d,d);
        return (float) Math.Min(t1, t2);
    }
    public override Vector Normal(Vector p)
    {
        Vector direction = p - _center;
        Vector.Normalize(ref direction);
        return direction;
    }

    public override bool Equals(object other)
    {
        if (other == null || GetType() != other.GetType())
        {
            return false;
        }

        Sphere otherShape = (Sphere)other;

        return _center.Equals(otherShape.C)
                && _radius == otherShape.R;
    }

}
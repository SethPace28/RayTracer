/// <summary>
/// This class defines a Vector as a three-dimensional vector and provides various operations for it.
/// </summary>
/// <author>Seth Pace</author>
/// <date>09/19/2023</date>
/// <time-spent>7 hours</time-spent>
/// <collaborators>None</collaborators>

using System;

class Vector
{
    private float _x;
    private float _y;
    private float _z;

    /// <summary>
    /// Initializes a new instance of the Vector class with default values.
    /// </summary>
    public Vector()
    {
        _x = 0;
        _y = 0;
        _z = 0;
    }

    /// <summary>
    /// Initializes a new instance of the Vector class with the specified coordinates.
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <param name="z">The Z coordinate.</param>
    public Vector(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }

    /// <summary>
    /// Gets or sets the X coordinate of the vector.
    /// </summary>
    public float X
    {
        get { return _x;}
        set { _x = value;}
    }

    /// <summary>
    /// Gets or sets the Y coordinate of the vector.
    /// </summary>
    public float Y
    {
        get { return _y; }
        set { _y = value;}
    }

    /// <summary>
    /// s or sets the Z coordinate of the vector.
    /// </summary>
    public float Z
    {
        get { return _z;}
        set { _z = value;}
    }
    
    /// <summary>
    /// Multiplication operator overload to scale the vector by a scalar value.
    /// </summary>
    /// <param name="k">The scalar value.</param>
    /// <param name="v">The vector to be scaled.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector operator *(float k, Vector v){
        float x = k * v.X;
        float y = k * v.Y;
        float z = k * v.Z;
        return new Vector(x, y, z);
    }

    /// <summary>
    /// Multiplication operator overload to scale the vector by a scalar value.
    /// </summary>
    /// <param name="k">The scalar value.</param>
    /// <param name="v">The vector to be scaled.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector operator *(Vector v, float k){
        float x = k * v.X;
        float y = k * v.Y;
        float z = k * v.Z;
        return new Vector(x, y, z);
    }

    /// <summary>
    /// Operator overload to return postive vector.
    /// </summary>
    /// <param name="v">The vector to be scaled.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector operator +(Vector v){
        return v;
    }

    /// <summary>
    /// Operator overload return the negative vector.
    /// </summary>
    /// <param name="v">The vector to be negated.</param>
    /// <returns>The negative vector.</returns>
    public static Vector operator -(Vector v){
        float x = 0 - v.X;
        float y = 0 - v.Y;
        float z = 0 - v.Z;
        return new Vector(x, y, z);
    }

    /// <summary>
    /// Addition operator overload to add two vectors together.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector</param>
    /// <returns>The addtion of the two vectors.</returns>
    public static Vector operator +(Vector a, Vector b){
        float x = a.X + b.X;
        float y = a.Y + b.Y;
        float z = a.Z + b.Z;
        return new Vector(x, y, z);
    }

    /// <summary>
    /// Subtraction operator overload to add two vectors together.
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector to be subtracted from the first</param>
    /// <returns>The difference of the two vectors.</returns>
    public static Vector operator -(Vector a, Vector b){
        float x = a.X - b.X;
        float y = a.Y - b.Y;
        float z = a.Z - b.Z;
        return new Vector(x, y, z);
    }

    /// <summary>
    /// Operator overload to get the magnitude of a vector
    /// </summary>
    /// <param name="a">.The vector to be measured</param>
    /// <returns>The float representation of the magnitude of vector "a".</returns>
    public static float operator ~(Vector a){
        float x = a.X * a.X;
        float y = a.Y * a.Y;
        float z = a.Z * a.Z;
        float magnitude = (float) Math.Sqrt(x + y + z);
        return magnitude;
    }

    /// <summary>
    /// Method to compute the dot product of two vectors
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector</param>
    /// <returns>The dot product of the two vectors.</returns>
    public static float Dot(Vector a, Vector b){
        float x = a.X * b.X;
        float y = a.Y * b.Y;
        float z = a.Z * b.Z;
        return x + y + z;
    }

    /// <summary>
    /// Method to compute the cross product of two vectors
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector</param>
    /// <returns>The cross product of the two vectors.</returns>
    public static Vector Cross(Vector a, Vector b){
        float x = (a.Y * b.Z) - (a.Z * b.Y);
        float y = (a.Z * b.X) - (a.X * b.Z);
        float z = (a.X * b.Y) - (a.Y * b.X);
        Vector product= new Vector(x, y, z);
        return product;
    }

    /// <summary>
    /// Method to compute the angle between two vectors
    /// </summary>
    /// <param name="a">The first vector.</param>
    /// <param name="b">The second vector</param>
    /// <returns>The angle between the two vectors.</returns>
    public static float GetAngle(Vector a, Vector b){
        float angle = (float) Math.Acos(Dot(a,b) / (~(a) * ~(b)));
        return angle;
    }

    /// <summary>
    /// Method to normalize a vector
    /// </summary>
    /// <param name="a">The vector to be normalized.</param>
    /// <returns>The normalized vector</returns>
    public static void Normalize(ref Vector v){
        float length = ~v;
        if (length == 0){
            Console.WriteLine("Cannot divide by zero in Normalize!");
            return;
        }
        v.X = v.X / length;
        v.Y = v.Y / length;
        v.Z = v.Z / length;
    }

    /// <summary>
    /// Method to get the absolute value of the vector
    /// </summary>
    /// <param name="a">The vector to find absolute value for</param>
    /// <returns>The absolute value of the vector</returns>
    public static void Abs(ref Vector v){
        if (v.X < 0){
            v.X *= -1;
        }
        if (v.Y < 0){
            v.Y *= -1;
        }
        if (v.Z < 0){
            v.Z *= -1;
        }
    }

    /// <summary>
    /// Converts the vector to a string representation.
    /// </summary>
    /// <returns>A string representation of the vector.</returns>
    public override string ToString()
    {
        return _x + ", " + _y + ", " + _z;
    }

    /// <summary>
    /// Checks if this vector is equal to another vector.
    /// </summary>
    /// <param name="other">The other vector to compare with.</param>
    /// <returns>True if the vectors are equal, otherwise false.</returns>
    public override bool Equals(object other)
    {
        if (other == null || GetType() != other.GetType())
        {
            return false;
        }

        Vector otherVector = (Vector)other;

        return _x == otherVector.X &&
               _y == otherVector.Y &&
               _z == otherVector.Z;
    }

}
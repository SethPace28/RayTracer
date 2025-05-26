using System;
/// <summary>
/// Class <c>Ray</c> Creates a ray with an origin, direction, and an At class.
/// Author: Seth Pace
/// Date: 10/05/2023
/// </summary>
class Ray 
{
    // Private fields to store the origin and direction of the ray.
    private Vector _origin;
    private Vector _direction;

    /// <summary>
    /// Default constructor for the <c>Ray</c> class. Initializes the ray with an origin at (0,0,0) and a direction of (0,0,0).
    /// </summary>
    public Ray()
    {
        _origin = new Vector();
        _direction = new Vector();
    }

    /// <summary>
    /// Parameterized constructor for the <c>Ray</c> class. Initializes the ray with the given origin and direction.
    /// </summary>
    /// <param name="o">The origin of the ray.</param>
    /// <param name="d">The direction of the ray.</param>
    public Ray(Vector o, Vector d)
    {
        _origin = o;
        _direction = d;
        Vector.Normalize(ref _direction);
    }

    /// <summary>
    /// Gets or sets the direction of the ray.
    /// </summary>
    public Vector Direction
    {
        get { return _direction; }
        set { _direction = value; Vector.Normalize(ref _direction);}
    }

    /// <summary>
    /// Gets or sets the origin of the ray.
    /// </summary>
    public Vector Origin
    {
        get { return _origin; }
        set { _origin = value; }
    }

    /// <summary>
    /// Calculates the point on the ray at a given parameter value 't'.
    /// </summary>
    /// <param name="t">The parameter value at which to calculate the point on the ray.</param>
    /// <returns>The point on the ray at parameter value 't', or null if 't' is not on the ray.</returns>
    public Vector At(float t)
    {
        Vector answer =  _origin + _direction * t;
        return answer;
    }


}
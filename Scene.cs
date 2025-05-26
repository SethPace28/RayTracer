using System;
using System.Drawing;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Globalization;

class Scene{
    private List<Shape> _shapes;
    private int _numShapes;
    private Vector _light;
    
    public Scene(){
        _shapes = new List<Shape>();
        _numShapes = 0;
        _light = new Vector();
    }

    public List<Shape> Shapes
    {
        get { return _shapes;}
        set { _shapes = value;}
    } 

    public int NumShapes
    {
        get {return _numShapes;}
        set {_numShapes = value;}
    }

    public Vector Light
    {
        get {return _light;}
        set {_light = value;}
    }
    
    public void AddShape(ref Shape shape) {
        _shapes.Add(shape);
        _numShapes++;
    }
}
using System;
using ShapeDrawer;
using SplashKitSDK;

public class Drawing
{
    private List<Shape> _shapes;
    private Color _background;

    public Drawing()
    {
        _shapes = new List<Shape>();
        _background = Color.White;
    }
    public Drawing(Color background)
    {
        _shapes = new List<Shape>();
        _background = background;
    }

    public List<Shape> SelectedShapes
    {
        get
        {
            List<Shape> result = new List<Shape>();
            foreach (Shape s in _shapes)
            {
                if (s.Selected == true)
                {
                    result.Add(s);
                }
            }
            return result;
        }
    }

    public int ShapeCount
    {
        get
        {
            return _shapes.Count;
        }
    }

    public Color Background
    {
        get
        {
            return _background;
        }
        set
        {
            _background = value;
        }
    }

    public void Draw()
    {
        SplashKit.ClearScreen(_background);
        foreach (Shape s in _shapes)
        {
            s.Draw();
        }
    }

    public void SelectedShapesAt(Point2D pt)
    {
        foreach (Shape s in _shapes)
        {
            if (s.IsAt(pt) == true)
            {
                s.Selected = s.IsAt(pt);
            }
        }
    }

    public void AddShape(Shape s)
    {
        _shapes.Add(s);
    }

    public void RemoveShape(Shape s)
    {
        _shapes.Remove(s);
    }
    
}
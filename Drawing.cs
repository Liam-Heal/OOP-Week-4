using System;
using System.Security.Cryptography.X509Certificates;
using System.IO;
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

    public void Save(string filename)
    {
        StreamWriter writer = new StreamWriter(filename);

        try
        {
            writer.WriteColor(Background);
            writer.WriteLine(ShapeCount);
            foreach (Shape s in _shapes)
            {
                s.SaveTo(writer);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("The write error is " + ex.Message);
        }
        finally
        {
            writer.Close();
        }
    }
    public void Load(string filename)
    {
        StreamReader reader = new StreamReader(filename);
        try
        {
            Background = reader.ReadColor();
            int count = reader.ReadInteger();
            _shapes.Clear();
            for (int i = 0; i < count; i++)
            {
                Shape s;
                string? kind = reader.ReadLine();  

                if (string.IsNullOrWhiteSpace(kind))
                    throw new InvalidDataException("Unexpected end of file or missing shape kind.");

                switch (kind)
                {
                    case "Circle":
                        s = new MyCircle();
                        break;
                    case "Rectangle":
                        s = new MyRectangle();
                        break;
                    default:
                        throw new InvalidDataException($"Unknown shape kind: {kind}");
                }

                s.LoadFrom(reader);
                AddShape(s);
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("The read error is " + ex.Message);
            SplashKit.CloseAllWindows();
        }
        finally
        {
            reader.Close();
        }
    }

}
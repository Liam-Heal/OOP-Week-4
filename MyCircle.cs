using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{
    public class MyCircle : Shape
    {
        private int _radius;

        public MyCircle()
        {
            _color = Color.Blue;
            _radius = 90;
        }

        public MyCircle(Color color, float x, float y, int radius)
        {
            _color = color;
            _x = x;
            _y = y;
            _radius = radius;
        }

        public override void Draw()
        {
            if (Selected) DrawOutline();
            SplashKit.FillCircle(_color, _x, _y, _radius);
        }

        public override void DrawOutline()
        {
            SplashKit.FillCircle(Color.Black, _x, _y, _radius + 2);
        }

        public override bool IsAt(Point2D pt)
        {
            // bugfix: second arg should be pt.Y (not pt.X)
            return SplashKit.PointInCircle(pt.X, pt.Y, _x, _y, _radius);
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Circle");
            base.SaveTo(writer);
            writer.WriteLine(_radius);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            _radius = reader.ReadInteger();
        }

        public override void Scale(float factor)
        {
            _radius = (int)System.Math.Max(1, _radius * factor);
        }
    }
}

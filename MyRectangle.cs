using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{

    public class MyRectangle : Shape
    {
        private int _width;
        private int _height;

        public MyRectangle()
        {
            _color = Color.Yellow;
            _x = 0.0f;
            _y = 0.0f;

            _width = 100 + 50;
            _height = 100 + 50;

        }
        public MyRectangle(Color color, float x, float y, int width, int height)
        {
            _color = color;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }


        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public override void Draw()
        {
            if (Selected == true)
            {
                DrawOutline();
            }
            SplashKit.FillRectangle(_color, _x, _y, _width, _height);
        }

        public override void DrawOutline()
        {
            SplashKit.FillRectangle(Color.Black, _x - 2, _y - 2, _width + 4, _height + 4);
        }

        public override bool IsAt(Point2D pt)
        {
            bool check = SplashKit.PointInRectangle(pt.X, pt.Y, _x, _y, _width, _height);
            return check;
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Rectangle");
            base.SaveTo(writer);
            writer.WriteLine(Width);
            writer.WriteLine(Height);
        }
        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);            
            _width = reader.ReadInteger();
            _height = reader.ReadInteger();
        }
    }

}
using System;
using SplashKitSDK;

namespace ShapeDrawer
{

    public class Shape
    {

        private Color _color;
        private float _x;
        private float _y;
        private int _width;
        private int _height;

        private bool _selected;

        public Shape(int param)
        {

            _color = Color.Chocolate;

            _x = 0.0f;
            _y = 0.0f;
            _width = param;
            _height = param;

            _selected = false;

        }

        public Shape(int param, double x, double y)  //: this(param)
        {
            _x = (float)x;
            _y = (float)y;

            _width = param;
            _height = param;

            _selected = false;

            _color = Color.Chocolate;
        }

        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        public float Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
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

        public void Draw()
        {
            SplashKit.FillRectangle(_color, _x, _y, _width, _height);
        }

        public bool IsAt(Point2D pt)
        {
            float x1 = _x;
            float y1 = _y;

            float x2 = _x + Width;
            float y2 = _y + Height;

            if (pt.X >= x1 && pt.X <= x2 && pt.Y >= y1 && pt.Y <= y2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsWithinRadius(Point2D pt, double radius = 50.0)
        {
           
            double dx = pt.X - this.X; 
            double dy = pt.Y - this.Y;
            return (dx * dx + dy * dy) <= (radius * radius);
        }


    }


}
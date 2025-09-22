using System;
using System.IO;
using SplashKitSDK;

namespace ShapeDrawer
{

    public abstract class Shape
    {

        protected Color _color;
        protected float _x;
        protected float _y;
        //private int _width;
        //private int _height;

        protected bool _selected;

        public Shape()
        {
            _color = Color.Chocolate;
            _x = 0.0f;
            _y = 0.0f;
        }

        public Shape(int param)
        {

            _color = Color.Chocolate;

            _x = 0.0f;
            _y = 0.0f;
            //_width = param;
            //_height = param;

            _selected = false;

        }

        public Shape(int param, double x, double y)  //: this(param)
        {
            _x = (float)x;
            _y = (float)y;

            //_width = param;
            //_height = param;

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

        public abstract void DrawOutline();
        public abstract void Draw();
        //{
        //SplashKit.FillRectangle(_color, _x, _y, _width, _height);
        // }

        public abstract bool IsAt(Point2D pt);



        public virtual void SaveTo(StreamWriter writer)
        {
            writer.WriteColor(_color);
            writer.WriteLine(X);
            writer.WriteLine(Y);
        }

        public virtual void LoadFrom(StreamReader reader)
        {
        _color = reader.ReadColor();
        _x = reader.ReadSingle();
        _y = reader.ReadSingle();
        }

    }



}
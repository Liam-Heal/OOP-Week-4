using SplashKitSDK;

namespace ShapeDrawer
{

    public class MyCircle : Shape
    {

        private int _radius;

        public MyCircle()
        {
            _color = Color.Blue;
            _radius = 40 + 50;
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
            if (Selected == true)
            {
                DrawOutline();
            }
            SplashKit.FillCircle(_color, _x, _y, _radius);
        }

        public override void DrawOutline()
        {
            SplashKit.FillCircle(Color.Black, _x, _y, _radius+2);
        }

        public override bool IsAt(Point2D pt)
        {
            return SplashKit.PointInCircle(pt.X, pt.X, _x, _y, _radius);
        }
    }
}
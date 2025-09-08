using SplashKitSDK;

namespace ShapeDrawer
{
    public class MyLine : Shape
    {
        private float _endX;
        private float _endY;

        public float EndX
        {
            get => _endX;
            set => _endX = value;
        }

        public float EndY
        {
            get => _endY;
            set => _endY = value;
        }
        public MyLine()
        {
            _color = Color.Red;
            _x = 0.0f;
            _y = 0.0f;
            _endX = 120.0f;
            _endY = 0.0f;
        }

        public MyLine(Color color, float startX, float startY, float endX, float endY)
        {
            _color = color;
            _x = startX;
            _y = startY;
            _endX = endX;
            _endY = endY;
        }

        public override void Draw()
        {
            if (Selected) DrawOutline();
            SplashKit.DrawLine(_color, _x, _y, _endX, _endY);
        }

        public override void DrawOutline()
        {
            const int r = 4;
            SplashKit.FillCircle(Color.Black, _x, _y, r);
            SplashKit.FillCircle(Color.Black, _endX, _endY, r);
        }

        public override bool IsAt(Point2D pt)
        {
            var line = SplashKit.LineFrom(_x, _y, _endX, _endY);
            return SplashKit.PointOnLine(pt, line, 3);
        }
    }
}

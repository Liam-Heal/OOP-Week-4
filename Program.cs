using System;
using SplashKitSDK;
using ShapeDrawer;

namespace OOP_Week_4
{
    public class Program
    {

        private enum ShapeKind
        {
            Rectangle,
            Circle,
            Line, 
        }
        public static void Main()
        {
            Window shapesWindows = new Window("Hello COS20007", 500, 500);

            ShapeKind shapeToAdd = ShapeKind.Rectangle;


            Drawing myDrawing = new Drawing();

            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    shapeToAdd = ShapeKind.Rectangle;
                }
                if (SplashKit.KeyTyped(KeyCode.CKey))
                {
                    shapeToAdd = ShapeKind.Circle;
                }
                if (SplashKit.KeyTyped(KeyCode.LKey))
                {
                    shapeToAdd = ShapeKind.Line;
                }

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Shape newShape;

                    switch (shapeToAdd)
                    {
                        case ShapeKind.Rectangle:
                            newShape = new MyRectangle();
                            break;
                        case ShapeKind.Circle:
                            newShape = new MyCircle();
                            break;
                        case ShapeKind.Line:
                        {
                            Point2D p = SplashKit.MousePosition();
                            const int lineCount = 5;
                            const float spacing = 8f;   // gap between lines
                            const float length = 120f;  // how long the line goes to the right

                            for (int i = 0; i < lineCount; i++)
                            {
                                float y = (float)p.Y + i * spacing;
                                var line = new MyLine
                                {
                                    X = (float)p.X,
                                    Y = y,
                                    EndX = (float)p.X + length,
                                    EndY = y,
                                };
                                myDrawing.AddShape(line);
                            }

                            continue;
                        }
                        default:
                            newShape = new MyRectangle();
                            break;
                    }
                    Point2D currentPosition = SplashKit.MousePosition();
                    newShape.X = (float)currentPosition.X;
                    newShape.Y = (float)currentPosition.Y;

                    // Shape NewlyCreatedShape = new Shape(100, currentPosition.X, currentPosition.Y); //500 was way too big, changed to 500
                    myDrawing.AddShape(newShape);
                }

                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    myDrawing.SelectedShapesAt(SplashKit.MousePosition());
                }

                myDrawing.Draw();

                SplashKit.RefreshScreen();
            }
            while (!shapesWindows.CloseRequested);
        }
    }
}

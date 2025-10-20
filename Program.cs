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

                if (SplashKit.KeyTyped(KeyCode.RKey)) shapeToAdd = ShapeKind.Rectangle;
                if (SplashKit.KeyTyped(KeyCode.CKey)) shapeToAdd = ShapeKind.Circle;
                if (SplashKit.KeyTyped(KeyCode.LKey)) shapeToAdd = ShapeKind.Line;

                if (SplashKit.KeyTyped(KeyCode.BKey))
                {
                    BurstRandomShapes(myDrawing, shapesWindows);
                }

                if (SplashKit.KeyTyped(KeyCode.NKey))
                {
                    var p = SplashKit.MousePosition();
                    DrawFirstNameLIAM_Lines(myDrawing, (float)p.X, (float)p.Y, 16, 64, 6, Color.Blue);
                }

                if (SplashKit.KeyTyped(KeyCode.KKey))
                {
                    myDrawing.ScaleAll(0.8f);
                }
                
                if (SplashKit.KeyTyped(KeyCode.HKey))
                {
                    myDrawing.RandomizeAllColors();
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
                            const int lineCount = 15;
                            const float spacing = 8f;
                            const float length = 120f;

                            for (int i = 0; i < lineCount; i++)
                            {
                                float y = (float)p.Y + i * spacing;
                                var line = new MyLine
                                {
                                    X = (float)p.X,
                                    Y = y,
                                    EndX = (float)p.X + length,
                                    EndY = y,
                                    Color = Color.RGBColor(SplashKit.Rnd(), SplashKit.Rnd(), SplashKit.Rnd())
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
                    newShape.Color = Color.RGBColor(SplashKit.Rnd(), SplashKit.Rnd(), SplashKit.Rnd());
                    myDrawing.AddShape(newShape);
                }

                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    myDrawing.SelectedShapesAt(SplashKit.MousePosition());
                }
                if (SplashKit.KeyTyped(KeyCode.SKey))
                {
                    myDrawing.Save("105923500.txt");
                }
                if (SplashKit.KeyTyped(KeyCode.OKey))
                {
                    try
                    {
                        myDrawing.Load("105923500.txt");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Load failed: " + ex.Message);
                    }
                }

                myDrawing.Draw();
                SplashKit.RefreshScreen();
            }
            while (!shapesWindows.CloseRequested);
        }

        private static void BurstRandomShapes(Drawing drawing, Window w)
        {
            int count = SplashKit.Rnd(8, 18);
            for (int i = 0; i < count; i++)
            {
                int pick = SplashKit.Rnd(0, 3);
                Color clr = Color.RGBColor(SplashKit.Rnd(), SplashKit.Rnd(), SplashKit.Rnd());

                if (pick == 0)
                {
                    var r = new MyRectangle();
                    r.Color = clr;
                    r.X = SplashKit.Rnd(0, w.Width - 150);
                    r.Y = SplashKit.Rnd(0, w.Height - 150);
                    drawing.AddShape(r);
                }
                else if (pick == 1)
                {
                    var c = new MyCircle();
                    c.Color = clr;
                    c.X = SplashKit.Rnd(60, w.Width - 60);
                    c.Y = SplashKit.Rnd(60, w.Height - 60);
                    drawing.AddShape(c);
                }
                else
                {
                    float x = SplashKit.Rnd(20, w.Width - 140);
                    float y = SplashKit.Rnd(20, w.Height - 20);
                    var line = new MyLine
                    {
                        Color = clr,
                        X = x,
                        Y = y,
                        EndX = x + 120,
                        EndY = y
                    };
                    drawing.AddShape(line);
                }
            }
        }

        private static void DrawFirstNameLIAM_Lines(Drawing drawing, float startX, float startY, int stroke, int letterHeight, int thickness, Color color)
        {
            float x = startX;
            float y = startY;
            int gap = stroke;

            void Rect(float rx, float ry, float rw, float rh)
            {
                var r = new MyRectangle();
                r.Color = color;
                r.X = rx;
                r.Y = ry;
                r.Width = (int)rw;
                r.Height = (int)rh;
                drawing.AddShape(r);
            }


            float lWidth = stroke * 2.5f;
            Rect(x, y, thickness, letterHeight);
            Rect(x, y + letterHeight - thickness, lWidth, thickness);
            x += lWidth + gap;

            Rect(x + (stroke / 2f) - (thickness / 2f), y, thickness, letterHeight);
            x += stroke + gap;

            float aWidth = stroke * 3f;
            float aLeft = x;
            float aRight = x + aWidth - thickness;
            Rect(aLeft, y, thickness, letterHeight);
            Rect(aRight, y, thickness, letterHeight);
            Rect(aLeft, y, aWidth, thickness);
            Rect(aLeft + thickness * 0.5f, y + letterHeight * 0.5f - thickness * 0.5f, aWidth - thickness, thickness);
            x += aWidth + gap;

            float mWidth = stroke * 4f;
            float mLeft = x;
            float mRight = x + mWidth - thickness;
            Rect(mLeft, y, thickness, letterHeight);
            Rect(mRight, y, thickness, letterHeight);
            Rect(mLeft, y, mWidth, thickness);
            float mMidX = x + mWidth / 2f - thickness / 2f;
            float mPeakY = y + letterHeight * 0.45f;
            Rect(mMidX, y, thickness, mPeakY - y);
        }
    }
}

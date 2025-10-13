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
                    // (x, x, x, Stroke unit(Spacing for letters), letter height, thickness of stroke, x)
                    DrawFirstNameLIAM_Lines(myDrawing, (float)p.X, (float)p.Y, 16, 64, 6, Color.CornflowerBlue);
                }

                if (SplashKit.KeyTyped(KeyCode.KKey))
                {
                    myDrawing.ScaleAll(0.8f);
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
            // ThickSeg draws a “thick” line between (x1,y1) → (x2,y2) by stacking many 1-pixel lines side-by-side. It computes the unit direction along the line to extend the ends, the perpendicular to offset it, then draw the lines with passes to give the diagonals some thickness.
            void ThickSeg(float x1, float y1, float x2, float y2)
            {
                float dx = x2 - x1;
                float dy = y2 - y1;
                float len = (float)Math.Sqrt(dx * dx + dy * dy);
                if (len <= 0.0001f) return;

                float tx =  dx / len;
                float ty =  dy / len;
                float nx = -dy / len;
                float ny =  dx / len;

                float half = thickness / 2f;

                // extend the ends along the tangent to hide small V-gaps where strokes meet
                float cap = half + 0.5f;
                float sx1 = x1 - tx * cap;
                float sy1 = y1 - ty * cap;
                float sx2 = x2 + tx * cap;
                float sy2 = y2 + ty * cap;

                // sub-pixel step and a second phase to pack lines tightly
                float step = 0.25f;
                float halfStep = step * 0.5f;

                void Pass(float phase)
                {
                    for (float i = -half; i <= half; i += step)
                    {
                        float t = i + phase;
                        float offx = nx * t;
                        float offy = ny * t;
                        var seg = new MyLine(color, sx1 + offx, sy1 + offy, sx2 + offx, sy2 + offy);
                        drawing.AddShape(seg);
                    }
                }

                Pass(0f);
                Pass(halfStep);
            }

            // L
            float lWidth = stroke * 2.5f;
            ThickSeg(x, y, x, y + letterHeight);
            ThickSeg(x, y + letterHeight, x + lWidth, y + letterHeight);
            x += lWidth + gap;

            // I
            ThickSeg(x + (stroke / 2f), y, x + (stroke / 2f), y + letterHeight);
            x += stroke + gap;

            // A (two diagonals meeting at top + a crossbar halfway)
            float aWidth = stroke * 3f;
            float aLeft  = x;
            float aRight = x + aWidth;
            float aTopX  = x + aWidth / 2f;
            float aTopY  = y;
            float aBotY  = y + letterHeight;
            ThickSeg(aLeft,  aBotY, aTopX, aTopY);
            ThickSeg(aRight, aBotY, aTopX, aTopY);
            float crossY = y + letterHeight / 2f;
            ThickSeg(aLeft + stroke * 0.5f, crossY, aRight - stroke * 0.5f, crossY);
            x += aWidth + gap;

            // M (two verticals + two diagonals towards middle low-peak)
            float mWidth = stroke * 4f;
            float mLeft  = x;
            float mRight = x + mWidth;
            float mTopY  = y;
            float mBotY  = y + letterHeight;
            float mMidX  = x + mWidth / 2f;
            float mPeakY = y + letterHeight * 0.45f;
            ThickSeg(mLeft, mTopY, mLeft, mBotY);
            ThickSeg(mRight, mTopY, mRight, mBotY);
            ThickSeg(mLeft,  mTopY, mMidX, mPeakY);
            ThickSeg(mRight, mTopY, mMidX, mPeakY);
        }
    }
}

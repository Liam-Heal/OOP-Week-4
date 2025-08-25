using System;
using SplashKitSDK;
using ShapeDrawer;

namespace OOP_Week_4
{
    public class Program
    {
        public static void Main()
        {
            Window shapesWindows;

            shapesWindows = new Window("Hello COS20007", 700, 800);
            Shape myShape = new Shape(500);
            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    myShape.X = SplashKit.MouseX();
                    myShape.Y = SplashKit.MouseY();
                }
                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    Console.WriteLine(myShape.IsAt(SplashKit.MousePosition()));
                    myShape.Color = SplashKit.RandomColor();
                }

                myShape.Draw();

                SplashKit.RefreshScreen();
            } while (!shapesWindows.CloseRequested);
        }
    }
}


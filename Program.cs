using System;
using SplashKitSDK;
using ShapeDrawer;

namespace OOP_Week_4
{
    public class Program
    {
        public static void Main()
        {
            Window shapesWindows = new Window("Hello COS20007", 700, 800);
            Shape myShape = new Shape(500);

            const float CircleRadius = 50f;
            bool showCircle = false;     
            int  circleFramesLeft = 0;     

            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                // Move shape on left click
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    myShape.X = SplashKit.MouseX();
                    myShape.Y = SplashKit.MouseY();
                }

                // Test + show circle for a short time
                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    var mouse   = SplashKit.MousePosition();
                    bool inside = myShape.IsWithinRadius(mouse, CircleRadius);
                    Console.WriteLine(
                        $"Inside {CircleRadius}px circle around ({myShape.X}, {myShape.Y})? {inside}"
                    );

                    myShape.Color = SplashKit.RandomColor();
                    showCircle = true;
                    circleFramesLeft = 60;  
                }

                // Draw the rectangle
                myShape.Draw();

                // Circle frames left loop until it reaches 0, when reaches zero showcircle set to false and no longer persists through clear screns.
                if (showCircle && circleFramesLeft > 0)
                {
                    SplashKit.DrawCircle(Color.Black, myShape.X, myShape.Y, CircleRadius);
                    circleFramesLeft--;
                }
                else if (circleFramesLeft <= 0)
                {
                    showCircle = false;
                }

                SplashKit.RefreshScreen();
            }
            while (!shapesWindows.CloseRequested);
        }
    }
}

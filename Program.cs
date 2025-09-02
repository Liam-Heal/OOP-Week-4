using System;
using SplashKitSDK;
using ShapeDrawer;

namespace OOP_Week_4
{
    public class Program
    {
        public static void Main()
        {
            Window shapesWindows = new Window("Hello COS20007", 500, 500);
            

            Drawing myDrawing = new Drawing();

            do
            {
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {

                    Point2D currentPosition = SplashKit.MousePosition();

                    Shape NewlyCreatedShape = new Shape(100, currentPosition.X, currentPosition.Y); //500 was way too big, changed to 50
                    myDrawing.AddShape(NewlyCreatedShape); 
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

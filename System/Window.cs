using amberos.Graphics;

namespace amberos.System
{
    public class Window
    {
        public static int topSize = 30;
        public static void DrawTop(Process proc)
        {
            CustomDrawing.DrawTopRoundedRectangle(proc.WindowData.WinPos.X, proc.WindowData.WinPos.Y, proc.WindowData.WinPos.Width, topSize, topSize/2, Gui.colors.ColorDark);
            Gui.MainCanvas.DrawString(proc.Name, Gui.FontDefault, Gui.colors.ColorText, proc.WindowData.WinPos.X + 15, proc.WindowData.WinPos.Y + 5);
        }
    }
}
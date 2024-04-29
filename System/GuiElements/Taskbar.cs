using amberos.Graphics;
using amberos.System;

namespace amberos.System.GuiElements
{
    public class Taskbar : Process
    {
        public override void Run()
        {
            Gui.MainCanvas.DrawFilledRectangle(Gui.colors.ColorMain, 0, 0, 1920, 30);
            CustomDrawing.DrawFullRoundedRectangle(14, 3, 40, 20, 5, Gui.colors.ColorDark);
            Gui.MainCanvas.DrawString("AmberOS", Gui.FontDefault, Gui.colors.ColorText, 20, 5);
        }
    }
}
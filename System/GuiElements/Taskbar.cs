using System;
using System.Drawing;
using amberos.Graphics;
using amberos.Apps;
using Cosmos.System;
namespace amberos.System.GuiElements
{
    public class Taskbar : Process
    {
        public override void Run()
        {
            Gui.MainCanvas.DrawFilledRectangle(Gui.colors.ColorMain, 0, 0, 1920, 30);
            CustomDrawing.DrawFullRoundedRectangle(14, 3, 40, 20, 5, Gui.colors.ColorDark);
            Gui.MainCanvas.DrawString("AmberOS", Gui.FontDefault, Gui.colors.ColorText, 20, 5);
            Gui.MainCanvas.DrawString("Terminal", Gui.FontDefault,Color.White ,140, 5);
			if (Gui.MX >= 140 && Gui.MX <= 148 && Gui.MY >= 5 && Gui.MY <= 5 && !Gui.Clicked && MouseManager.MouseState == MouseState.Left)
            {
                Random rnd = new ();
                ProcessManager.Start(new Terminal { WindowData = new WindowData { WinPos = new Rectangle(rnd.Next(800, 1120), rnd.Next(0, 800), 800, 400) }, Name = "Terminal" });
                Gui.Clicked = true;
            }
        }
    }
}
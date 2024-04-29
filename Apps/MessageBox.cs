using System.Collections.Generic;
using amberos.Graphics;
using amberos.System;

namespace amberos.Apps
{
    public class MessageBox : Process
    {
        public List<string> Messages { get; set; }

        public MessageBox(List<string> messages)
        {
            Messages = messages;
        }

        public override void Run()
        {
            Window.DrawTop(this);
            int x = WindowData.WinPos.X;
            int y = WindowData.WinPos.Y;
            int sizeX = WindowData.WinPos.Width;
            int sizeY = WindowData.WinPos.Height;
            Gui.MainCanvas.DrawFilledRectangle(Gui.colors.ColorMain, x, y + Window.topSize, sizeX, sizeY - Window.topSize);
            int okButtonX = x + (sizeX - 100) / 2;
            int okButtonY = y + sizeY - 80;
            Gui.MainCanvas.DrawFilledRectangle(Gui.colors.ColorDark, okButtonX, okButtonY, 100, 50);
            int textWidth = "OK".Length * 10;
            int textHeight = 20;
            int textX = okButtonX + (100 - textWidth) / 2;
            int textY = okButtonY + (50 - textHeight) / 2;
            Gui.MainCanvas.DrawString("OK", Gui.FontDefault, Gui.colors.ColorText, textX, textY);

            int messageX = x + 20;
            int messageY = y + Window.topSize + 20;
            foreach (string message in Messages)
            {
                Gui.MainCanvas.DrawString(message, Gui.FontDefault, Gui.colors.ColorText, messageX, messageY);
                messageY += 20;
            }

            if (Gui.Clicked && Gui.MX >= okButtonX && Gui.MX <= okButtonX + 100 && Gui.MY >= okButtonY && Gui.MY <= okButtonY + 50)
            {
                Close();
            }
        }

        public void Close()
        {
            Gui.currentProcess = null;
            ProcessManager.Stop(this);
        }
    }
}
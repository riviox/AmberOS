using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using amberos.Graphics;
using amberos.System;

namespace amberos.Apps
{
    public class Notepad : Process
    {
        private List<string> textLines = new List<string>();
        private string currentLine = "";
        bool canWrite;

        public override void Run()
		{
			Window.DrawTop(this);
			int x = WindowData.WinPos.X;
			int y = WindowData.WinPos.Y;
			int sizeX = WindowData.WinPos.Width;
			int sizeY = WindowData.WinPos.Height;
            if (x < 0)
            {
                WindowData.WinPos.X = 0;
                x = 0;
            }

            if (x + sizeX > 1920)
            {
                WindowData.WinPos.X = 1920 - sizeX;
                x = 1920 - sizeX;
            }
            if(WindowData.WinPos.Y < 0)
            {
                WindowData.WinPos.Y = 0;
                y = 0;
            }
            if (y + sizeY > 1050)
            {
                y = 1020 - sizeY;
                WindowData.WinPos.Y = 1020 - sizeY;
            }
            int maxLines = (sizeY - Window.topSize - 20) / 18;
            int maxCharsPerLine = (sizeX - 20) / 8;
			Gui.MainCanvas.DrawFilledRectangle(Gui.colors.ColorDark, x, y + Window.topSize, sizeX, sizeY-Window.topSize);
            if ((Gui.MX >= x && Gui.MX <= x+ sizeX)&&(Gui.MY >= y && Gui.MY <= y+sizeY))
            {
                canWrite = true;
            }
            else
            {
                canWrite = false;
            }
            int consoleX = x + 10;
            int consoleY = y + Window.topSize + 10;
            int lineHeight = 16;

            int startIndex = Math.Max(0, textLines.Count - maxLines);
            for (int i = startIndex; i < textLines.Count; i++)
            {
                string line = textLines[i];

                if (line.Length > maxCharsPerLine)
                {
                    line = line.Substring(0, maxCharsPerLine);
                    textLines[i].Substring(0, maxCharsPerLine);
                }
                Gui.MainCanvas.DrawString(line, Gui.FontDefault, Gui.colors.ColorText, consoleX, consoleY);
                consoleY += lineHeight;
            }

            consoleY += lineHeight;
            string commandToShow = "Notepad - " + Kernel.Path;

            if (commandToShow.Length > maxCharsPerLine)
            {
                commandToShow = commandToShow.Substring(0, maxCharsPerLine);
            }
            Gui.MainCanvas.DrawString(commandToShow, Gui.FontDefault, Gui.colors.ColorText, consoleX, consoleY);
            if (Console.KeyAvailable && canWrite)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    textLines.Add(currentLine);
                    currentLine = "";
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (currentLine.Length > 0)
                        currentLine = currentLine.Remove(currentLine.Length - 1);
                }
                else
                {
                    ProcessKeyPress(keyInfo.KeyChar.ToString());
                }
            }
		}

        public void ProcessKeyPress(string key)
        {
            if (canWrite)
            {
                currentLine += key;
            }
        }
    }
}
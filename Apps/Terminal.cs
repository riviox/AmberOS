using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using amberos.Graphics;
using amberos.System;

namespace amberos.Apps
{
    public class Terminal : Process
    {
        private List<string> consoleOutput = new List<string>();
        private string currentCommand = "";
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
			Gui.MainCanvas.DrawFilledRectangle(Gui.colors.Terminal, x, y + Window.topSize, sizeX, sizeY-Window.topSize);
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

            int startIndex = Math.Max(0, consoleOutput.Count - maxLines);
            for (int i = startIndex; i < consoleOutput.Count; i++)
            {
                string line = consoleOutput[i];

                if (line.Length > maxCharsPerLine)
                {
                    line = line.Substring(0, maxCharsPerLine);
                    consoleOutput[i].Substring(0, maxCharsPerLine);
                }
                Gui.MainCanvas.DrawString(line, Gui.FontDefault, Gui.colors.ColorText, consoleX, consoleY);
                consoleY += lineHeight;
            }

            consoleY += lineHeight;
            string commandToShow = Kernel.Path + ">" + currentCommand;

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

                    consoleOutput.Add(Kernel.Path + ">" + currentCommand);
                    ProcessCommand();
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (currentCommand.Length > 0)
                        currentCommand = currentCommand.Remove(currentCommand.Length - 1);
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
                if (key == "\r") 
                {
                    consoleOutput.Add(Kernel.Path + ">" + currentCommand);
                    ProcessCommand();
                }
                else 
                {
                    currentCommand += key;
                }
            }
        }

        public void ProcessCommand()
        {
            if (currentCommand.StartsWith("echo "))
            {
                string textToEcho = currentCommand.Substring(5);
                consoleOutput.Add(textToEcho);
            }
            else if (currentCommand == "reboot")
            {
                Cosmos.System.Power.Reboot();
            }
            else if (currentCommand == "exit")
            {
                ProcessManager.Stop(this);
            }
            else if (currentCommand == "formatdsk")
            {
                if (Kernel.fs.Disks[0].Partitions.Count > 0)
                {
                    Kernel.fs.Disks[0].DeletePartition(0);
                }
                Kernel.fs.Disks[0].Clear();
                Kernel.fs.Disks[0].CreatePartition(Kernel.fs.Disks[0].Size/(1024*1024));
                Kernel.fs.Disks[0].FormatPartition(0, "FAT32", true);
                consoleOutput.Add("Success!");
                consoleOutput.Add("Please reboot AmberOS!");
            }
            else if (currentCommand == "info")
            {
                List<string> mes = new List<string>
                {
                    "Made by riviox",
                    " ",
                    "https://riviox.is-a.dev/"
                };
			    ProcessManager.Start(new MessageBox(mes) { WindowData = new WindowData { WinPos = new Rectangle(300, 100, 300, 250) },Name = "Info" });
            }
            else if (currentCommand == "showfps")
            {
                if (Gui.ShowFps)
                {
                    Gui.ShowFps = false;
                }
                else
                {
                    Gui.ShowFps = true;
                }
            }
            else if (currentCommand == "notepad")
            {
                Random rnd = new ();
				ProcessManager.Start(new Notepad { WindowData = new WindowData { WinPos = new Rectangle(rnd.Next(800, 1120), rnd.Next(0, 800), 800, 400) }, Name = "Notepad" });
            }
            else if (currentCommand == "ls")
            {
                var dirs = Directory.GetDirectories(Kernel.Path);
                var files = Directory.GetFiles(Kernel.Path);
                consoleOutput.Add("Dirs:");
                for (
                    int i = 0;
                    i < dirs.Length;
                    i++
                    )
                    {
                        consoleOutput.Add(dirs[i]);
                    }
                consoleOutput.Add("Files:");
                for (
                    int i = 0;
                    i < files.Length;
                    i++
                    )
                    {
                        consoleOutput.Add(files[i]);
                    }
            }
            else
            {
                consoleOutput.Add("Invalid command!");
            }
            currentCommand = "";
        }
    }
}
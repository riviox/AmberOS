using amberos.Apps;
using amberos.System;
using Cosmos.System;
using Cosmos.System.Graphics;
using System.Collections.Generic;
using System.Drawing;
using Cosmos.System.Graphics.Fonts;
using amberos.System.GuiElements;
using System;
using Cosmos.HAL;

namespace amberos.Graphics
{
	public static class Gui
	{
		public static int ScreenSizeX = 1920, ScreenSizeY = 1080;
		public static SVGAIICanvas MainCanvas;
		public static Bitmap Wallpaper, Cursor;
		public static Colors colors = new Colors();
		public static bool Clicked;
		public static Process currentProcess;
		public static int MX, MY;
		static int oldX, oldY;
		public static PCScreenFont FontDefault = PCScreenFont.Default;
		public static int _frames;
		public static int _fps = 200;
		public static int _deltaT = 0;
		public static bool ShowFps = false;
		public static void Update()
		{
			MX = (int)MouseManager.X;
			MY = (int)MouseManager.Y;
			MainCanvas.DrawImage(Wallpaper, 0, 0);
			Move();
			ProcessManager.Update();
			MainCanvas.DrawFilledRectangle(Color.Black, 0, 1020, 100, 60);
			MainCanvas.DrawString("Terminal", Gui.FontDefault,Color.White ,15, 1046);
			if (MX >= 0 && MX <= 100 && MY >= 1020 && MY <= 1080 && !Clicked && MouseManager.MouseState == MouseState.Left)
			{

				Random rnd = new Random();
				ProcessManager.Start(new Terminal { WindowData = new WindowData { WinPos = new Rectangle(rnd.Next(800, 1120), rnd.Next(0, 800), 800, 400) }, Name = "Terminal" });

				Clicked = true;
			}
			MainCanvas.DrawImageAlpha(Cursor, (int)MouseManager.X, (int)MouseManager.Y);
			if (MouseManager.MouseState == MouseState.Left)
				Clicked = true;
			else if(MouseManager.MouseState == MouseState.None && Clicked)
			{
				Clicked = false;
				currentProcess = null;
			}
			if (ShowFps)
			{
				if (_deltaT != RTC.Second)
				{
					_fps = _frames;
					_frames = 0;
					_deltaT = RTC.Second;
				}
				_frames++;
				Gui.MainCanvas.DrawString(_fps.ToString(), Gui.FontDefault, Color.White, ScreenSizeX/2, 0);
			}
			MainCanvas.Display();
		}
		public static void Move()
		{
			if(currentProcess != null)
			{
				currentProcess.WindowData.WinPos.X = (int)MouseManager.X - oldX;

				currentProcess.WindowData.WinPos.Y = (int)MouseManager.Y - oldY;
			}
			else if(MouseManager.MouseState == MouseState.Left && !Clicked)
			{
				foreach (var proc in ProcessManager.ProcessList)
				{
					if (!proc.WindowData.MoveAble)
						continue;
					if(MX > proc.WindowData.WinPos.X && MX < proc.WindowData.WinPos.X + proc.WindowData.WinPos.Width)
					{
						if(MY > proc.WindowData.WinPos.Y && MY < proc.WindowData.WinPos.Y + Window.topSize)
						{
							currentProcess = proc;
							oldX = MX - proc.WindowData.WinPos.X;
							oldY = MY - proc.WindowData.WinPos.Y;
						}
					}
				}
			}
		}
		public static void StartGUI()
		{
			List<string> mes = new List<string>
            {
				"Made by riviox",
				" ",
                "https://riviox.is-a.dev/"
            };
			MainCanvas = new SVGAIICanvas(new Mode((uint)ScreenSizeX, (uint)ScreenSizeY, ColorDepth.ColorDepth32));
			MouseManager.ScreenWidth = (uint)ScreenSizeX;
			MouseManager.ScreenHeight = (uint)ScreenSizeY;
			MouseManager.X = (uint)ScreenSizeX / 2;
			MouseManager.Y = (uint)ScreenSizeY / 2;
			ProcessManager.Start(new Taskbar());
		}
	}
}
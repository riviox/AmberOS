using amberos.Graphics;
using System.Drawing;

namespace amberos.System
{
	public static class CustomDrawing
	{
		public static void DrawFullRoundedRectangle(int x, int y, int width, int height, int radius, Color col)
		{
			Gui.MainCanvas.DrawFilledRectangle(col, x + radius, y, width - 2 * radius, height);
			Gui.MainCanvas.DrawFilledRectangle(col, x, y + radius, radius, height - 2 * radius);
			Gui.MainCanvas.DrawFilledRectangle(col, x + width - radius, y + radius, radius, height - 2 * radius);
			Gui.MainCanvas.DrawFilledCircle(col, x + radius, y + radius, radius);
			Gui.MainCanvas.DrawFilledCircle(col, x + width - radius - 1, y + radius, radius);
			Gui.MainCanvas.DrawFilledCircle(col, x + radius, y + height - radius - 1, radius);
			Gui.MainCanvas.DrawFilledCircle(col, x + width - radius - 1, y + height - radius - 1, radius);
		}
		public static void DrawTopRoundedRectangle(int x, int y, int width, int height, int radius, Color col)
		{
			Gui.MainCanvas.DrawFilledRectangle(col, x + radius, y, width - 2 * radius, height);
			Gui.MainCanvas.DrawFilledRectangle(col, x, y + radius, width, height - radius);
			Gui.MainCanvas.DrawFilledCircle(col, x + radius, y + radius, radius);
			Gui.MainCanvas.DrawFilledCircle(col, x + width - radius - 1, y + radius, radius);
		}
	}
}
using amberos.Graphics;
using Cosmos.System.Graphics;

namespace amberos.System
{
    public static class Boot
    {
        public static void onBoot()
        {
            Kernel.GuiRunning = true;
            Gui.Wallpaper = new Bitmap(Resources.Files.bgraw);
            Gui.Cursor = new Bitmap(Resources.Files.curraw);
            Gui.StartGUI();
        }
    }
}
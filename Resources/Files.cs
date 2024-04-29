using IL2CPU.API.Attribs;

namespace amberos.Resources
{
    public static class Files
    {
        [ManifestResourceStream(ResourceName = "amberos.Resources.Wallpapers.bg.bmp")] public static byte[] bgraw;
        [ManifestResourceStream(ResourceName = "amberos.Resources.Wallpapers.bg1.bmp")] public static byte[] bg1raw;
        [ManifestResourceStream(ResourceName = "amberos.Resources.Cursors.cursor.bmp")] public static byte[] curraw;
    }
}
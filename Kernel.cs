using System;
using amberos.Graphics;
using amberos.System;
using Cosmos.Core.Memory;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Sys = Cosmos.System;
namespace amberos
{
    public class Kernel: Sys.Kernel
    {
        public static string Version = "1.0.0";
        public static string Path = @"0:\";
        public static CosmosVFS fs;
        public static bool GuiRunning;
        int lastHeapCollect;
        protected override void BeforeRun()
        {
            Console.SetWindowSize(90, 30);
            Console.OutputEncoding = Sys.ExtendedASCII.CosmosEncodingProvider.Instance.GetEncoding(437);
            fs = new CosmosVFS();
            VFSManager.RegisterVFS(fs);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Booting AmberOS {Version}");
            Console.ForegroundColor = ConsoleColor.White;
            Message.Info("run 'gui' to open Gui");
        }

        protected override void Run()
        {
            if (!GuiRunning)
            {
                Console.Write($"{Path} >");
                var command = Console.ReadLine();
                Commands.Run(command);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Gui.Update();
            }
            if (lastHeapCollect >= 25)
            {
                Heap.Collect();
                lastHeapCollect = 0;
            }
            else
            {
                lastHeapCollect++;
            }
        }
    }
}

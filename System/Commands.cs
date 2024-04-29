using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace amberos.System
{
    public static class Commands
    {
        public static void Run(string command)
        {
            string[] words = command.Split(' ');
            if (words.Length > 0)
            {
                if (words[0] == "info")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"AmberOS {Kernel.Version}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Made by riviox with COSMOS.");
                    Console.WriteLine("https://riviox.is-a.dev");
                }
                else if (words[0] == "lsdsk")
                {
                    long freeSpace = Kernel.fs.GetAvailableFreeSpace(Kernel.Path)/1024;
                    Console.WriteLine($"Free space: {freeSpace}KB");
                }
                else if (words[0] == "formatdsk")
                {
                    if (Kernel.fs.Disks[0].Partitions.Count > 0)
                    {
                        Kernel.fs.Disks[0].DeletePartition(0);
                    }
                    Kernel.fs.Disks[0].Clear();
                    Kernel.fs.Disks[0].CreatePartition(Kernel.fs.Disks[0].Size/(1024*1024));
                    Kernel.fs.Disks[0].FormatPartition(0, "FAT32", true);
                    Message.Info("Success!");
                    Message.Warning("AmberOS will reboot in 2 seconds!");
                    Thread.Sleep(2000);
                    Cosmos.System.Power.Reboot();
                }
                else if (words[0] == "exit")
                {
                    Cosmos.System.Power.Shutdown();
                }
                else if (words[0] == "reboot")
                {
                    Cosmos.System.Power.Reboot();
                }
                else if (words[0] == "ls")
                {
                    var dirs = Directory.GetDirectories(Kernel.Path);
                    var files = Directory.GetFiles(Kernel.Path);
                    Console.ForegroundColor = ConsoleColor.Green;
                    for (
                        int i = 0;
                        i < dirs.Length;
                        i++
                        )
                        {
                            Console.WriteLine(dirs[i]);
                        }
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    for (
                        int i = 0;
                        i < files.Length;
                        i++
                        )
                        {
                            Console.WriteLine(files[i]);
                        }
                }
                else if (words[0] == "echo")
                {
                    if (words.Length > 1)
                    {
                        string message = string.Join(" ", words.Skip(1));
                        Console.WriteLine(message);
                    }
                    else
                    {
                        Message.Error("Invalid Syntax");
                    }
                }
                else if (words[0] == "cat")
				{
					if (words.Length > 1)
					{
						string path = words[1];
						if (!path.Contains(@"\"))
							path = Kernel.Path + path;
						if (path.EndsWith(' '))
						{
							path = path.Substring(0, path.Length - 1);
						}
						if (File.Exists(path))
						{
							string text = File.ReadAllText(path);
							Console.ForegroundColor = ConsoleColor.Gray;
							Console.WriteLine(text);
						}
						else
                        {
							Message.Error("File " + path + " not found!");
                        }
					}
					else
                    {
						Message.Error("Invalid Syntax!");
                    }
				}
                else if (words[0] == "rm")
				{
					if (words.Length > 1)
					{
						string path = words[1];
						if (!path.Contains(@"\"))
							path = Kernel.Path + path;
						if (path.EndsWith(' '))
						{
							path = path.Substring(0, path.Length - 1);
						}
						if (File.Exists(path))
						{
							File.Delete(path);
							Message.Info("Deleted " + path + "!");
						}
						else
                        {
							Message.Error("File " + path + " not found!");
                        }
					}
					else
                    {
						Message.Error("Invalid Syntax!");
                    }
				}
				else if (words[0] == "mkdir")
				{
					if (words.Length > 1)
					{
						string path = words[1];
						if (!path.Contains(@"\"))
							path = Kernel.Path + path;
						if (path.EndsWith(' '))
						{
							path = path.Substring(0, path.Length - 1);
						}
						Directory.CreateDirectory(path);
					}
					else
                    {   
						Message.Error("Invalid Syntax!");
                    }
				}
                else if (words[0] == "cd")
				{
					if (words.Length > 1)
					{
						if (words[1] == "..")
						{
							if (Kernel.Path != @"0:\")
							{
								string tempPath = Kernel.Path.Substring(0, Kernel.Path.Length - 1);
								Kernel.Path = tempPath.Substring(0, tempPath.LastIndexOf(@"\") + 1);
								return;
							}
							else
                            {
								return;
                            }
						}
						string path = words[1];
						if (!path.Contains(@"\"))
							path = Kernel.Path + path + @"\";
						if (path.EndsWith(' '))
						{
							path = path.Substring(0, path.Length - 1);
						}
						if (!path.EndsWith(@"\"))
							path += @"\";
						if (Directory.Exists(path))
							Kernel.Path = path;
						else
                        {
							Message.Error("Directory " + path + " not found!");
                        }
					}
					else
                    {
						Kernel.Path = @"0:\";
                    }
				}
                else if (words[0] == "gui")
                {
                    Boot.onBoot();
                }
            }
            else
            {
                Message.Error("Invalid command.");
            }
        }
    }
}
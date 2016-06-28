using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace FileEventListener
{
    class Program
    {
        static void Main()
        {
            Console.Clear();

            string path = GetFilePath();

            FileSystemWatcher watcher = new FileSystemWatcher(path);
            RegisterHandlers(watcher);

            Console.WriteLine("Press 'q' to Quit");

            while (Console.ReadLine() != "q") ;
        }

        private static string GetFilePath()
        {
            string path = "";

            while (true)
            {
                Console.Write("Enter a file path: ");
                path = Console.ReadLine();

                try
                {
                    if (path != null && !Directory.Exists(path))
                        throw new DirectoryNotFoundException("Directory supplied could not be found");

                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Please enter a valid directory");
                }
            }

            return path;
        }

        private static void RegisterHandlers(FileSystemWatcher watcher)
        {
            watcher.Changed += OnChanged;
            watcher.Deleted += OnChanged;
            watcher.Created += OnChanged;
            watcher.Renamed += OnRenamed;
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
        }

        private static void OnChanged(object o, FileSystemEventArgs e)
        {
            Console.WriteLine("File: " + e.Name + " " + e.ChangeType);
        }

        private static void OnRenamed(object o, FileSystemEventArgs e)
        {
            Console.WriteLine("File {0} renamed to {1}", e.FullPath, e.FullPath);
        }
    }
}

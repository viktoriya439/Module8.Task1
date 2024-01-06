using System;
using System.IO;

namespace ConsoleApp14
{
    class Program
    {
        //    static void Main()
        //    {
        //        Console.WriteLine("Пожалуйста, введите путь до папки:");
        //        string path = Console.ReadLine(); // читаем путь до папки из консоли
        //        TimeSpan timeSpan = TimeSpan.FromMinutes(30); // временной интервал в 30 минут

        //        if (Directory.Exists(path))
        //        {
        //            try
        //            {
        //                DeleteOldFilesAndFolders(path, timeSpan);
        //                Console.WriteLine("Удаление файлов и папок, которые не использовались более 30 минут, завершено.");
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"Произошла ошибка при удалении файлов и папок: {ex.Message}");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Папка по заданному пути не существует.");
        //        }
        //    }

        //    static void DeleteOldFilesAndFolders(string path, TimeSpan timeSpan)
        //    {
        //        var directory = new DirectoryInfo(path);
        //        DateTime lastWriteTime = directory.LastWriteTime; // сохраняем время последнего изменения папки

        //        foreach (var file in directory.GetFiles())
        //        {
        //            if (DateTime.Now - file.LastAccessTime > timeSpan)
        //            {
        //                file.Delete();
        //            }
        //        }

        //        foreach (var dir in directory.GetDirectories())
        //        {
        //            DeleteOldFilesAndFolders(dir.FullName, timeSpan); // рекурсивное удаление
        //        }

        //        // Если папка пуста после удаления старых файлов и подпапок, удаляем ее
        //        if (directory.GetFiles().Length == 0 && directory.GetDirectories().Length == 0)
        //        {
        //            if (DateTime.Now - lastWriteTime > timeSpan) // используем сохраненное время последнего изменения
        //            {
        //                directory.Delete();
        //            }
        //        }
        //    }
        //}
        static void Main()
        {
            Console.WriteLine("Пожалуйста, введите путь до папки:");
            string path = Console.ReadLine(); // читаем путь до папки из консоли
            TimeSpan timeSpan = TimeSpan.FromMinutes(30); // временной интервал в 30 минут

            if (Directory.Exists(path))
            {
                try
                {
                    DeleteOldFilesAndFolders(path, timeSpan);
                    Console.WriteLine("Удаление файлов и папок, которые не использовались более 30 минут, завершено.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка при удалении файлов и папок: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Папка по заданному пути не существует.");
            }
        }

        static void DeleteOldFilesAndFolders(string path, TimeSpan timeSpan)
        {
            var directory = new DirectoryInfo(path);

            foreach (var file in directory.GetFiles())
            {
                if (DateTime.Now - file.LastAccessTime > timeSpan)
                {
                    file.Delete();
                }
            }

            foreach (var dir in directory.GetDirectories())
            {
                DateTime lastWriteTime = dir.LastWriteTime; // сохраняем время последнего изменения подпапки
                DeleteOldFilesAndFolders(dir.FullName, timeSpan); // рекурсивное удаление

                // Если подпапка пуста после удаления старых файлов и подпапок, удаляем ее
                if (dir.GetFiles().Length == 0 && dir.GetDirectories().Length == 0)
                {
                    if (DateTime.Now - lastWriteTime > timeSpan) // используем сохраненное время последнего изменения
                    {
                        dir.Delete();
                    }
                }
            }
        }
    }
}
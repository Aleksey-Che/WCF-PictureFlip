using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace wcfPictureFlip
{
    public class ServicePictureFlip : IServicePictureFlip
    {
        static void Main(string[] args)
        {
            ServicePictureFlip prog = new ServicePictureFlip();
            prog.Execution();
        }
        
        public void Execution() {
            Console.WriteLine("Введите путь к папке с изображениями: ");
            string dirName = Console.ReadLine();
            var listFiles = getPath(dirName);
            Console.WriteLine("Введите угол поворота изображения (90, 180 или 270 градусов): ");
            string angleStr = Console.ReadLine();
            int angle = Convert.ToInt32(angleStr);
            while (true)
            {
                if (angle == 90 || angle == 180 || angle == 270)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введенное значение неверно! Введите корректное значение: ");
                    angleStr = Console.ReadLine();
                    angle = Convert.ToInt32(angleStr);
                }
            }
            long result = doFlip(listFiles, angle, dirName);
            Console.WriteLine("Выполнение завершено успешно.");
            Console.WriteLine($"Время выполнения операции: {result} мс");
            Console.ReadKey();
        }


        public object getPath(string dirName)
        {
            object files = null;
            try
            {
                files = Directory.GetFiles(dirName, "*.*").Where(str => str.EndsWith(".jpg") || str.EndsWith(".png") || str.EndsWith(".bmp"));
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка! Указан неверный путь!");
                Console.ReadKey();
                Process.GetCurrentProcess().Kill();
            }
            return files;
        }

        public long doFlip(object listFiles, int angle, string dirName)
        {
            var sw = new Stopwatch();
            sw.Start();
            Parallel.ForEach((IEnumerable<string>)listFiles, (currentFile) =>
            {
                string filename = Path.GetFileName(currentFile);
                using (var bitmap = new Bitmap(currentFile))
                {
                    if (angle == 90)
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    }
                    else if (angle == 180)
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);

                    }
                    else if (angle == 270)
                    {
                        bitmap.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    }
                    bitmap.Save(Path.Combine(dirName, filename));
                }
            });
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}

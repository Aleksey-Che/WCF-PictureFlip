﻿using System;
using System.ServiceModel;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(wcfPictureFlip.ServicePictureFlip)))
            {
                host.Open();
                Console.WriteLine("Хост запущен");
                Console.ReadKey();
            }
        }
    }
}

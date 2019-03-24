﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Cars
{
    class Program
    { 
        static void Main(string[] args)
        {
            var cars = ProcessFile("fuel2.csv");
            foreach (var car in cars)
            {
                Console.WriteLine(car.Name);
            }
            Console.ReadLine();
        }

        private static List<Car> ProcessFile(string path)
        {
            return
            File.ReadAllLines(path)
                .Skip(1)
                .Where(line => line.Length > 1)
                .Select(Car.ParseFromCsv)
                .ToList();
        }
    }
}

using System;
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
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            //extension method syntax
            //var query = cars.OrderByDescending(c => c.Combined)
            //                .ThenBy(c => c.Name);

            //using query syntax
            var query =
                from car in cars
                where car.Manufacturer == "BMW" && car.Year == 2016
                orderby car.Combined descending, car.Name ascending
                // creating an anonymous object
                select new
                {
                    car.Manufacturer,
                    car.Name,
                    car.Combined
                };

            //using select many
            var result = cars.SelectMany(c => c.Name)
                             .OrderBy(c => c);

            foreach (var character in result)
            {               
                Console.WriteLine(character);
            }


            //extension method syntax with lambda expressions
            //var query2 =
            //    cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
            //        .OrderByDescending(c => c.Combined)
            //        .ThenBy(c => c.Name);

            //foreach (var car in query.Take(10))
            //{
            //    Console.WriteLine($"{car.Manufacturer} : {car.Name} : {car.Combined}");
            //}
            Console.ReadLine();
        }

        private static List<Car> ProcessFile(string path)
        {
            //using query syntax
            //var query =
            //    from line in File.ReadAllLines(path).Skip(1)
            //    where line.Length > 1
            //    select Car.ParseFromCsv(line);

            //return query.ToList();

            //using extension method syntax
            var query =
                File.ReadAllLines(path)
                .Skip(1)
                .Where(l => l.Length > 1)
                .ToCar();

            return query.ToList();

        }

        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                .Where(l => l.Length > 1)
                .Select(l =>
                {
                    var columns = l.Split(',');
                    return new Manufacturer
                    {
                        Name = columns[0],
                        Headquarters = columns[1],
                        Year = int.Parse(columns[2])
                    };
                });
            return query.ToList();
        }
    }

    public static class CarExtensions
    {
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {
                var columns = line.Split(',');

                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])

                };
            }
        }
    }
}

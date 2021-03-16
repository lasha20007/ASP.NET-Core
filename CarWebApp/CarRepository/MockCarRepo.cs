using CarWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWebApp.CarRepository
{
    public class MockCarRepo : ICarRepository
    {
        private List<Car> cars = new List<Car>();
        private int _nextId = 1;

        public Car Add(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException("item");
            }
            car.Id = _nextId++;
            cars.Add(car);
            return car;
        }

        public bool Edit(Car car)
        {
            if (car == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = cars.FindIndex(p => p.Id == car.Id);
            if (index == -1)
            {
                return false;
            }
            cars.RemoveAt(index);
            cars.Add(car);
            return true;
        }

        public Car Get(int Id)
        {
            return cars.Find(p => p.Id == Id);
        }

        public IEnumerable<Car> GetAll()
        {
            return cars;
        }

        public void Remove(int Id)
        {
            cars.RemoveAll(p => p.Id == Id);
        }
    }
}

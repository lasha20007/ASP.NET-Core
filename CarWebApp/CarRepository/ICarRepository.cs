using CarWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWebApp.CarRepository
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll();

        Car Get(int Id);

        Car Add(Car car);

        void Remove(int Id);

        bool Edit(Car car);
    }
}

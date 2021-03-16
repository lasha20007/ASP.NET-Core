using CarWebApp.CarRepository;
using CarWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarWebApp.Controllers
{
    [ApiController]
    public class VehicleController : ControllerBase
    {
        static readonly ICarRepository repository = new MockCarRepo();

        [HttpGet]
        [Route("api/[controller]")]
        public IEnumerable<Car> GetAllCars()
        {
            return repository.GetAll();
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public Car GetCarById(int id)
        {
            Car item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        [HttpPost]
        [Route("api/[controller]")]
        public Car AddCar(Car car)
        {
            car = repository.Add(car);
            return car;
        }

        [HttpPut]
        [Route("api/[controller]/{id}")]
        public void PutCar(int Id, Car car)
        {
            car.Id = Id;
            if (!repository.Edit(car));
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public void DeleteCar(int Id)
        {
            Car item = repository.Get(Id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            repository.Remove(Id);
        }
    }
}

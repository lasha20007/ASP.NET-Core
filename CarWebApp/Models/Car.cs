using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarWebApp.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string CarModel { get; set; }

        public int ReleaseDate { get; set; }

        public string Description { get; set; }

        public string Photoname { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }

        public int Price { get; set; }

        public string Currency { get; set; }

        public int PriceGel { get; set; }

        public string CarSpecs { get; set; }

        public Car()
        {
                
        }
    }
}

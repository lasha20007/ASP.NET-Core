using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CarWebApp.Models;

namespace CarWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<CarWebApp.Models.Car> Car { get; set; }
        public DbSet<CarWebApp.Models.CarSpecs> CarSpecs { get; set; }
    }
}

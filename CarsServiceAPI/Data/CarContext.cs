﻿using CarsServiceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsServiceAPI.Data
{
    public class CarContext : DbContext
    {
        public CarContext(DbContextOptions<CarContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; } = null!;
    }
}

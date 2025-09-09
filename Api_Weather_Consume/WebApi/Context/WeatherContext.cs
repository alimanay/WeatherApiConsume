using ApiProject_Weather.Entitys;
using Microsoft.EntityFrameworkCore;

namespace ApiProject_Weather.Context
{
    public class WeatherContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
    "Server=DESKTOP-410QTSD\\SQLEXPRESS;Initial Catalog=DbWeatherApi;Integrated Security=True;TrustServerCertificate=True"
);
        }
         public DbSet<City> Cities { get; set; }
    }
    }


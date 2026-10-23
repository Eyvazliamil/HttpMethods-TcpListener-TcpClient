using Shared.SharedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public class Repository : IRepository
    {
        private readonly List<Car> _Cars = new();
        public Command command = new Command();
        public bool Add(Car car)
        {
            if (car != null)
            {
                _Cars.Add(car);
                return true;
            }
            else
                return false; 
        }

        public bool Delete(int id)
        {
            var car = _Cars.FirstOrDefault(c => c.Id == id);
            return car != null && _Cars.Remove(car); 
        }

        public List<Car>? GetAll() { return _Cars.ToList(); }

        public Car? GetByld(int id)
        {
            var car = _Cars.FirstOrDefault(c => c.Id == id);
            return car;
        }

        public bool Update(Car car)
        {
            var old = _Cars.FirstOrDefault(c => c.Id == car.Id);
            if (old == null) return false;
            old.Make = car.Make;
            old.Model = car.Model;
            old.Year = car.Year;
            old.VIN = car.VIN;
            old.Color = car.Color;
            return true;
        }
    }
}

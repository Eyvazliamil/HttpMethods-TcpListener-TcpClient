using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Shared.SharedClasses;

namespace Server.Interfaces
{ 
    public interface IRepository
    {
        Car? GetByld(int id);
        List<Car>? GetAll();
        bool Add(Car car);
        bool Update(Car car);
        bool Delete(int id);
    }
}

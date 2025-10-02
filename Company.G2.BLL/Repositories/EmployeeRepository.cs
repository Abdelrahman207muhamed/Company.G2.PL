using Company.G2.BLL.Interfaces;
using Company.G2.DAL.Data.Contexts;
using Company.G2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G2.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> ,IEmployeeRepository
    {
        public EmployeeRepository(CompanyDbContext context) :base(context) //ASK CLR Create Object From  CompanyDbContext
        {
            
        }
    }
}

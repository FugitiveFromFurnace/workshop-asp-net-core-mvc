using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context) => this._context = context;

        public List<Department> FindAll() => _context.Department.OrderBy(x => x.Name).ToList();

        public async Task<List<Department>> FindAllAsync() => await _context.Department.OrderBy(x => x.Name).ToListAsync();
    }
}

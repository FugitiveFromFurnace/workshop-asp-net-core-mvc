using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context) => this._context = context;

        public List<Seller> FindAll() => _context.Seller.OrderBy(x => x.Name).ToList();

        public void Insert(Seller obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id) => _context.Seller.FirstOrDefault(x => x.Id == id);

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);

            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }
    }
}

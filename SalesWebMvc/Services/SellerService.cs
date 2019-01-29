using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context) => this._context = context;

        public List<Seller> FindAll() => _context.Seller.OrderBy(x => x.Name).ToList();

        public async Task<List<Seller>> FindAllAsync() => await _context.Seller.OrderBy(x => x.Name).ToListAsync();

        public async void InsertAsync(Seller obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public Seller FindById(int id) => _context.Seller.Include(x => x.Department).FirstOrDefault(x => x.Id == id);

        public async Task<Seller> FindByIdAsync(int id) => await _context.Seller.Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id);

        public async void RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }

        public async void UpdateAsync(Seller seller)
        {
            if (!await _context.Seller.AnyAsync(x => x.Id == seller.Id))
                throw new NotFoundException("Id not found");

            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}

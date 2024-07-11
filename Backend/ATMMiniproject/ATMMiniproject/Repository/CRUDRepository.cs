using ATM_MiniProject.Context;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ATMMiniproject.Repository
{
    public abstract class CRUDRepository<K,T> where T : class
    {
        protected ATMContext _context;

        public CRUDRepository(ATMContext context) 
        {
            _context= context;
        }
        public async Task<T> Add(T item)
        {
            if (item == null) return null;

            try
            {

                _context.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"Database error: {dbEx.Message}");

            }
            return null;

        }
        public async Task<bool> Delete(K key)
        {
            try
            {
                var item = await GetbyId(key);
                if (item == null) return false;

                _context.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"Database error: {dbEx.Message}");
            }

            return false;

        }
        public abstract Task<T> GetbyId(K key);
        public async Task<T> Update(T item)
        {
            try
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (DbException dbEx)
            {
                Console.WriteLine($"Database error: {dbEx.Message}");
            }
            return null;
        }
        public abstract Task<IEnumerable<T>> GetAll();

    }
}

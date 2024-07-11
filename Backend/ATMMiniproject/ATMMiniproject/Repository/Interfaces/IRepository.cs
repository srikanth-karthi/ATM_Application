namespace ATMMiniproject.Repository.Interfaces
{
    public interface IRepository<K,T> where T: class
    {
        public Task<T> Add(T item);
        public Task<bool> Delete(K key);
        public abstract Task<T> GetbyId(K key);
        public Task<T> Update(T item);
        public Task<IEnumerable<T>> GetAll();
    }
}

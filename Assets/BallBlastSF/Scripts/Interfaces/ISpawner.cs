public interface ISpawner<T>
{
    void StartSpawning();
    void StopSpawning();
    void Add(T entity);
    void Remove(T entity);
    int GetCount();
}
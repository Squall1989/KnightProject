public interface IPoolable<T>
{
    T GetFromPool();

    void ReturnToPool(T poolObject);
}

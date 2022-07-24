
namespace KnightProject
{
    public interface Initiable<T>
    {
        void Init(T initType);
    }

    public interface Initiable<T, U>
    {
        void Init(T initT, U initU);
    }
}
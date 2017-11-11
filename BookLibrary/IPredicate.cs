namespace BookStore
{
    public interface IPredicate<T>
    {
        bool IsTrue(T item);
    }
}

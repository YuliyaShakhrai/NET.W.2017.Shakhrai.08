using BookStore;
using System.Collections.Generic;

namespace BookStore
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();

        void AddBook(Book book);

        void RemoveBook(Book book);

        void RemoveAllBooks();

        Book FindBookByTag(IPredicate<Book> predicate);

        void SortBooksByTag(IComparer<Book> comparer);

        void LoadBooksFromStorage(IBookStorage storage);

        void SaveBooksToStorage(IBookStorage storage);
    }
}

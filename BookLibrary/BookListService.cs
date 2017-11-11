using System;
using System.Collections.Generic;
using BookStore;
using System.Linq;

namespace BookStore
{
    public class BookListService : IBookService
    {
        #region Private fields

        private List<Book> books = new List<Book>();

        #endregion
        #region Constructor
        public BookListService(ICollection<Book> bookList)
        {
            books = new List<Book>(bookList.Where(book => book != null));
        }
        #endregion

        #region Book services

        public void AddBook(Book book)
        {
            VerifyNullBook(book);
            if (books.Contains(book))
            {
                throw new ArgumentException($"{nameof(book)} is already at list.");
            }

            books.Add(book);
        }

        public Book FindBookByTag(IPredicate<Book> predicate)
        {
            foreach (Book book in books)
            {
                if (predicate.IsTrue(book))
                {
                    return book;
                }
            }

            return null;
        }

        public IEnumerable<Book> GetBooks()
        {
            return books;
        }

        public void LoadBooksFromStorage(IBookStorage storage)
        {
            foreach (Book book in storage.Load())
            {
                if (!books.Contains(book))
                {
                    books.Add(book);
                }
            }
        }

        public void RemoveAllBooks()
        {
            books.Clear();
        }

        public void RemoveBook(Book book)
        {
            VerifyNullBook(book);

            if (!books.Contains(book))
            {
                throw new ArgumentException($"{nameof(book)} not found at the list.");
            }

            books.Remove(book);
        }

        public void SaveBooksToStorage(IBookStorage storage)
        {
            storage.Save(GetBooks());
        }

        public void SortBooksByTag(IComparer<Book> comparer)
        {
            books.Sort(comparer);
        }

        #endregion

        #region Private methods

        private void VerifyNullBook(Book book)
        {
            if (ReferenceEquals(book, null))
            {
                throw new ArgumentNullException($"{nameof(book)} is null.");
            }
        }

        #endregion
    }
}

using BookStore;
using System.Collections.Generic;

namespace BookStore
{
    public interface IBookStorage
    {
        void Save(IEnumerable<Book> books);

        IEnumerable<Book> Load();
    }
}

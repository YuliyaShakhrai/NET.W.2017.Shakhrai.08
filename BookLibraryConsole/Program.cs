using System;
using System.Collections.Generic;
using BookStore;
using System.Configuration;

namespace BookStoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create some books
            var book1 = new Book("Dune", "Frank Herbert", "978-8-37-150211-8", "Chilton Books", 1965, 412, 12.67);
            var book2 = new Book("Witcher. The Last Wish", "Andrzej Sapkowski", "978-7-15-257954-1", "Gollancz", 2007, 514, 15.44);
            var book3 = new Book("Roadside Picnic", "Arkady and Boris Strugatsky", "978-0-02-615170-2", "Macmillan", 1972, 722, 11.78);

            //Add books to List
            List<Book> books = new List<Book> { book1, book2 };

            Console.WriteLine("\nNew list of books.\n");
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }

            //Creating service working with book list
            var service = new BookListService(books);
            Console.WriteLine("\nNew service was created.\n");

            //Add third book to List by service
            service.AddBook(book3);

            Console.WriteLine("\nAdded new book by service.\n");
            foreach (var book in service.GetBooks())
            {
                Console.WriteLine(book);
            }

            //Create a copy of existing book
            var book4 = new Book("Roadside Picnic", "Arkady and Boris Strugatsky", "978-0-02-615170-2", "Macmillan", 1972, 722, 11.78);

            //Trying to add existing book to list
            try
            {
                Console.WriteLine("\nTrying to add existing book\n");
                service.AddBook(book4);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Trying to add null book to list
            Book book5 = null;
            try
            {
                Console.WriteLine("\nTrying to add null book\n");
                service.AddBook(book5);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Trying to remove existing book from list
            service.RemoveBook(book1);
            Console.WriteLine("\nRemove book by service.\n");
            foreach (var book in service.GetBooks())
            {
                Console.WriteLine(book);
            }

            //Trying to remove existing book from list twice
            try
            {
                Console.WriteLine("\nTrying remove existing book from list twice\n");
                service.RemoveBook(book1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //Added book1 again by service
            service.AddBook(book1);

            //Existing list of books
            Console.WriteLine("\nExisting list of books.\n");
            foreach (var book in service.GetBooks())
            {
                Console.WriteLine(book);
            }

            Console.WriteLine("\nFind a book with author Andrzej Sapkowski:");
            Console.WriteLine(service.FindBookByTag(new AuthorPredicate()));

            Console.WriteLine("\nFind a book with price less than 15:");
            Console.WriteLine(service.FindBookByTag(new PricePredicate()));

            service.SortBooksByTag(new PublishYearComparer());
            Console.WriteLine("\nBooks ordered by date sort:");
            foreach (var book in service.GetBooks())
            {
                Console.WriteLine(book);
            }

            //filepath is at app.config
            string storageFilepath = ConfigurationSettings.AppSettings["filepath"];
            var storage = new BookListStorage(storageFilepath);
            storage.Save(service.GetBooks());

            //Creating other service to load books from binary file
            List<Book> books2 = new List<Book>();
            var service2 = new BookListService(books2);
            service2.LoadBooksFromStorage(storage);
            Console.WriteLine("\nBooks loaded by other service from binary storage:");
            foreach (var book in service2.GetBooks())
            {
                Console.WriteLine(book);
            }


            Console.ReadLine();
        }
    }

    class AuthorPredicate : IPredicate<Book>
    {
        public bool IsTrue(Book book)
        {
            return book != null && book.Author == "Andrzej Sapkowski";
        }
    }

    class PricePredicate : IPredicate<Book>
    {
        public bool IsTrue(Book book)
        {
            return book != null && book.Price < 15;
        }
    }

    class PublishYearComparer : IComparer<Book>
    {
        public int Compare(Book book1, Book book2)
        {
            return book1 != null && book2 != null ? book1.PublishYear.CompareTo(book2.PublishYear) : book1 == null ? 1 : -1;
        }
    }

    class PriceComparer : IComparer<Book>
    {
        public int Compare(Book book1, Book book2)
        {
            return book1 != null && book2 != null ? book1.Price.CompareTo(book2.Price) : book1 == null ? 1 : -1;
        }
    }
}

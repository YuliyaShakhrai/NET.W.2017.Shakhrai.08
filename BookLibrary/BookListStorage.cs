using System;
using System.Collections.Generic;
using System.IO;
using BookStore;

namespace BookStore
{
    public class BookListStorage : IBookStorage
    {
        #region Private fields

        private string filepath;

        #endregion

        #region Constructor

        public BookListStorage(string filepath)
        {
            this.filepath = string.Copy(filepath) ?? throw new ArgumentNullException($"{nameof(filepath)} is not exist.");
        }

        #endregion

        #region Interface implementations
        public IEnumerable<Book> Load()
        {
            if (!File.Exists(filepath))
            {
                throw new ArgumentException($"{nameof(filepath)} is not exist.");
            }

            var books = new List<Book>();

            using (BinaryReader reader = new BinaryReader(File.Open(filepath, FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    books.Add(ReadBook(reader));
                }
            }

            return books;
        }

        public void Save(IEnumerable<Book> books)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filepath, FileMode.Create)))
            {
                foreach (Book book in books)
                {
                    WriteBook(writer, book);
                }
            }
        }

        #endregion

        #region Private methods

        private void WriteBook(BinaryWriter writer, Book book)
        {
            writer.Write(book.Title);
            writer.Write(book.Author);
            writer.Write(book.Price);
            writer.Write(book.Pages);
            writer.Write(book.ISBN);
            writer.Write(book.Publisher);
            writer.Write(book.PublishYear);
        }

        private Book ReadBook(BinaryReader reader)
        {
            string title = reader.ReadString();
            string author = reader.ReadString();
            double price = reader.ReadDouble();
            int pages = reader.ReadInt32();
            string isbn = reader.ReadString();
            string publisher = reader.ReadString();
            int publishYear = reader.ReadInt32();

            return new Book(title, author, isbn, publisher, publishYear, pages, price);
        }

        #endregion
    }
}

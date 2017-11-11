using System;

namespace BookStore
{
    public class Book : IComparable<Book>, IComparable, IEquatable<Book>
    {
        #region Private fields
        private string _isbn;
        private int _pages;
        private double _price;
        #endregion

        #region Public properties
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Publisher { get; private set; }
        public int PublishYear { get; private set; }

        public string ISBN
        {
            get { return _isbn; }
            private set
            {
                _isbn = value;
            }
        }

        public int Pages
        {
            get { return _pages; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(Pages)} is lesser than 0.");
                }

                _pages = value;
            }
        }

        public double Price
        {
            get { return _price; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(Price)} is lesser than 0.");
                }

                _price = value;
            }
        }
        #endregion

        #region Constructor
        public Book(string title = "", string author = "", string isbn = "", string publisher = "",
            int publishYear = 0, int pages = 0, double price = 0)
        {
            Title = title ?? string.Empty;
            Author = author ?? string.Empty;
            ISBN = isbn;
            Publisher = publisher ?? string.Empty;
            PublishYear = publishYear;
            Pages = pages;
            Price = price;
        }
        #endregion

        #region Overriden methods
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals(obj as Book);
        }

        public override string ToString()
        {
            return $"Title: {Title}; Author: {Author}; Price: {Price}; " +
                $"ISBN: {ISBN}; " + $"Publisher: {Publisher}; Publish year: {PublishYear}; " + $"Pages: {Pages};";
        }

        public override int GetHashCode()
        {
            int hash = 13;

            hash = hash * 7 + Title.GetHashCode();
            hash = hash * 7 + Author.GetHashCode();
            hash = hash * 7 + ISBN.GetHashCode();
            hash = hash * 7 + Publisher.GetHashCode();
            hash = hash * 7 + PublishYear.GetHashCode();
            hash = hash * 7 + Price.GetHashCode();
            hash = hash * 7 + Pages.GetHashCode();

            return hash;
        }
        #endregion

        #region Interface implementations
        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;

            bool result = Title == other.Title && Author == other.Author &&
                          Price == other.Price && ISBN == other.ISBN &&
                          Pages == other.Pages && Publisher == other.Publisher &&
                          PublishYear == other.PublishYear;

            return result;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            return CompareTo(obj as Book);
        }

        public int CompareTo(Book other)
        {
            if (ReferenceEquals(other, null)) return 1;

            return Title.CompareTo(other.Title);
        }
        #endregion

    }
}

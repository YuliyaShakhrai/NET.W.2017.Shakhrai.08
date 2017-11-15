using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public abstract class AbstractAccount
    {
        #region Private fields

        private string _iban;
        private string _firstName;
        private string _lastName;
        private decimal _balance;
        private long _bonusPoints;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates bank account with zero balance and bonus points.
        /// </summary>
        /// <param name="iban">Account's IBAN.</param>
        /// <param name="firstName">First name of the account's owner.</param>
        /// <param name="lastName">Last name of the account's owner.</param>
        protected AbstractAccount(string iban, string firstName, string lastName)
        {
            Iban = iban;
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Creates bank account with input balance and bonus points.
        /// </summary>
        /// <param name="id">Client's id.</param>
        /// <param name="firstName">First name of the account's owner.</param>
        /// <param name="lastName">Last name of the account's owner.</param>
        /// <param name="balance">Account balance.</param>
        /// <param name="bonusPoints">Account bonus points.</param>
        /// <param name="isClosed">Account status.</param>
        protected AbstractAccount(string iban, string firstName, string lastName, decimal balance, long bonusPoints, bool isClosed)
        {
            Iban = iban;
            FirstName = firstName;
            LastName = lastName;
            Balance = balance;
            BonusPoints = bonusPoints;
            IsClosed = isClosed;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Account's identifier number.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public string Iban
        {
            get
            {
                return _iban;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"{nameof(Iban)} cannot be null.");
                }

                _iban = value;
            }
        }

        /// <summary>
        /// First name of the account's owner.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public string FirstName
        {
            get
            {
                return _firstName;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"{nameof(FirstName)} cannot be null.");
                }

                _iban = value;
            }
        }

        /// <summary>
        /// Last name of the account's owner.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public string LastName
        {
            get
            {
                return _lastName;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException($"{nameof(FirstName)} cannot be null.");
                }

                _lastName = value;
            }
        }

        /// <summary>
        /// Current balance on the account.
        /// </summary>
        /// <exception cref="ArgumentException">Value is less than zero.</exception>
        public decimal Balance
        {
            get
            {
                return _balance;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"{nameof(Balance)} is less than zero.");
                }

                _balance = value;
            }
        }

        /// <summary>
        /// Current bonus points on the account.
        /// </summary>
        public long BonusPoints
        {
            get
            {
                return _bonusPoints;
            }

            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"{nameof(BonusPoints)} is less than zero.");
                }

                _bonusPoints = value;
            }
        }

        /// <summary>
        /// Account's closed status.
        /// </summary>
        public bool IsClosed { get; private set; }

        protected abstract int ReplenishBalanceCoeff { get; }

        protected abstract int ReplenishValueCoeff { get; }

        protected abstract int WithdrawBalanceCoeff { get; }

        protected abstract int WithdrawValueCoeff { get; }

        #endregion

        #region Public methods

        /// <summary>
        /// Closes account.
        /// </summary>
        public void Close()
        {
            IsClosed = true;
        }

        /// <summary>
        /// Replenish amount to the account.
        /// </summary>
        /// <param name="amount">Amount.</param>
        public void Replenish(decimal amount)
        {
            if (IsClosed)
            {
                throw new InvalidOperationException($"Account is closed. All operations are unavailable.");
            }

            Balance += amount;
            BonusPoints += (long)((ReplenishBalanceCoeff * Balance) + (ReplenishValueCoeff * amount));
        }

        /// <summary>
        /// Withdraw amount from the account.
        /// </summary>
        /// <param name="amount">Amount.</param>
        public void Withdraw(decimal amount)
        {
            if (IsClosed)
            {
                throw new InvalidOperationException($"Account is closed. All operations are unavailable.");
            }

            Balance -= amount;
            BonusPoints -= (long)((WithdrawBalanceCoeff * Balance) + (WithdrawValueCoeff * amount));
        }
        
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public sealed class BaseBankAccount : AbstractAccount
    {
        #region Private fields

        #endregion

        #region Constructors

        protected BaseBankAccount(string iban, string firstName, string lastName) : base(iban, firstName, lastName)
        {
        }

        protected BaseBankAccount(string iban, string firstName, string lastName, decimal balance, long bonusPoints, bool isClosed) : base(iban, firstName, lastName, balance, bonusPoints, isClosed)
        {
        }

        #endregion

        #region Properties

        protected override int ReplenishBalanceCoeff => 3;

        protected override int ReplenishValueCoeff => 1;

        protected override int WithdrawBalanceCoeff => 2;

        protected override int WithdrawValueCoeff => 1;

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE
{
    internal class Rental : Expense
    {
        private bool renting;
        private int monthlyRent;

        public Rental()
        {
            renting = false;
            monthlyRent = 0;
        }

        public Rental(int income, List<double> expenses, bool renting, int monthlyRent) : base(income, expenses)
        {
            this.renting = renting;
            this.monthlyRent = monthlyRent;
        }

        public bool Renting
        {
            set { renting = value; }
            get { return renting; }
        }

        public int MonthlyRent
        {
            set { monthlyRent = value; }
            get { return monthlyRent; }
        }
    }
}

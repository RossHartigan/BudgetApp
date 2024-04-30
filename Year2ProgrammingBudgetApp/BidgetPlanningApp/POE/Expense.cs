using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE
{
    internal class Expense
    { 

        private int income;
        private List<double> expenses = new List<double>();

        public Expense()
        {
            income = 0;
        }

        public Expense(int income, List<double> expenses)
        {
            this.income = income;
            this.expenses = expenses;
        }

        public int Income
        {
            set { income = value; }
            get { return income; }
        }

        public List<double> Expenses
        {
            get { return expenses; }

            set
            {  
                expenses = value;
            }
        }
    }
}

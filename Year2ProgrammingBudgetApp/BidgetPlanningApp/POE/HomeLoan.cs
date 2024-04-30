using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE
{
    internal class HomeLoan : Expense
    {
        private int purchasePriceHouse;
        private int depositHouse;
        private double homeRate;
        private int months;
        private double bond;


        public HomeLoan()
        {
            purchasePriceHouse = 0;
            depositHouse = 0;
            homeRate = 0;
            months = 0;
            bond = 0.0;

        }

        public HomeLoan(int income, List<double> expenses, int purchasePriceHouse, int depositHouse, double homeRate, int months, double bond) : base(income, expenses)
        {
            this.purchasePriceHouse = purchasePriceHouse;
            this.depositHouse = depositHouse;
            this.homeRate = homeRate;
            this.months = months;
            this.bond = bond;

        }

        public int PurchaseHouse
        {
            set { purchasePriceHouse = value; }
            get { return purchasePriceHouse; }
        }

        public int DepositHouse
        {
            set { depositHouse = value; }
            get { return depositHouse; }
        }

        public double RateHouse
        {
            set { homeRate = value; }
            get { return homeRate; }
        }

        public int Months
        {
            set { months = value; }
            get { return months; }
        }

        public double Bond
        {
            get { return bond; }
        }

        public double calcHouseExpense()                                            //this method will add up all the expenses together and will also calculate the bond monthly payment if necessary
        {
            double total = 0;
            bool validBond = false;

            bond = (((PurchaseHouse - DepositHouse) * (1 + ((RateHouse / 100) * (Months / 12)))) / Months);
            validBond = checkBond(bond);                                    //this method checks to see if the bond will be valid

            if (validBond)                                                  //if the bond is valid
            {
                total = bond;
            }
            else                                                            //if the bond is invalid then it will display an error message and exit the program
            {
                Console.WriteLine($"This property is too expensive as it exceeds 1/3 of your gross monthly income in payments ({bond}). Try again");
                System.Environment.Exit(0);
            }

            return total;                                                       //returns the total expenses 
        }

        public bool checkBond(double bond)                                      //this methods checks to see if the bond is valid by 
        {                                                                       //making sure the monthly payments will not exceed 1/3 of the users gross income amount 
            bool output = false;                                                //sets the bond to false originally so no need to ad an else statement as if the bond isn't valid
                                                                                //it will skip the if statement and the output will stay false
            if (bond < (Income / 3))
            {
                output = true;
            }
            return output;
        }
    }
}

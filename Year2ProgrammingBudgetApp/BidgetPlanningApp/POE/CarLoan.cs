using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE
{
    internal class CarLoan : Expense
    {

        private bool carLoan;
        private string model;
        private int purchasePriceCar;
        private int depositCar;
        private double carRate;
        private int insurance;
        private double carMonthly;

        public CarLoan() : base()
        {
            carLoan = false;
            model = "";
            purchasePriceCar = 0;
            depositCar = 0;
            carRate = 0;
            insurance = 0;
            carMonthly = 0;
        }

        public CarLoan(int income, List<double> expenses, bool carLoan, string model, int purchasePriceCar, int depositCar, double carRate, int insurance, double carMonthly) : base(income, expenses)
        {
            this.carLoan = carLoan;
            this.model = model;
            this.purchasePriceCar = purchasePriceCar;
            this.depositCar = depositCar;
            this.carRate = carRate;
            this.insurance = insurance;
            this.carMonthly = carMonthly;
        }

        public bool Car
        {
            set { carLoan = value; }
            get { return carLoan; }
        }

        public string Model
        {
            set { model = value; }
            get { return model; }
        }

        public int PurchaseCar
        {
            set { purchasePriceCar = value; }
            get { return purchasePriceCar; }
        }

        public int DepositCar
        {
            set { depositCar = value; }
            get { return depositCar; }
        }

        public double RateCar
        {
            set { carRate = value; }
            get { return carRate; }
        }

        public int Insurance
        {
            set { insurance = value; }
            get { return insurance; }
        }

        public double CarMonthly
        {
            get { return carMonthly; }
        }

        public double calcCarExpense()
        {
            carMonthly = Insurance + (((PurchaseCar - DepositCar) * (1 + ((RateCar / 100) * 5)) / 60));// this simply calculates the monthly payments for the car and add the monthly insurance to the price of the car


            return carMonthly;
        }
    }
}

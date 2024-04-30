using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POE 
{
    internal class Savings
    {
        private bool savingsStatus;
        private int savingsAmount;
        private double savingsInterest;
        private int savingsPeriod;
        public Savings()
        {
            savingsStatus = false;
            savingsAmount = 0;
            savingsInterest = 0;
            savingsPeriod = 0; 
        }
        public Savings(bool savingsStatus, int savingsAmount, double savingsInterest, int savingsPeriod)
        {
            this.savingsStatus = savingsStatus;
            this.savingsAmount = savingsAmount;
            this.savingsInterest = savingsInterest;
            this.savingsPeriod = savingsPeriod;
        } 
        public bool SavingsSatus
        {
            set { savingsStatus = value; }
            get { return savingsStatus; }
        }
        public int SavingsAmount
        {
            set { savingsAmount = value; }
            get { return savingsAmount; }
        }
        public double SavingsInterest
        {
            set { savingsInterest = value; }
            get { return savingsInterest; }
        }
        public int SavingsPeriod
        {
            set { savingsPeriod = value; }  
            get { return savingsPeriod; }   
        }

        public double calcSavingsAmount() // calculates how much the user will need to save per month for anual compound interest 
        {
            double savings;

            savings = ((SavingsAmount)/ (Math.Pow((1 + SavingsInterest / 100) , (1 * SavingsPeriod))) / (SavingsPeriod * 12));

            return savings;
        }
    }
}

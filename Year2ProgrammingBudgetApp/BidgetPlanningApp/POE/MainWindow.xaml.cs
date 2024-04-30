using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool expenseStatus = true;          //these boolean are used for the progression of the form
        bool livingStatus = false;
        bool carStatus = false;
        bool reportStatus = false;
        bool savingStatus = false;

        HomeLoan homeLoan = new HomeLoan();     //global object to be used by all the functions easily
        Rental rnt = new Rental();
        CarLoan car = new CarLoan();
        Savings save = new Savings();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void expenseSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (!expenseStatus) { return; }   //if the expense status is set to false then it will exit the method

            //Store input into the expense list

            homeLoan.Income = Convert.ToInt32(grossMonthlyIncome.Text);
            homeLoan.Expenses.Add(Convert.ToDouble(taxReduction.Text));
            homeLoan.Expenses.Add(Convert.ToDouble(groceriesExpense.Text));
            homeLoan.Expenses.Add(Convert.ToDouble(utilitiesExpense.Text));
            homeLoan.Expenses.Add(Convert.ToDouble(travelExpense.Text));
            homeLoan.Expenses.Add(Convert.ToDouble(phoneExpense.Text));
            homeLoan.Expenses.Add(Convert.ToDouble(otherExpense.Text));

            expenseStatus = !expenseStatus;          //toggle
            livingStatus = !livingStatus;                   //toggle
            expenseSubmit.IsEnabled = false;             //disables the button once clicked
            livingStatusSubmit.IsEnabled = true;

        }

        private void reportButton_Click(object sender, RoutedEventArgs e)       //This method generates the report based on all the user input and displays it in the text box
        {
            double totalExpense = 0;
            string output;
            string descendingOutput = "\nExpenses in descneding order : \n";
            string savingsMessage = "\nThe amount you will need to save per month is : R ";

            reportBox.Text = String.Empty;

            totalExpense = homeLoan.Expenses.Sum(x => Convert.ToDouble(x));

            output = ($"Expense Breakdown : \n\nAll Expenses : R {totalExpense.ToString("0.00")}\n");

            //Renting or Home Loan?

            if(rnt.Renting)
            {
                output = output + ($"Monthly Rental : R {rnt.MonthlyRent.ToString("0.00")}\n");
            }
            else
            {
                output = output + ($"Monthly Bond Payment : R {homeLoan.Bond.ToString("0.00")}\n");
            }

            //Car Payment?

            if (car.Car)
            {
                output = output + ($"Monthly Car Payment for {car.Model} (including insurance) is : R {car.CarMonthly.ToString("0.00")} \n");
            }
            else
            {
                output = output + ($"No Car payment made.\n");
            }

            output = output + ($"Other Monthly Expenses : R {(totalExpense - rnt.MonthlyRent - homeLoan.Bond - car.CarMonthly - homeLoan.Expenses[0]).ToString("0.00")}\n");
            output = output + ($"Tax : R {homeLoan.Expenses[0].ToString("0.00")}\n");
            output = output + ($"The total amount of money you will have left for the month is : R {(homeLoan.Income - totalExpense).ToString("0.00")}\n");

            homeLoan.Expenses.Sort();
            homeLoan.Expenses.Reverse();

            foreach (double k in homeLoan.Expenses)// this will display the expenses in descending order.
            {
                descendingOutput = descendingOutput + ($"R : {k.ToString("0.00")}\n");
            }

            //Savings           

            if (save.SavingsSatus)
            {
                save.SavingsAmount = Convert.ToInt32(savingsAmountInput.Text);
                save.SavingsPeriod = Convert.ToInt32(savingsPeriodInput.Text);
                save.SavingsInterest = Convert.ToDouble(savingsInterestInput.Text);

                savingsMessage = savingsMessage + save.calcSavingsAmount().ToString("0.00");

                reportBox.Text = output + descendingOutput + savingsMessage;
            }
            else
            {
                reportBox.Text = output + descendingOutput;
            }
        }

        private void livingStatusSubmit_Click(object sender, RoutedEventArgs e)     //this will submit the data of the living status to the appropriate class
        {
            if (!livingStatus) { return; }                    //if living status is false the it will exit the method            

            if (rentingRadioButton.IsChecked == true) //will display the renting fields on the form
            {
                rnt.Renting = true;
                rnt.MonthlyRent = Convert.ToInt32(rentalInput.Text);

                homeLoan.Expenses.Add(rnt.MonthlyRent);

                livingStatus = !livingStatus;       //toggle 
                carStatus = !carStatus;             //toggle
                livingStatusSubmit.IsEnabled = false;           //disables the button once clicked 
                carStatusSubmit.IsEnabled = true;      //enables the other button
            }
            else if (homeLoanRadioButton.IsChecked == true) //will display the hom loan details on the form
            {
                rnt.Renting = false;

                homeLoan.PurchaseHouse = Convert.ToInt32(homePrice.Text);
                homeLoan.DepositHouse = Convert.ToInt32(depositPrice.Text);
                homeLoan.RateHouse = Convert.ToInt32(rate.Text);
                homeLoan.Months = Convert.ToInt32(months.Text);

                homeLoan.calcHouseExpense();
                homeLoan.Expenses.Add(homeLoan.Bond);

                livingStatus = !livingStatus;       //toggle 
                carStatus = !carStatus;             //toggle
                livingStatusSubmit.IsEnabled = false;           //disables the button once clicked 
                carStatusSubmit.IsEnabled = true;      //enables the other button

                //MessageBox.Show($"Bond : {homeLoan.Bond.ToString("0.00")}");
            }
            else
            {
                MessageBox.Show("Error! Choose an option, either Renting or Home Loan");
            }            
        }

        private void carStatusSubmit_Click(object sender, RoutedEventArgs e)        //will capture the input of the car loan payment fields
        {
            if (!carStatus) { return; }           //if car status is false the it will exit the method

            if (yesCarRadioButton.IsChecked == true)        //if the yes radio button is checked will display the appropriate fiuelds in order for the user to fill out the data 
            {
                car.Car = true;

                car.Model = carModelName.Text;
                car.PurchaseCar = Convert.ToInt32(carPurchaseInput.Text);
                car.DepositCar = Convert.ToInt32(carDepositPrice.Text);
                car.RateCar = Convert.ToInt32(carRate.Text);
                car.Insurance = Convert.ToInt32(carInsurance.Text);

                homeLoan.Expenses.Add(car.calcCarExpense());

                carStatus = !carStatus;                   //toggle
                reportStatus = !reportStatus;       //toggle
                carStatusSubmit.IsEnabled = false;         //disables the button once clicked
                reportButton.IsEnabled = true;
            }
            else if (noCarRadioButton.IsChecked == true)        //user has chosen not to add a car payment
            {
                car.Car = false;

                carStatus = !carStatus;                   //toggle
                reportStatus = !reportStatus;       //toggle
                carStatusSubmit.IsEnabled = false;         //disables the button once clicked
                reportButton.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Error! Choose an option for car loan status");
            }
        }

        private void yesCarRadioButton_Click(object sender, RoutedEventArgs e)      //toggles visibilty of car fields
        {
            carModelLabel.Visibility = Visibility.Visible;
            carModelName.Visibility = Visibility.Visible;
            carPurchaseLabel.Visibility = Visibility.Visible;
            carPurchaseInput.Visibility = Visibility.Visible;
            carDepositLabel.Visibility = Visibility.Visible;
            carDepositPrice.Visibility = Visibility.Visible;
            carRateLabel.Visibility = Visibility.Visible;
            carRate.Visibility = Visibility.Visible;
            insuranceLabel.Visibility = Visibility.Visible;
            carInsurance.Visibility = Visibility.Visible;
        }

        private void noCarRadioButton_Click(object sender, RoutedEventArgs e)       //toggles visibilty of car fields
        {
            carModelLabel.Visibility = Visibility.Hidden;
            carModelName.Visibility = Visibility.Hidden;
            carPurchaseLabel.Visibility = Visibility.Hidden;
            carPurchaseInput.Visibility = Visibility.Hidden;
            carDepositLabel.Visibility = Visibility.Hidden;
            carDepositPrice.Visibility = Visibility.Hidden;
            carRateLabel.Visibility = Visibility.Hidden;
            carRate.Visibility = Visibility.Hidden;
            insuranceLabel.Visibility = Visibility.Hidden;
            carInsurance.Visibility = Visibility.Hidden;
        }

        private void rentingRadioButton_Click(object sender, RoutedEventArgs e)//toggles visibilty of renting field
        {
            homePrice.Visibility = Visibility.Hidden;
            homePriceLabel.Visibility = Visibility.Hidden;
            depositPrice.Visibility = Visibility.Hidden;
            depositLabel.Visibility = Visibility.Hidden;
            rate.Visibility = Visibility.Hidden;
            rateLabel.Visibility = Visibility.Hidden;
            months.Visibility = Visibility.Hidden;
            monthsLabel.Visibility = Visibility.Hidden;

            rentingLabel.Visibility = Visibility.Visible;
            rentalInput.Visibility = Visibility.Visible;
        }

        private void homeLoanRadioButton_Click(object sender, RoutedEventArgs e)//toggles visibilty of home loan fields
        {
            rentingLabel.Visibility = Visibility.Hidden;
            rentalInput.Visibility = Visibility.Hidden;

            homePrice.Visibility = Visibility.Visible;
            homePriceLabel.Visibility = Visibility.Visible;
            depositPrice.Visibility = Visibility.Visible;
            depositLabel.Visibility = Visibility.Visible;
            rate.Visibility = Visibility.Visible;
            rateLabel.Visibility = Visibility.Visible;
            months.Visibility = Visibility.Visible;
            monthsLabel.Visibility = Visibility.Visible;
        }

        private void savingsSubmit_Click(object sender, RoutedEventArgs e)      //displays the data for the savings input
        {
            savingStatus = !savingStatus;           //toggles the savings status           

            savingsAmountLabel.Visibility = Visibility.Visible;
            savingsAmountInput.Visibility = Visibility.Visible;
            savingsPeriodLabel.Visibility = Visibility.Visible;
            savingsPeriodInput.Visibility = Visibility.Visible;
            savingsInterestLabel.Visibility = Visibility.Visible;
            savingsInterestInput.Visibility = Visibility.Visible;

            save.SavingsSatus = true;
        }

        private void savingsCancel_Click(object sender, RoutedEventArgs e)      //cancels the savings option
        {
            savingStatus = !savingStatus;           //toggles the savings status           

            savingsAmountLabel.Visibility = Visibility.Hidden;
            savingsAmountInput.Visibility = Visibility.Hidden;
            savingsPeriodLabel.Visibility = Visibility.Hidden;
            savingsPeriodInput.Visibility = Visibility.Hidden;
            savingsInterestLabel.Visibility = Visibility.Hidden;
            savingsInterestInput.Visibility = Visibility.Hidden;

            save.SavingsSatus = false;
        }
    }
}

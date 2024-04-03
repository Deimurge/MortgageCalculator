// See https://aka.ms/new-console-template for more information
// MortgageCalculator.cs
class MortgageCalculator
{
    public static void Main()
    {
        // Welcome message
        Console.WriteLine("Hello User");
        Console.WriteLine("Welcome to UXI Mortgage Loans Calculater");

        // Get user input for loan amount
        Console.WriteLine("Please enter enter your loan amount:");
        double loanAmount = double.Parse(Console.ReadLine());

        // Validate loan amount
        if (loanAmount < 10000 || loanAmount > 100000000)
        {
            Console.WriteLine("Warning: The minimum loan amount is R10,000.");
            Console.WriteLine("Warning: The maximum loan amount is R100,000,000.");
            throw new Exception();
        }

        // Get user input for annual interest rate
        Console.WriteLine("Please enter enter the annual Interest Rate (e.g. 5.5 for 5.5%):");
        double annualInterestRate = double.Parse(Console.ReadLine()) / 100 / 12;
        // Get user input for annual interest rate
        if (annualInterestRate > 12)
        {
            Console.WriteLine("Warning: The maximum annual interest rate is 12%.");
            throw new Exception();
            
        }

        // Get user input for loan term in years
        Console.WriteLine("Please enter enter the loan Term in Years:");
        int loanTermYears = int.Parse(Console.ReadLine());

        // Validate loan term
        if (loanTermYears > 79)
        {
            Console.WriteLine("Warning: The maximum loan term is 79 years.");
            Console.WriteLine("Warning: Don't act like you gonna live that long.");
            throw new Exception();
        }

        // Calculate monthly repayment
        double monthlyPayment = CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears);
        // Calculate total interest paid
        double totalInterestPaid = CalculateTotalInterestPaid(loanAmount, annualInterestRate, loanTermYears);
        // Generate amortization schedule
        List<AmortizationScheduleEntry> amortizationSchedule = GenerateAmortizationSchedule(loanAmount, annualInterestRate, loanTermYears);

        // Display monthly repayment
        Console.WriteLine($"Your monthly replayment will be: R{monthlyPayment:F2}");
        // Display total interest paid
        Console.WriteLine($"Your Total Interest Paid will be: R{totalInterestPaid:F2}");
        // Display amortization schedule
        Console.WriteLine("Amortization schedule:");

        int i = 1;
        foreach (var entry in amortizationSchedule)
        {
            Console.WriteLine($"Payment {i++}: Interest {entry.InterestPaid:F2} Principal {entry.PrincipalPaid:F2} Balance {entry.RemainingBalance:F2}");
        }
    }

    // Calculate monthly repayment
    static double CalculateMonthlyRepayment(double loanAmount, double annualInterestRate, int loanTermYears)
    {
        // Calculate monthly interest rate
        double monthlyInterestRate = annualInterestRate * 12;
        // Calculate monthly repayment
        return (loanAmount * monthlyInterestRate) / (1 - Math.Pow(1 + monthlyInterestRate, -loanTermYears * 12));
    }

    // Calculate total interest paid
    static double CalculateTotalInterestPaid(double loanAmount, double annualInterestRate, int loanTermYears)
    {
        // Calculate monthly interest rate
        double monthlyInterestRate = annualInterestRate * 12;
        // Calculate total interest paid
        return CalculateMonthlyRepayment(loanAmount, annualInterestRate, loanTermYears) * loanTermYears * 12 - loanAmount;
    }

    // Generate amortization schedule
    static List<AmortizationScheduleEntry> GenerateAmortizationSchedule(double loanAmount, double annualInterestRate, int loanTermYears)
    {
        int numberOfPayments = loanTermYears * 12;
        double monthlyInterestRate = annualInterestRate * 12;
        double monthlyPayment = (loanAmount * monthlyInterestRate) / (1 - Math.Pow(1 + monthlyInterestRate, -numberOfPayments));

        var schedule = new List<AmortizationScheduleEntry>();

        for (int i = 1; i <= numberOfPayments; i++)
        {
            double interest = loanAmount * monthlyInterestRate;
            double principal = monthlyPayment - interest;
            loanAmount -= principal;

            schedule.Add(new AmortizationScheduleEntry
            {
                PaymentNumber = i,
                PaymentAmount = monthlyPayment,
                InterestPaid = Math.Round(interest, 2),
                PrincipalPaid = Math.Round(principal, 2),
                RemainingBalance = Math.Round(loanAmount, 2)
            });
        }

        return schedule;
    }

    public class AmortizationScheduleEntry
    {
        public int PaymentNumber { get; set; }
        public double PaymentAmount { get; set; }
        public double InterestPaid { get; set; }
        public double PrincipalPaid { get; set; }
        public double RemainingBalance { get; set; }
    }
}
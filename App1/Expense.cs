namespace PartyCalculator
{
    public class Expense
    {
        private string userName;
        private string expenseType;
        private string expensedescription;
        private double amount;

        public Expense(string userName, string expenseType, string expenseDesc, double amount)
        {
            this.userName = userName;
            this.amount = amount;
            expensedescription = expenseDesc;
            this.expenseType = expenseType;
        }
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public string ExpenseType
        {
            get
            {
                return expenseType;
            }

            set
            {
                expenseType = value;
            }
        }

        public string Expensedescription
        {
            get
            {
                return expensedescription;
            }

            set
            {
                expensedescription = value;
            }
        }

        public double Amount
        {
            get
            {
                return amount;
            }

            set
            {
                amount = value;
            }
        }
    }
}
